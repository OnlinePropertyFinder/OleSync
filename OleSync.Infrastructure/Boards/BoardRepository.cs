using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;
using OleSync.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace OleSync.Infrastructure.Boards
{
    public class BoardRepository : IBoardRepository
    {
        private readonly OleSyncContext _context;
        private readonly IMapper _mapper;
        public BoardRepository(OleSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Board board)
        {
            ArgumentNullException.ThrowIfNull(board);

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, long userId)
        {
            var board = await _context.Boards.FindAsync(id) ?? throw new Exception("Board not found");

            board.MarkAsDeleted(userId);
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Board> FilterBy(Expression<Func<Board, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Board, bool>>>(filter);

            var query = _context.Boards
                .Where(expression)
                .Select(board => board);

            return query;
        }

        public async Task<IEnumerable<Board>> FilterByAsync(Expression<Func<Board, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Board, bool>>>(filter);

            var boards = await _context.Boards
                            .Where(expression)
                            .ToListAsync();

            return boards;
        }

        public async Task<Board?> GetByIdAsync(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
                return null;

            return board;
        }

        public async Task<Board?> GetByIdNotTrackedAsync(int id)
        {
            var board = await _context.Boards
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => e.Id == id);

            if (board == null) 
                return null;

            return board;
        }

        public async Task UpdateAsync(Board board)
        {
            ArgumentNullException.ThrowIfNull(board);

            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

        public async Task<Board?> GetWithMembersAndCommitteesAsync(int id)
        {
            var board = await _context.Boards
                .Include(b => b.Members)
                .ThenInclude(m => m.Employee)
                .Include(b => b.Members)
                .ThenInclude(m => m.Guest)
                .Include(b => b.BoardCommittees)
                .ThenInclude(c => c.Committee)
                .FirstOrDefaultAsync(b => b.Id == id);
            return board;
        }

        public async Task AddMemberAsync(BoardMember member)
        {
            ArgumentNullException.ThrowIfNull(member);

            _context.BoardMembers.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task AddBoardCommitteeAsync(BoardCommittee committee)
        {
            ArgumentNullException.ThrowIfNull(committee);

            _context.BoardCommittees.Add(committee);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteMemberAsync(int memberId, long userId)
        {
            var member = await _context.BoardMembers.FirstOrDefaultAsync(m => m.Id == memberId);
            if (member == null)
                return;
            member.MarkAsDeleted(userId);
            _context.BoardMembers.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
