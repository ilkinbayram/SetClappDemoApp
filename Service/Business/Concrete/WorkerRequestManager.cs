using AutoMapper;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Entities.Dto.WorkerRequest;
using Core.Extensions;
using Core.Resources.Enum;
using Core.Resources.OperationResources.Results;
using Core.Utilities.Generators;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Operation.Concrete.Base;
using Operation.Concrete.email.Sendgrid;
using Operation.Concrete.email.Sendgrid.Models.Request;
using Operation.Concrete.email.Sendgrid.Models.Template;
using Service.Business.Abstract;
using Service.ValidationRules.FluentValidation;
using System.Linq.Expressions;

namespace Service.Business.Concrete
{
    public class WorkerRequestManager : IWorkerRequestService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IWorkerRequestDal _workerRequestDal;
        private readonly IUserWorkerRequestDal _userWorkerRequestDal;
        public WorkerRequestManager(IWorkerRequestDal workerRequestDal,
                                    IMapper mapper,
                                    IUserDal userDal,
                                    IUserWorkerRequestDal userWorkerRequestDal)
        {
            _userDal = userDal;
            _mapper = mapper;
            _workerRequestDal = workerRequestDal;
            _userWorkerRequestDal = userWorkerRequestDal;
        }

        [ExceptionLogAspect(typeof(FileLogger))]
        public async Task<IDataResult<int>> UpdateAndAssignAsync(WorkerRequest entity, int userId, SendgridEmailDto emailDto)
        {
            var existUser = await _userDal.GetAsync(x => x.Id == userId);
            var requests = await _workerRequestDal.GetAllAsync(x => x.Status >= RequestStatus.SentToHr);
            var hrWorkers = await _userDal.GetAllAsync(x => x.UserType == UserType.HR);

            if (existUser.UserType == UserType.Worker)
            {
                entity.AssignedWorkerId = existUser.ChiefId;
            }
            else if (existUser.UserType == UserType.Manager)
            {
                entity.AssignedWorkerId = hrWorkers[requests.Count % hrWorkers.Count].Id;
            }
            else if (existUser.UserType == UserType.HR)
            {
                entity.AssignedWorkerId = 0;

                var manager = await _userDal.GetByIdAsync(entity.UserRequests.FirstOrDefault(x => x.IsRequestOwner).User.ChiefId);

                if (!manager.Email.Contains("example.com"))
                {
                    var reportEmailOperattion = new Operation<SendReportEmailFunction<OperationResponse>, OperationResponse>();

                    var userRequestDetails = await GetWorkerRequestDetailsAsync(existUser, entity.Id);

                    var reportingBody = new EmailBody<RequestReportingTemplate>
                    {
                        FromEmail = emailDto.DefaultEmailFrom,
                        ClientEmail = manager.Email,
                        Template = new RequestReportingTemplate
                        {
                            DocumentNumber = userRequestDetails.DocumentNumber,
                            FinishDate = userRequestDetails.FinishDate.ToShortDateString(),
                            StartFrom = userRequestDetails.StartFrom.ToShortDateString(),
                            RequestType = userRequestDetails.RequestType.Translate(),
                            FirstName = userRequestDetails.FirstName,
                            LastName = userRequestDetails.LastName,
                            AdditionalDescription = userRequestDetails.AdditionalDescription,
                            FatherName = userRequestDetails.FatherName,
                            Position = userRequestDetails.Position,
                            ReplacerFatherName = userRequestDetails.ReplacerFatherName,
                            ReplacerFirstName = userRequestDetails.ReplacerFirstName,
                            ReplacerLastName = userRequestDetails.ReplacerLastName
                        },

                        TemplateId = emailDto.TemplateId,
                        ApiKey = emailDto.ApiKey
                    };

                    var emailResult = await reportEmailOperattion.ExecuteAsync(reportingBody);
                }
            }

            int affectedRows = await _workerRequestDal.UpdateAsync(entity);
            IDataResult<int> dataResult;
            if (affectedRows > 0)
            {
                // TODO : Message
                dataResult = new SuccessDataResult<int>(affectedRows, "");
            }
            else
            {
                // TODO : Message
                dataResult = new ErrorDataResult<int>(-1, false, "");
            }

            return dataResult;
        }

