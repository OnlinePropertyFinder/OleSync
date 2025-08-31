using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;
using OleSync.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace OleSync.Infrastructure.Boards
{
	public class CommitteeRepository : ICommitteeRepository
	{
		private readonly OleSyncContext _context;
		private readonly IMapper _mapper;
		public CommitteeRepository(OleSyncContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task AddAsync(Committee committee)
		{
			ArgumentNullException.ThrowIfNull(committee);
			_context.Committees.Add(committee);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id, long userId)
		{
			var committee = await _context.Committees.FindAsync(id) ?? throw new Exception("Committee not found");
			committee.MarkAsDeleted(userId);
			_context.Committees.Update(committee);
			await _context.SaveChangesAsync();
		}

		public IQueryable<Committee> FilterBy(Expression<Func<Committee, bool>> filter)
		{
			var expression = _mapper.Map<Expression<Func<Committee, bool>>>(filter);
			var query = _context.Committees
				.Where(expression)
				.Select(c => c);
			return query;
		}

		public async Task<IEnumerable<Committee>> FilterByAsync(Expression<Func<Committee, bool>> filter)
		{
			var expression = _mapper.Map<Expression<Func<Committee, bool>>>(filter);
			var committees = await _context.Committees
				.Where(expression)
				.ToListAsync();
			return committees;
		}

		public async Task<Committee?> GetByIdAsync(int id)
		{
			var committee = await _context.Committees.FindAsync(id);
			if (committee == null)
				return null;
			return committee;
		}

		public async Task<Committee?> GetByIdNotTrackedAsync(int id)
		{
			var committee = await _context.Committees
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
			if (committee == null)
				return null;
			return committee;
		}

		public async Task UpdateAsync(Committee committee)
		{
			ArgumentNullException.ThrowIfNull(committee);
			_context.Committees.Update(committee);
			await _context.SaveChangesAsync();
		}
	}
}

