using MediatR;
using OleSync.Application.Committees.Dtos;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Committees.Queries
{
	public class GetCommitteeByIdQueryHandler : IRequestHandler<GetCommitteeByIdQueryRequest, CommitteeDetailDto?>
	{
		private readonly ICommitteeRepository _repository;
		public GetCommitteeByIdQueryHandler(ICommitteeRepository repository)
		{
			_repository = repository;
		}

		public async Task<CommitteeDetailDto?> Handle(GetCommitteeByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var committee = await _repository.GetWithMembersAndMeetingsAsync(request.Id);
			return committee?.ToDetailDto();
		}
	}
}

