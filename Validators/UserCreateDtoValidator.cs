using FluentValidation;
using IamAlive.DTOs.UserDtos;

namespace IamAlive.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(x => x.EmergencyContactName)
                .NotEmpty().WithMessage("Emergency contact name is required.");

            RuleFor(x => x.EmergencyContactPhone)
                .NotEmpty().WithMessage("Emergency contact phone is required.");
        }
    }
}
