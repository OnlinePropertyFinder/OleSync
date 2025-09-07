using MediatR;
using OleSync.Application.Boards.Dtos;

namespace OleSync.Application.Boards.Requests
{
    public class GetBoardsLookupQueryRequest : IRequest<List<BoardLookupDto>> { }
}
