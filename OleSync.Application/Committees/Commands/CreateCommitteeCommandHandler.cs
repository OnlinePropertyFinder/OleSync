using MediatR;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Committees.Commands
{
	public class CreateCommitteeCommandHandler : IRequestHandler<CreateCommitteeCommandRequest, int>
	{
		private readonly ICommitteeRepository _repository;
		public CreateCommitteeCommandHandler(ICommitteeRepository repository)
		{
			_repository = repository;
		}

		public async Task<int> Handle(CreateCommitteeCommandRequest request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);
			var committee = request.Committee.ToDomainEntity();
			await _repository.AddAsync(committee);
			return committee.Id;
		}
	}
}

