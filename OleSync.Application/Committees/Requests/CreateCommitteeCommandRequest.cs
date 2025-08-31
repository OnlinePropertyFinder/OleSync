using MediatR;
using OleSync.Application.Committees.Dtos;

namespace OleSync.Application.Committees.Requests
{
	public class CreateCommitteeCommandRequest : IRequest<int>
	{
		public required CreateOrUpdateCommitteeDto Committee { get; set; }
	}
}

