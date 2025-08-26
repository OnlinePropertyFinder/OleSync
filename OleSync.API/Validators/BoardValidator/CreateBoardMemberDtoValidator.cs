using FluentValidation;
using OleSync.Application.Boards.Dtos;

namespace OleSync.API.Validators.BoardValidator
{
	public class CreateBoardMemberDtoValidator : AbstractValidator<CreateBoardMemberDto>
	{
		public CreateBoardMemberDtoValidator()
		{
			RuleFor(x => new { x.EmployeeId, x.GuestId })
				.Must(ids => !(Normalize(ids.EmployeeId).HasValue && Normalize(ids.GuestId).HasValue))
				.WithMessage("Member cannot be both Employee and Guest");

			// If neither EmployeeId nor GuestId provided, then require fields to create Guest
			When(x => !Normalize(x.EmployeeId).HasValue && !Normalize(x.GuestId).HasValue, () =>
			{
				RuleFor(x => x.FullName)
					.NotEmpty().WithMessage("FullName is required when not linking Employee or Guest");
				RuleFor(x => x.Email)
					.NotEmpty().WithMessage("Email is required when not linking Employee or Guest");
				RuleFor(x => x.Phone)
					.NotEmpty().WithMessage("Phone is required when not linking Employee or Guest");
				RuleFor(x => x.Role)
					.IsInEnum().WithMessage("Role is required and must be valid");
				RuleFor(x => x.MemberType)
					.IsInEnum().WithMessage("MemberType is required and must be valid");
			});

			static int? Normalize(int? id) => id.HasValue && id.Value == 0 ? null : id;
		}
	}
}