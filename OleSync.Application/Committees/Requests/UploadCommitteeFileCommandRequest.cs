using MediatR;
using Microsoft.AspNetCore.Http;

namespace OleSync.Application.Committees.Requests
{
    public class UploadCommitteeFileCommandRequest : IRequest<bool>
    {
        public int CommitteeId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
