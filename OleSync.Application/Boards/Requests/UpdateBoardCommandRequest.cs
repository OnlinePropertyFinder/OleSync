using MediatR;
using OleSync.Application.Boards.Dtos;

namespace OleSync.Application.Boards.Requests
{
    public class UpdateBoardCommandRequest : IRequest<bool>
    {
        public required CreateOrUpdateBoardDto Board { get; set; }
        public long UserId { get; set; }
    }
}
