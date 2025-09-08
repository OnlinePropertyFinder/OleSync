using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Commands
{
    public class UnlinkCommitteeFromBoardCommandHandler : IRequestHandler<UnlinkCommitteeFromBoardCommandRequest, bool>
    {
        private IBoardCommitteeRepository _repository;
        public UnlinkCommitteeFromBoardCommandHandler(IBoardCommitteeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UnlinkCommitteeFromBoardCommandRequest request, CancellationToken cancellationToken)
        {
            var boardCommittee = await _repository.GetByBoardAndCommitteeAsync(request.BoardId, request.CommitteeId);
            if (boardCommittee == null)
                return true;//throw new ArgumentException("Board-Committee association not found");

            await _repository.DeleteAsync(request.BoardId, request.CommitteeId);
            return true;
        }
    }
}
