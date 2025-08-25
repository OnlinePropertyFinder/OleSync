using MediatR;
using OleSync.Application.People.Criterias;
using OleSync.Application.People.Dtos;
using OleSync.Application.Utilities;

namespace OleSync.Application.People.Requests
{
    public class GetPaginatedGuestQueryRequest : IRequest<PaginatedResult<GuestListDto>>
    {
        public required GetPaginatedGuestsCriteria Criteria { get; set; }
    }
}
