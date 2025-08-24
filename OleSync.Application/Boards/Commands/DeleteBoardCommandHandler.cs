using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OleSync.Application.Boards.Commands
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommandRequest, bool>
    {
        private IBoardRepository _repository;
        public DeleteBoardCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBoardCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, request.UserId);
            return true;
        }
    }
}
