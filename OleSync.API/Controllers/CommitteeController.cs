using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OleSync.API.Shared;
using OleSync.Application.Committees.Criterias;
using OleSync.Application.Committees.Dtos;
using OleSync.Application.Committees.Requests;
using OleSync.Application.Utilities;
using System.Net;

namespace OleSync.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CommitteeController : ControllerBase
	{
		private readonly IValidator<CreateOrUpdateCommitteeDto> _validator;
		private readonly IMediator _mediator;
		public CommitteeController(IValidator<CreateOrUpdateCommitteeDto> validator, IMediator mediator)
		{
			_validator = validator;
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrUpdateCommitteeDto dto)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(dto);
				if (!validationResult.IsValid)
				{
					var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
					return new WebResponse<int>($"Validation failed: {errorMessages}", HttpStatusCode.BadRequest);
				}

				var request = new CreateCommitteeCommandRequest { Committee = dto };
				var result = await _mediator.Send(request);
				if (result == 0)
					return new WebResponse<int>("Failed to create committee.", HttpStatusCode.InternalServerError);
				return new WebResponse<int>(result);
			}
			catch (Exception ex)
			{
				return new WebResponse<int>($"An error occurred while creating the committee : {ex.Message}", HttpStatusCode.InternalServerError);
			}
		}

		[HttpPut("{id}")]
		public async Task<WebResponse<bool>> Update(int id, [FromBody] CreateOrUpdateCommitteeDto dto)
		{
			try
			{
				if (dto == null || id != dto.Id)
					return new WebResponse<bool>("Invalid committee data.", HttpStatusCode.BadRequest);

				var validationResult = await _validator.ValidateAsync(dto);
				if (!validationResult.IsValid)
				{
					var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
					return new WebResponse<bool>($"Validation failed: {errorMessages}", HttpStatusCode.BadRequest);
				}

				var command = new UpdateCommitteeCommandRequest
				{
					Committee = dto,
					UserId = 1
				};

				var result = await _mediator.Send(command);
				if (!result)
					return new WebResponse<bool>($"Committee with id {id} not found.", HttpStatusCode.NotFound);

				return new WebResponse<bool>(true);
			}
			catch (Exception ex)
			{
				return new WebResponse<bool>($"An error occurred while updating the committee: {ex.Message}", HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("All")]
		[ProducesResponseType(typeof(CommitteeListDto), StatusCodes.Status200OK)]
		public async Task<WebResponse<PaginatedResult<CommitteeListDto>>> GetPaginated([FromBody] GetPaginatedCommitteesCriteria criteria)
		{
			try
			{
				if (criteria == null)
					return new WebResponse<PaginatedResult<CommitteeListDto>>("Invalid criteria provided.", HttpStatusCode.BadRequest);

				var request = new GetPaginatedCommitteesQueryRequest { Criteria = criteria };
				var result = await _mediator.Send(request);
				if (result == null || result.Items == null || !result.Items.Any())
					return new WebResponse<PaginatedResult<CommitteeListDto>>("No committees found for the given criteria.", HttpStatusCode.NotFound);

				return new WebResponse<PaginatedResult<CommitteeListDto>>(result);
			}
			catch (Exception ex)
			{
				return new WebResponse<PaginatedResult<CommitteeListDto>>($"An error occurred while retrieving the committees: {ex.Message}", HttpStatusCode.InternalServerError);
			}
		}

		[HttpDelete("{id}")]
		public async Task<WebResponse<bool>> Delete(int id)
		{
			try
			{
				var command = new DeleteCommitteeCommandRequest { Id = id, UserId = 1 };
				var result = await _mediator.Send(command);
				if (!result)
					return new WebResponse<bool>($"Committee with id {id} not found or already deleted.", HttpStatusCode.NotFound);
				return new WebResponse<bool>(true);
			}
			catch (Exception ex)
			{
				return new WebResponse<bool>($"An error occurred while deleting the committee: {ex.Message}", HttpStatusCode.InternalServerError);
			}
		}

		[HttpGet("{id}")]
		public async Task<WebResponse<CommitteeDetailDto>> GetById(int id)
		{
			try
			{
				if (id == 0)
					return new WebResponse<CommitteeDetailDto>("Invalid id provided.", HttpStatusCode.BadRequest);

				var request = new GetCommitteeByIdQueryRequest { Id = id };
				var result = await _mediator.Send(request);
				if (result == null)
					return new WebResponse<CommitteeDetailDto>($"Committee with id {id} not found.", HttpStatusCode.NotFound);

				return new WebResponse<CommitteeDetailDto>(result);
			}
			catch (Exception ex)
			{
				return new WebResponse<CommitteeDetailDto>($"An error occurred while retrieving the committee: {ex.Message}", HttpStatusCode.InternalServerError);
			}
		}
	}
}

