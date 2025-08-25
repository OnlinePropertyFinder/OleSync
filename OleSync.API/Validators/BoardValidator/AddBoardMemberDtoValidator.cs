using FluentValidation;
using OleSync.Application.Boards.Dtos;

namespace OleSync.API.Validators.BoardValidator
{
    public class AddBoardMemberDtoValidator : AbstractValidator<AddBoardMemberDto>
    {
        public AddBoardMemberDtoValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
            RuleFor(x => x.MemberType).IsInEnum();
            RuleFor(x => x.FullName).NotEmpty();

            RuleFor(x => new { x.EmployeeId, x.GuestId })
                .Must(ids => !(ids.EmployeeId.HasValue && ids.GuestId.HasValue))
                .WithMessage("Member cannot be both Employee and Guest");
        }
    }
}

