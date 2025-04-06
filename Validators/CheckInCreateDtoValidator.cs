using FluentValidation;
using IamAlive.DTOs.CheckInDtos;

namespace IamAlive.Validators
{
    public class CheckInCreateDtoValidator : AbstractValidator<CheckInCreateDto>
    {
        public CheckInCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than zero.");
        }
    }
}
