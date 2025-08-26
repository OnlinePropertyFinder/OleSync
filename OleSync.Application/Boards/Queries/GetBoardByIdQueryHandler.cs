using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Application.Boards.Dtos;
using OleSync.Application.Boards.Mapping;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Boards.Queries
{
	public class GetBoardByIdQueryHandler : IRequestHandler<GetBoardByIdQueryRequest, BoardDetailDto>
	{
		private readonly IBoardRepository _repository;
		public GetBoardByIdQueryHandler(IBoardRepository repository)
		{
			_repository = repository;
		}

		public async Task<BoardDetailDto> Handle(GetBoardByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var board = await _repository.GetWithMembersAsync(request.Id);
			return board?.ToDetailDto();
		}
	}
}
