using MediatR;
using OleSync.Application.Boards.Criterias;
using OleSync.Application.Boards.Dtos;
using OleSync.Application.Utilities;

namespace OleSync.Application.Boards.Requests
{
    public class GetPaginatedBoardQueryRequest : IRequest<PaginatedResult<BoardListDto>>
    {
        public required GetPaginatedBoardsCriteria Criteria { get; set; }
    }
}
