using FluentValidation;
using OleSync.Application.Committees.Dtos;

namespace OleSync.API.Validators.CommitteeValidator
{
	public class CreateCommitteeDtoValidator : AbstractValidator<CreateOrUpdateCommitteeDto>
	{
		public CreateCommitteeDtoValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Committee name is required.");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Committee description is required.");

			// Validate nested members when provided
			When(x => x.Members != null && x.Members.Count > 0, () =>
			{
				RuleForEach(x => x.Members!)
					.SetValidator(new CreateCommitteeMemberDtoValidator());
			});
		}
	}
}

