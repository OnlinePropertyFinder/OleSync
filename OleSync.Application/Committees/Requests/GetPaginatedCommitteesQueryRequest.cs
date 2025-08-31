using MediatR;
using OleSync.Application.Committees.Criterias;
using OleSync.Application.Committees.Dtos;
using OleSync.Application.Utilities;

namespace OleSync.Application.Committees.Requests
{
	public class GetPaginatedCommitteesQueryRequest : IRequest<PaginatedResult<CommitteeListDto>>
	{
		public required GetPaginatedCommitteesCriteria Criteria { get; set; }
	}
}

