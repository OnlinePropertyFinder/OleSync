using MediatR;

namespace OleSync.Application.Boards.Requests
{
    public class DeleteBoardCommandRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public long UserId { get; set; }
    }
}
