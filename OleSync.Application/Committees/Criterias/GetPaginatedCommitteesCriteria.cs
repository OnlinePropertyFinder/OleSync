using OleSync.Domain.Shared.Enums;
using System.ComponentModel;

namespace OleSync.Application.Committees.Criterias
{
	public class GetPaginatedCommitteesCriteria
	{
		[DefaultValue(1)]
		public int PageNumber { get; set; }

		[DefaultValue(10)]
		public int PageSize { get; set; }

		[DefaultValue(null)]
		public CommitteeType? CommitteeType { get; set; }

		[DefaultValue(null)]
		public Status? Status { get; set; }

		[DefaultValue(null)]
		public string? FilterText { get; set; }
	}
}

