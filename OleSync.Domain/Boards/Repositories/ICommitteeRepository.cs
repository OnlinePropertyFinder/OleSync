using OleSync.Domain.Boards.Core.Entities;
using System.Linq.Expressions;

namespace OleSync.Domain.Boards.Repositories
{
	public interface ICommitteeRepository
	{
		Task<Committee?> GetByIdAsync(int id);
		Task<Committee?> GetByIdNotTrackedAsync(int id);
		Task AddAsync(Committee committee);
		Task UpdateAsync(Committee committee);
		Task AddMemberAsync(CommitteeMember member);
		Task AddMeetingAsync(CommitteeMeeting meeting);
		Task<Committee?> GetWithMembersAndMeetingsAsync(int id);
		Task<IEnumerable<Committee>> FilterByAsync(Expression<Func<Committee, bool>> filter);
		IQueryable<Committee> FilterBy(Expression<Func<Committee, bool>> filter);
		Task DeleteAsync(int id, long userId);
		Task<IEnumerable<Committee>> GetUnLinkedCommitteesAsync();

    }
}