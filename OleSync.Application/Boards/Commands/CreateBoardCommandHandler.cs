using MediatR;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommandRequest, int>
    {
        private readonly IBoardRepository _repository;
        public CreateBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateBoardCommandRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var board = request.Board.ToDomainEntity();
            await _repository.AddAsync(board);
            return board.Id;
        }
    }
}
