using MediatR;
using OleSync.Application.Boards.Dtos;

namespace OleSync.Application.Boards.Requests
{
    public class AddBoardMemberCommandRequest : IRequest<int>
    {
        public required AddBoardMemberDto Member { get; set; }
    }
}

