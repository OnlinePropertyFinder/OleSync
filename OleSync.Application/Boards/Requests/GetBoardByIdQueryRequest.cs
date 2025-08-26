using MediatR;
using OleSync.Application.Boards.Dtos;

namespace OleSync.Application.Boards.Requests
{
	public class GetBoardByIdQueryRequest : IRequest<BoardDetailDto>
	{
		public int Id { get; set; }
	}
}
