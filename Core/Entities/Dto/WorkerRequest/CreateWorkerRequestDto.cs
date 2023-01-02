using Core.Entities.Abstract;
using Core.Entities.Concrete.Base;
using Core.Resources.Enum;

namespace Core.Entities.Dto.WorkerRequest
{
    public class CreateWorkerRequestDto : BaseDto, ICreateDto
    {
        public int ReplacerUserId { get; set; }
        public string? AdditionalDescription { get; set; }
        public string StartFrom { get; set; }
        public string FinishDate { get; set; }
        public RequestType RequestType { get; set; }
    }
}
