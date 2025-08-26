using OleSync.Domain.Shared.Enums;
using System.ComponentModel;

namespace OleSync.Application.Boards.Criterias
{
    public class GetPaginatedBoardsCriteria
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; }
        
        [DefaultValue(10)]
        public int PageSize { get; set; }

        [DefaultValue(null)]
        public BoardType? BoardType { get; set; }

        [DefaultValue(null)]
        public Status? Status { get; set; }
        
        [DefaultValue(null)]
        public string? FilterText { get; set; }
    }
}
