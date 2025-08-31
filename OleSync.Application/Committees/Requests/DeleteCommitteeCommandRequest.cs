using MediatR;

namespace OleSync.Application.Committees.Requests
{
	public class DeleteCommitteeCommandRequest : IRequest<bool>
	{
		public int Id { get; set; }
		public long UserId { get; set; }
	}
}

