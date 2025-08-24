using OleSync.Domain.Boards.Core.Entities;
using System.Linq.Expressions;

namespace OleSync.Domain.Boards.Repositories
{
    public interface IBoardRepository
    {
        Task<Board?> GetByIdAsync(int id);
        Task<Board?> GetByIdNotTrackedAsync(int id);
        Task AddAsync(Board board);
        Task UpdateAsync(Board board);
        Task<IEnumerable<Board>> FilterByAsync(Expression<Func<Board, bool>> filter);
        IQueryable<Board> FilterBy(Expression<Func<Board, bool>> filter);
        Task DeleteAsync(int id, long userId);
    }
}
