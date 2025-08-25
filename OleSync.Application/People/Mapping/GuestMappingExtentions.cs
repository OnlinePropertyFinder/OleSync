using OleSync.Application.People.Dtos;
using OleSync.Domain.People.Core.Entities;

namespace OleSync.Application.People.Mapping
{
    public static class GuestMappingExtentions
    {
        public static GuestListDto ToListDto(this Guest guest)
        {
            return new GuestListDto
            {
                Id = guest.Id,
                FullName = guest.FullName,
                Email = guest.Email,
                Phone = guest.Phone,
                Position = guest.Position
            };
        }
    }
}
