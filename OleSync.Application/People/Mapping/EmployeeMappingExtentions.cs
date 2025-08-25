using OleSync.Application.Boards.Dtos;
using OleSync.Application.People.Dtos;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.People.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OleSync.Application.People.Mapping
{
    public static class EmployeeMappingExtentions
    {
        public static EmployeeListDto ToListDto(this Employee employee)
        {
            return new EmployeeListDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                Phone = employee.Phone,
                Position = employee.Position
            };
        }
    }
}
