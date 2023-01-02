using Core.Entities.Dto.User;
using Core.Entities.Dto.User.Auth;
using Core.Resources.Enum;
using FluentValidation;

namespace Service.ValidationRules.FluentValidation
{
    public class UserForLoginValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginValidator()
        {
            RuleFor(p => p.Username).NotEmpty().Length(3, 20).WithMessage("İstifadəçi adı minimum 3 maksimum 20 simvoldan ibarət olmalıdır");
            RuleFor(p => p.Password).NotEmpty().Length(3, 20).WithMessage("Şifrə minimum 3 maksimum 20 simvoldan ibarət olmalıdır");
        }
    }
}
