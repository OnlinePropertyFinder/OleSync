using MediatR;
using OleSync.Application.Boards.Dtos;
using OleSync.Application.Boards.Requests;
using OleSync.Application.Utilities;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OleSync.Application.Boards.Mapping;

namespace OleSync.Application.Boards.Queries
{
    public class GetPaginatedBoardQueryHandler : IRequestHandler<GetPaginatedBoardQueryRequest, PaginatedResult<BoardListDto>>
    {
        private IBoardRepository _repository;
        public GetPaginatedBoardQueryHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<BoardListDto>> Handle(GetPaginatedBoardQueryRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<Board, bool>> expression = x => true;
            IQueryable<Board> query = _repository.FilterBy(expression);

            if (!string.IsNullOrWhiteSpace(request.Criteria.FilterText))
                query = query.Where(x => x.Name.Contains(request.Criteria.FilterText));

            if (request.Criteria.Status != null)
                query = query.Where(x => x.Status == request.Criteria.Status);

            if (request.Criteria.BoardType != null)
                query = query.Where(x => x.BoardType == request.Criteria.BoardType);

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedItems = await query
                .Skip((request.Criteria.PageNumber - 1) * request.Criteria.PageSize)
                .Take(request.Criteria.PageSize)
                .ToListAsync(cancellationToken);

            var boardList = pagedItems.Select(b => b.ToListDto()).ToList();

            var paginatedResult = new PaginatedResult<BoardListDto>
            {
                Items = boardList,
                TotalCount = totalCount,
                PageNumber = request.Criteria.PageNumber,
                PageSize = request.Criteria.PageSize
            };

            return paginatedResult;
        }
    }
}
