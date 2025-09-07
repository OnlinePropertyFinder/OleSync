using MediatR;
using Microsoft.AspNetCore.Http;

namespace OleSync.Application.Boards.Requests
{
    public class UploadBoardFileCommandRequest : IRequest<bool>
    {
        public int BoardId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
