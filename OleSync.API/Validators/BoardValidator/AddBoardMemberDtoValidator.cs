using FluentValidation;
using OleSync.Application.Boards.Dtos;

namespace OleSync.API.Validators.BoardValidator
{
    public class AddBoardMemberDtoValidator : AbstractValidator<AddBoardMemberDto>
    {
        public AddBoardMemberDtoValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);

            RuleFor(x => new { x.EmployeeId, x.GuestId })
                .Must(ids => !(ids.EmployeeId.HasValue && ids.GuestId.HasValue))
                .WithMessage("Member cannot be both Employee and Guest");

            RuleFor(x => x)
                .Must(x => x.EmployeeId.HasValue || x.GuestId.HasValue)
                .WithMessage("Either EmployeeId or GuestId must be provided");
        }
    }
}

