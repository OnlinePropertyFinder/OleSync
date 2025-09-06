using MediatR;
using OleSync.Application.Committees.Dtos;

namespace OleSync.Application.Committees.Requests
{
	public class GetCommitteeByIdQueryRequest : IRequest<CommitteeDetailDto?>
	{
		public int Id { get; set; }
	}
}