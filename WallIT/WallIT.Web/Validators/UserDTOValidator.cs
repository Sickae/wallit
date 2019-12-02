using FluentValidation;
using WallIT.Shared.DTOs;

namespace WallIT.Web.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail is required!")
                .EmailAddress().WithMessage("Format is incorrect!");
        }
    }
}
