using MediatR;
using OleSync.Application.People.Criterias;
using OleSync.Application.People.Dtos;
using OleSync.Application.Utilities;

namespace OleSync.Application.People.Requests
{
    public class GetPaginatedEmployeeQueryRequest : IRequest<PaginatedResult<EmployeeListDto>>
    {
        public required GetPaginatedEmployeesCriteria Criteria { get; set; }
    }
}
