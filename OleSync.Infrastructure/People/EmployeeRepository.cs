using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.People.Repositories;
using OleSync.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace OleSync.Infrastructure.People
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly OleSyncContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(OleSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<Employee> FilterBy(Expression<Func<Employee, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Employee, bool>>>(filter);

            var query = _context.Employees
                .Where(expression)
                .Select(Employee => Employee);

            return query;
        }

        public async Task<IEnumerable<Employee>> FilterByAsync(Expression<Func<Employee, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Employee, bool>>>(filter);

            var Employees = await _context.Employees
                            .Where(expression)
                            .ToListAsync();

            return Employees;
        }

    }
}
