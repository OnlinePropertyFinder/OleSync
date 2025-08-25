using MediatR;
using Microsoft.EntityFrameworkCore;
using OleSync.Application.People.Dtos;
using OleSync.Application.People.Mapping;
using OleSync.Application.People.Requests;
using OleSync.Application.Utilities;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.People.Repositories;
using System.Linq.Expressions;

namespace OleSync.Application.People.Queries
{
    public class GetPaginatedEmployeeQueryHandler : IRequestHandler<GetPaginatedEmployeeQueryRequest, PaginatedResult<EmployeeListDto>>
    {
        private IEmployeeRepository _repository;
        public GetPaginatedEmployeeQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<EmployeeListDto>> Handle(GetPaginatedEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<Employee, bool>> expression = x => true;

            if (!string.IsNullOrWhiteSpace(request.Criteria.FilterText))
                expression = x => x.FullName.Contains(request.Criteria.FilterText) || x.Email.Contains(request.Criteria.FilterText);

            var query = _repository.FilterBy(expression);

            var pagedItems = await query
                .Skip((request.Criteria.PageNumber - 1) * request.Criteria.PageSize)
                .Take(request.Criteria.PageSize)
                .ToListAsync();

            var EmployeeList = pagedItems.Select(b => b.ToListDto()).ToList();

            var paginatedResult = new PaginatedResult<EmployeeListDto>
            {
                Items = EmployeeList,
                TotalCount = query.Count(),
                PageNumber = request.Criteria.PageNumber,
                PageSize = request.Criteria.PageSize
            };

            return paginatedResult;
        }
    }
}
