using MediatR;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Commands
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommandRequest, bool>
    {
        private IBoardRepository _repository;
        public UpdateBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateBoardCommandRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var updateBoard = await _repository.GetByIdNotTrackedAsync(request.Board.Id) ?? throw new InvalidOperationException($"Board with id {request.Board.Id} not found.");
            updateBoard.UpdateFromDto(request.Board, request.UserId);

            await _repository.UpdateAsync(updateBoard);
            return true;
        }
    }
}