        public async Task<IDataResult<WorkerRequest>> GetAsync(Expression<Func<WorkerRequest, bool>> filter)
        {
            var response = await _workerRequestDal.GetAsync(filter);
            if (response == null)
                return new ErrorDataResult<WorkerRequest>(null, "Entity is not exist with the given filter");

            return new SuccessDataResult<WorkerRequest>(response);
        }

        public async Task<IDataResult<int>> UpdateAsync(WorkerRequest entity)
        {
            int affectedRows = await _workerRequestDal.UpdateAsync(entity);

            // TODO: Messages
            if (affectedRows <= 0)
                throw new Exception("Error");

            return new SuccessDataResult<int>(affectedRows, "Success");
        }

        public IDataResult<IList<WorkerRequest>> GetListWithRelations(Expression<Func<WorkerRequest, bool>> filter = null)
        {
            var response = _workerRequestDal.GetWorkerRequestsWithRelations(filter);
            return new SuccessDataResult<IList<WorkerRequest>>(response);
        }

        [ValidationAspect(typeof(WorkerRequestValidator), typeof(int), Priority = 1)]
        public IDataResult<int> Add(CreateWorkerRequestDto entity, int activeUserId)
        {
            var activeUser = _userDal.GetById(activeUserId);
            var replacerUser = _userDal.GetById(entity.ReplacerUserId);

            var requestsCount = _workerRequestDal.GetCount();

            var mappedModel = new WorkerRequest
            {
                AdditionalDescription = entity.AdditionalDescription,
                FinishDate = Convert.ToDateTime(entity.FinishDate),
                StartFrom = Convert.ToDateTime(entity.StartFrom),
                Created_at = DateTime.Now,
                Modified_at = DateTime.Now,
                Created_by = $"{activeUser.FirstName} {activeUser.LastName}",
                Modified_by = $"{activeUser.FirstName} {activeUser.LastName}",
                DocumentNumber = StringGenerator.GenerateDocumentNumber(DateTime.Now, requestsCount),
                Status = RequestStatus.New,
                RequestType = entity.RequestType
            };

            var userWorkerRequestCreator = new UserWorkerRequest
            {
                IsRequestOwner = true,
                User = activeUser
            };
            var userWorkerRequestReplacer = new UserWorkerRequest
            {
                IsRequestOwner = false,
                User = replacerUser
            };

            mappedModel.UserRequests.Add(userWorkerRequestCreator);
            mappedModel.UserRequests.Add(userWorkerRequestReplacer);

            int affectedRows = _workerRequestDal.Add(mappedModel);

            // TODO: Messages
            if (affectedRows <= 0)
                throw new Exception("Error");

            return new SuccessDataResult<int>(affectedRows, "Success");
        }

        public async Task<UserWorkerRequestDetailsDto> GetWorkerRequestDetailsAsync(User existUser, int requestId)
        {
            var userRequests = await _userWorkerRequestDal.GetAllAsync(x => x.RequestId == requestId);
            var userOwner = userRequests.Where(x => x.IsRequestOwner).First().User;
            var replacer = userRequests.Where(x => !x.IsRequestOwner).First().User;
            var workerRequest = userRequests.Where(x => !x.IsRequestOwner).First().Request;

            UserWorkerRequestDetailsDto result = new UserWorkerRequestDetailsDto
            {
                FirstName = userOwner.FirstName,
                FatherName = userOwner.FatherName,
                LastName = userOwner.LastName,
                Position = userOwner.Position,
                ReplacerFirstName = replacer.FirstName,
                ReplacerFatherName = replacer.FatherName,
                ReplacerLastName = replacer.LastName,
                AdditionalDescription = workerRequest.AdditionalDescription,
                DocumentNumber = workerRequest.DocumentNumber,
                FinishDate = workerRequest.FinishDate,
                StartFrom = workerRequest.StartFrom,
                RequestType = workerRequest.RequestType,
                RequestStatus = workerRequest.Status,
                UserType = existUser.UserType
            };

            return result;
        }
    }
}
