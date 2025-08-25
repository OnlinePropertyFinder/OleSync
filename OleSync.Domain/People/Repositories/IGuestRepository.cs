using OleSync.Domain.People.Core.Entities;
using System.Linq.Expressions;

namespace OleSync.Domain.People.Repositories
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> FilterByAsync(Expression<Func<Guest, bool>> filter);
        IQueryable<Guest> FilterBy(Expression<Func<Guest, bool>> filter);
    }
}
