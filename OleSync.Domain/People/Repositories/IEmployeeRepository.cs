using OleSync.Domain.People.Core.Entities;
using System.Linq.Expressions;

namespace OleSync.Domain.People.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> FilterByAsync(Expression<Func<Employee, bool>> filter);
        IQueryable<Employee> FilterBy(Expression<Func<Employee, bool>> filter);
    }
}
