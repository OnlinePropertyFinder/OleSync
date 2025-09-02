using MediatR;
using Microsoft.EntityFrameworkCore;
using OleSync.Application.Committees.Dtos;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Application.Utilities;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Repositories;
using System.Linq.Expressions;

namespace OleSync.Application.Committees.Queries
{
	public class GetPaginatedCommitteesQueryHandler : IRequestHandler<GetPaginatedCommitteesQueryRequest, PaginatedResult<CommitteeListDto>>
	{
		private readonly ICommitteeRepository _repository;
		public GetPaginatedCommitteesQueryHandler(ICommitteeRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedResult<CommitteeListDto>> Handle(GetPaginatedCommitteesQueryRequest request, CancellationToken cancellationToken)
		{
			Expression<Func<Committee, bool>> expression = x => !x.Audit.IsDeleted;
			IQueryable<Committee> query = _repository.FilterBy(expression);

			if (!string.IsNullOrWhiteSpace(request.Criteria.FilterText))
				query = query.Where(x => x.Name.Contains(request.Criteria.FilterText));

			if (request.Criteria.Status != null)
				query = query.Where(x => x.Status == request.Criteria.Status);

			if (request.Criteria.CommitteeType != null)
				query = query.Where(x => x.CommitteeType == request.Criteria.CommitteeType);

			var totalCount = await query.CountAsync(cancellationToken);

			var pagedItems = await query
				.Skip((request.Criteria.PageNumber - 1) * request.Criteria.PageSize)
				.Take(request.Criteria.PageSize)
				.ToListAsync(cancellationToken);

			var list = pagedItems.Select(c => c.ToListDto()).ToList();

			var result = new PaginatedResult<CommitteeListDto>
			{
				Items = list,
				TotalCount = totalCount,
				PageNumber = request.Criteria.PageNumber,
				PageSize = request.Criteria.PageSize
			};

			return result;
		}
	}
}

