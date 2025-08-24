using MediatR;
using OleSync.Domain.Boards.Core.Entities;

namespace OleSync.Application.Boards.Requests
{
    public class GetAllBoardsQueryRequest : IRequest<List<Board>>
    {
    }
}
