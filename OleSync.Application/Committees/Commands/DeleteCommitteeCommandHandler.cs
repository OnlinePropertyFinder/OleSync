using MediatR;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Committees.Commands
{
	public class DeleteCommitteeCommandHandler : IRequestHandler<DeleteCommitteeCommandRequest, bool>
	{
		private readonly ICommitteeRepository _repository;
		public DeleteCommitteeCommandHandler(ICommitteeRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(DeleteCommitteeCommandRequest request, CancellationToken cancellationToken)
		{
			await _repository.DeleteAsync(request.Id, request.UserId);
			return true;
		}
	}
}

