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
        }
    }
}
