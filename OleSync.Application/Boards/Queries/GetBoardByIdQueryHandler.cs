using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Queries
{
    public class GetBoardByIdQueryHandler : IRequestHandler<GetBoardByIdQueryRequest, Board>
    {
        private readonly IBoardRepository _repository;
        public GetBoardByIdQueryHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Board> Handle(GetBoardByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var board = await _repository.GetByIdAsync(request.Id);
            return board;
        }
    }
}
