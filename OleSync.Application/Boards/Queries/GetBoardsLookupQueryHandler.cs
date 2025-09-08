using MediatR;
using OleSync.Application.Boards.Dtos;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Queries
{
    public class GetBoardsLookupQueryHandler : IRequestHandler<GetBoardsLookupQueryRequest, List<BoardLookupDto>>
    {
        private readonly IBoardRepository _repository;
        public GetBoardsLookupQueryHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BoardLookupDto>> Handle(GetBoardsLookupQueryRequest request, CancellationToken cancellationToken)
        {
            var boards = await _repository.FilterByAsync(b => true);
            var list = boards.Select(b => b.ToLookupDto()).ToList();
            return list;
        }
    }
}
