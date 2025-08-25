using MediatR;
using Microsoft.AspNetCore.Mvc;
using OleSync.API.Shared;
using OleSync.Application.People.Criterias;
using OleSync.Application.People.Dtos;
using OleSync.Application.People.Requests;
using OleSync.Application.Utilities;
using System.Net;

namespace OleSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("All")]
        public async Task<WebResponse<PaginatedResult<EmployeeListDto>>> GetPaginatedEmployee(GetPaginatedEmployeesCriteria criteria)
        {
            try
            {
                if (criteria == null)
                {
                    return new WebResponse<PaginatedResult<EmployeeListDto>>("Invalid criteria provided.", HttpStatusCode.BadRequest);
                }

                var request = new GetPaginatedEmployeeQueryRequest { Criteria = criteria };
                var result = await _mediator.Send(request);
                if (result == null || result.Items == null || !result.Items.Any())
                {
                    return new WebResponse<PaginatedResult<EmployeeListDto>>("No Employees found for the given criteria.", HttpStatusCode.NotFound);
                }

                return new WebResponse<PaginatedResult<EmployeeListDto>>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<PaginatedResult<EmployeeListDto>>($"An error occurred while retrieving the Employees: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}