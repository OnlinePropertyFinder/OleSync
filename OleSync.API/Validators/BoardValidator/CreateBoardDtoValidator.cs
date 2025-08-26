using FluentValidation;
using OleSync.Application.Boards.Dtos;

namespace OleSync.API.Validators.BoardValidator
{
    public class CreateBoardDtoValidator : AbstractValidator<CreateOrUpdateBoardDto>
    {
        public CreateBoardDtoValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Borad name is required.");
            RuleFor(x => x.Purpose).NotEmpty().WithMessage("Board purpose is required.");
            RuleFor(x => x.BoardType).IsInEnum().WithMessage("Invalid board type.");

            // Validate members when provided
            When(x => x.Members != null && x.Members.Count > 0, () =>
            {
                RuleForEach(x => x.Members!)
                    .SetValidator(new CreateBoardMemberDtoValidator());
            });
        }
    }
}
