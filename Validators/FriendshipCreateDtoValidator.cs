using FluentValidation;
using IamAlive.DTOs.FriendshipDtos;

namespace IamAlive.Validators
{
    public class FriendshipCreateDtoValidator : AbstractValidator<FriendshipCreateDto>
    {
        public FriendshipCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId is required.");

            RuleFor(x => x.FriendId)
                .GreaterThan(0).WithMessage("FriendId is required.");

            RuleFor(x => x)
                .Must(x => x.UserId != x.FriendId)
                .WithMessage("User cannot be friends with themselves.");
        }
    }
}
