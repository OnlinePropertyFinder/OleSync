using FluentValidation;
using OleSync.Application.Boards.Dtos;

namespace OleSync.API.Validators.BoardValidator
{
    public class UpdateBoardDtoValidator : AbstractValidator<CreateOrUpdateBoardDto>
    {
        public UpdateBoardDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Borad name is required.");
            RuleFor(x => x.Purpose).NotEmpty().WithMessage("Board purpose is required.");
            RuleFor(x => x.BoardType).IsInEnum().WithMessage("Invalid board type.");
        }
    }
}
