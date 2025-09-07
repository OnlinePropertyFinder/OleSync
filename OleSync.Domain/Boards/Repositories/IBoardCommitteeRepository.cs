using OleSync.Domain.Boards.Core.Entities;

namespace OleSync.Domain.Boards.Repositories
{
    public interface IBoardCommitteeRepository
    {
        Task<BoardCommittee?> GetByBoardAndCommitteeAsync(int boardId, int committeeId);
        Task<bool> ExistsAsync(int boardId, int committeeId);
        Task DeleteAsync(int boardId, int committeeId);
    }
}