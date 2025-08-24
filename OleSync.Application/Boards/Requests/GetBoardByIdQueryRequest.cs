using MediatR;
using OleSync.Domain.Boards.Core.Entities;

namespace OleSync.Application.Boards.Requests
{
    public class GetBoardByIdQueryRequest : IRequest<Board>
    {
        public int Id { get; set; }
    }
}
