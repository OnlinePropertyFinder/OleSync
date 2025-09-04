using MediatR;
using OleSync.Application.Committees.Dtos;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;

namespace OleSync.Application.Committees.Queries
{
    public class GetCommitteesLookupQueryHandler : IRequestHandler<GetCommitteesLookupQueryRequest, List<CommitteLookupDto>>
    {
        private readonly ICommitteeRepository _repository;
        public GetCommitteesLookupQueryHandler(ICommitteeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommitteLookupDto>> Handle(GetCommitteesLookupQueryRequest request, CancellationToken cancellationToken)
        {
            var committees = await _repository.GetUnLinkedCommitteesAsync();
            var list = committees.Select(c => c.ToLookupDto()).ToList();
            return list;
        }
    }
}
