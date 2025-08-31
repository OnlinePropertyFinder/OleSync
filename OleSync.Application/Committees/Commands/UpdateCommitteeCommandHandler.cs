using MediatR;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Committees.Commands
{
	public class UpdateCommitteeCommandHandler : IRequestHandler<UpdateCommitteeCommandRequest, bool>
	{
		private readonly ICommitteeRepository _repository;
		public UpdateCommitteeCommandHandler(ICommitteeRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(UpdateCommitteeCommandRequest request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);
			var committee = await _repository.GetByIdAsync(request.Committee.Id) ?? throw new InvalidOperationException($"Committee with id {request.Committee.Id} not found.");
			committee.UpdateFromDto(request.Committee, request.UserId);
			await _repository.UpdateAsync(committee);
			return true;
		}
	}
}

