using Core.Entities.Dto.WorkerRequest;
using Core.Resources.Enum;
using FluentValidation;

namespace Service.ValidationRules.FluentValidation
{
    public class WorkerRequestValidator : AbstractValidator<CreateWorkerRequestDto>
    {
        public WorkerRequestValidator()
        {
            RuleFor(p => p.ReplacerUserId).NotEmpty().GreaterThan(0).WithMessage("Sizi əvəz edəcək şəxsin adını mütləq seçməlisiniz");
            RuleFor(p => p.RequestType).Must(CheckRequestTypeIsValid).NotEmpty().WithMessage("Müraciətinizin növünü boş buraxa bilməzsiniz");
            RuleFor(p => p.StartFrom).NotEmpty().Must(CheckIsBusinessDay).WithMessage("Müraciətin başlama tarixi mütləqdir və Sadəcə həftənin iş günləri seçilə bilər. Başlama tarixi bu gündən böyük olmalıdır.");
            RuleFor(p => p.FinishDate).NotEmpty().Must(CheckIsBusinessDay).GreaterThan(x=>x.StartFrom).WithMessage("Müraciətin bitmə tarixi mütləqdir və Sadəcə həftənin iş günləri seçilə bilər. Bitmə tarixi bu gündən böyük olmaqla yanaşı eyni zamanda başlama tarixindən böyük olmalıdır.");
        }

        private bool CheckIsBusinessDay(string date)
        {
            if (DateTime.TryParse(date, out DateTime _temp))
            {
                var dateTime = Convert.ToDateTime(date);
                if (dateTime >= DateTime.Now)
                {
                    return dateTime.DayOfWeek != DayOfWeek.Sunday && dateTime.DayOfWeek != DayOfWeek.Saturday;
                }
            }

            return false;
        }

        private bool CheckRequestTypeIsValid(RequestType type)
        {
            return (int)type > 0;
        }
    }
}
