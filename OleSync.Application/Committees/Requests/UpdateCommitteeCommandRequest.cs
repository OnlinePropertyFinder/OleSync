using MediatR;
using OleSync.Application.Committees.Dtos;

namespace OleSync.Application.Committees.Requests
{
	public class UpdateCommitteeCommandRequest : IRequest<bool>
	{
		public required CreateOrUpdateCommitteeDto Committee { get; set; }
		public long UserId { get; set; }
	}
}

