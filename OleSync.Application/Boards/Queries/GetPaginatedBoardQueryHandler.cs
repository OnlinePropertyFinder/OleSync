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

            if (!string.IsNullOrWhiteSpace(request.Criteria.FilterText))
                expression = x => x.Name.Contains(request.Criteria.FilterText) || x.Purpose.Contains(request.Criteria.FilterText);

            var query = _repository.FilterBy(expression);

            var pagedItems = await query
                .Skip((request.Criteria.PageNumber - 1) * request.Criteria.PageSize)
                .Take(request.Criteria.PageSize)
                .ToListAsync();

            var boardList = pagedItems.Select(b => b.ToListDto()).ToList();

            var paginatedResult = new PaginatedResult<BoardListDto>
            {
                Items = boardList,
                TotalCount = query.Count(),
                PageNumber = request.Criteria.PageNumber,
                PageSize = request.Criteria.PageSize
            };

            return paginatedResult;
        }
    }
}
