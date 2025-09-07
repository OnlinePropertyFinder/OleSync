using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Commands
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommandRequest, bool>
    {
        private IBoardRepository _repository;
        public DeleteBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBoardCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, request.UserId);
            return true;
        }
    }
}
