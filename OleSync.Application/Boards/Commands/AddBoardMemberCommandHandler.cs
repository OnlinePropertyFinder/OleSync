using MediatR;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Commands
{
    public class AddBoardMemberCommandHandler : IRequestHandler<AddBoardMemberCommandRequest, int>
    {
        private readonly IBoardRepository _repository;
        public AddBoardMemberCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddBoardMemberCommandRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Member);

            // Ensure board exists
            var board = await _repository.GetByIdAsync(request.Member.BoardId);
            if (board == null)
                throw new Exception($"Board with id {request.Member.BoardId} not found");

            var member = request.Member.ToDomainEntity();
            await _repository.AddMemberAsync(member);
            return member.Id;
        }
    }
}

