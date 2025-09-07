using MediatR;

namespace OleSync.Application.Boards.Requests
{
    public class UnlinkCommitteeFromBoardCommandRequest : IRequest<bool>
    {
        public int BoardId { get; set; }
        public int CommitteeId { get; set; }
    }
}
