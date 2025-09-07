using OleSync.Application.Boards.Dtos;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Application.Utilities;
using OleSync.Domain.Shared.Enums;
using OleSync.Application.Committees.Dtos;

namespace OleSync.Application.Boards.Mapping
{
    public static class BoardMappingExtentions
    {
        public static Board ToDomainEntity(this CreateOrUpdateBoardDto boardDto)
        {
            ArgumentNullException.ThrowIfNull(boardDto);

            var auditInfoDto = boardDto.Audit?.ToValueObjectOnCreate() ?? AuditInfo.CreateEmpty();

            return Board.Create(
                boardDto.Name,
                boardDto.Purpose,
                boardDto.BoardType,
                boardDto.StartDate,
                boardDto.EndDate,
                boardDto.Status,
                auditInfoDto
            );
        }

        public static void UpdateFromDto(this Board board, CreateOrUpdateBoardDto boardDto, long modifiedBy)
        {
            ArgumentNullException.ThrowIfNull(board);

            ArgumentNullException.ThrowIfNull(boardDto);

            board.Update(
                boardDto.Name,
                boardDto.Purpose,
                boardDto.BoardType,
                boardDto.StartDate,
                boardDto.EndDate,
                boardDto.Status,
                modifiedBy
            );
        }

        public static BoardListDto ToListDto(this Board board)
        {
            return new BoardListDto
            {
                Id = board.Id,
                Name = board.Name,
                Purpose = board.Purpose,
                BoardType = board.BoardType,
                StartDate = board.StartDate,
                EndDate = board.EndDate,
                Status = board.Status,
                StatusDescription = board.Status.GetDescription(),
                CreatedBy = board.Audit.CreatedBy,
                CreatedAt = board.Audit.CreatedAt,
                ModifiedBy = board.Audit.ModifiedBy,
                ModifiedAt = board.Audit.ModifiedAt,
                DeletedBy = board.Audit.DeletedBy,
                DeletedAt = board.Audit.DeletedAt
            };
        }

        public static BoardMember ToDomainEntity(this AddBoardMemberDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var auditInfoDto = dto.Audit?.ToValueObjectOnCreate() ?? AuditInfo.CreateEmpty();
            return BoardMember.Create(
                dto.BoardId,
                dto.MemberType,
                dto.EmployeeId,
                dto.GuestId,
                auditInfoDto
            );
        }

        public static BoardMemberListDto ToListDto(this BoardMember member)
        {
            var fullName = member.Employee != null ? member.Employee.FullName : member.Guest != null ? member.Guest.FullName : string.Empty;
            var email = member.Employee != null ? member.Employee.Email : member.Guest != null ? member.Guest.Email : null;
            var phone = member.Employee != null ? member.Employee.Phone : member.Guest != null ? member.Guest.Phone : null;
            var position = member.Employee != null ? member.Employee.Position : member.Guest != null ? member.Guest.Position : null;
            var role = member.Employee != null ? (MemberRole?)member.Employee.Role : member.Guest != null ? (MemberRole?)member.Guest.Role : null;
            var memberType = member.Employee != null ? member.Employee.MemberType : member.Guest != null ? member.Guest.MemberType : member.MemberType;

            return new BoardMemberListDto
            {
                Id = member.Id,
                MemberType = memberType,
                EmployeeId = member.EmployeeId,
                GuestId = member.GuestId,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                Role = role,
                CreatedBy = member.Audit.CreatedBy,
                CreatedAt = member.Audit.CreatedAt,
                ModifiedBy = member.Audit.ModifiedBy,
                ModifiedAt = member.Audit.ModifiedAt
            };
        }

        public static BoardDetailDto ToDetailDto(this Board board)
        {
            return new BoardDetailDto
            {
                Id = board.Id,
                Name = board.Name,
                Purpose = board.Purpose,
                BoardType = board.BoardType,
                StartDate = board.StartDate,
                EndDate = board.EndDate,
                Status = board.Status,
                CreatedBy = board.Audit.CreatedBy,
                CreatedAt = board.Audit.CreatedAt,
                ModifiedBy = board.Audit.ModifiedBy,
                ModifiedAt = board.Audit.ModifiedAt,
                DeletedBy = board.Audit.DeletedBy,
                DeletedAt = board.Audit.DeletedAt,
                Members = board.Members?.Select(m => m.ToListDto()).ToList() ?? new List<BoardMemberListDto>()
            };
        }

        public static BoardLookupDto ToLookupDto(this Board board)
        {
            return new BoardLookupDto
            {
                Id = board.Id,
                Name = board.Name
            };
        }
    }
}
