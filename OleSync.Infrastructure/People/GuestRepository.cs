using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.People.Repositories;
using OleSync.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace OleSync.Infrastructure.People
{
    public class GuestRepository : IGuestRepository
    {
        private readonly OleSyncContext _context;
        private readonly IMapper _mapper;
        public GuestRepository(OleSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<Guest> FilterBy(Expression<Func<Guest, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Guest, bool>>>(filter);

            var query = _context.Guests
                .Where(expression)
                .Select(Guest => Guest);

            return query;
        }

        public async Task<IEnumerable<Guest>> FilterByAsync(Expression<Func<Guest, bool>> filter)
        {
            var expression = _mapper.Map<Expression<Func<Guest, bool>>>(filter);

            var Guests = await _context.Guests
                            .Where(expression)
                            .ToListAsync();

            return Guests;
        }

        public async Task AddAsync(Guest guest)
        {
            ArgumentNullException.ThrowIfNull(guest);
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }
    }
}
