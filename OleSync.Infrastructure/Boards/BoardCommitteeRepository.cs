using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;
using OleSync.Infrastructure.Persistence.Context;

namespace OleSync.Infrastructure.Boards
{
    public class BoardCommitteeRepository : IBoardCommitteeRepository
    {
        private readonly OleSyncContext _context;
        private readonly IMapper _mapper;
        public BoardCommitteeRepository(OleSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BoardCommittee?> GetByBoardAndCommitteeAsync(int boardId, int committeeId)
        {
            return await _context.BoardCommittees
                .FirstOrDefaultAsync(bc => bc.BoardId == boardId && bc.CommitteeId == committeeId);
        }

        public async Task<bool> ExistsAsync(int boardId, int committeeId)
        {
            return await _context.BoardCommittees
                .AnyAsync(bc => bc.BoardId == boardId && bc.CommitteeId == committeeId);
        }

        public async Task DeleteAsync(int boardId, int committeeId)
        {
            var boardCommittee = await _context.BoardCommittees
                .FirstOrDefaultAsync(bc => bc.BoardId == boardId && bc.CommitteeId == committeeId) ?? throw new Exception("Board-Committee association not found");

            _context.BoardCommittees.Remove(boardCommittee);
            await _context.SaveChangesAsync();
        }
    }
}
