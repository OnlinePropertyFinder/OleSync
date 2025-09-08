using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class BoardCommittee
    {
        public int BoardId { get; private set; }
        public int CommitteeId { get; private set; }

        [JsonIgnore]
        public Board Board { get; private set; } = null!;
        public Committee Committee { get; private set; } = null!;

        public static BoardCommittee Create(
            int boardId,
            int committeeId)
        {
            if (boardId == 0)
                throw new ArgumentException("Board id can not be 0.");

            if (committeeId == 0)
                throw new ArgumentException("Committee id can not be 0.");

            return new BoardCommittee
            {
                BoardId = boardId,
                CommitteeId = committeeId
            };
        }
    }
}
