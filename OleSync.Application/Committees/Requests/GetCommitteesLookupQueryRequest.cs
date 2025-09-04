using MediatR;
using OleSync.Application.Committees.Dtos;

namespace OleSync.Application.Committees.Requests
{
    public class GetCommitteesLookupQueryRequest : IRequest<List<CommitteLookupDto>> { }
}
