using Core.Entities.Dto.User.Auth;
using Core.Resources.Enum;
using FluentValidation;

namespace Service.ValidationRules.FluentValidation
{
    public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Email düzgün formatda deyil.");
            RuleFor(p => p.Username).NotEmpty().Length(4, 20).WithMessage("İstifadəçi adı minimum 4 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.Password).NotEmpty().Length(3, 20).WithMessage("Şifrə minimum 4 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.FirstName).NotEmpty().Length(2, 20).WithMessage("Adınız minimum 2 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.LastName).NotEmpty().Length(2, 20).WithMessage("Soyadınız minimum 2 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.FatherName).NotEmpty().Length(2, 20).WithMessage("Ata adınız minimum 2 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.Position).NotEmpty().Length(2, 20).WithMessage("Vəzifəniz minimum 2 maksimum 50 simvoldan ibarət olmalıdır");
            RuleFor(p => p.UserType).NotEmpty().WithMessage("İşinizin növünü qeyd etməlisiniz.");
            RuleFor(p => p.ChiefId).GreaterThan(0).When(x => x.UserType == UserType.Worker).WithMessage("Əgər sadə işçisinizsə mütləq rəhbərinizi qeyd etməlisiniz");
        }
    }
}
