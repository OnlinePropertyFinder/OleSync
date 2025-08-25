using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class GuestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GuestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("All")]
        public async Task<WebResponse<PaginatedResult<GuestListDto>>> GetPaginatedGuest(GetPaginatedGuestsCriteria criteria)
        {
            try
            {
                if (criteria == null)
                {
                    return new WebResponse<PaginatedResult<GuestListDto>>("Invalid criteria provided.", HttpStatusCode.BadRequest);
                }

                var request = new GetPaginatedGuestQueryRequest { Criteria = criteria };
                var result = await _mediator.Send(request);
                if (result == null || result.Items == null || !result.Items.Any())
                {
                    return new WebResponse<PaginatedResult<GuestListDto>>("No Guests found for the given criteria.", HttpStatusCode.NotFound);
                }

                return new WebResponse<PaginatedResult<GuestListDto>>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<PaginatedResult<GuestListDto>>($"An error occurred while retrieving the Guests: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
