using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
	public class CreateCommitteeMeetingDto
	{
		public int? Id;
		public string Name { get; set; } = null!;
		public MeetingType MeetingType { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public string? Address { get; set; }
	}
}

