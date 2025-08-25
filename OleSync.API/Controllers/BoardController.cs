using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OleSync.API.Shared;
using OleSync.Application.Boards.Criterias;
using OleSync.Application.Boards.Dtos;
using OleSync.Application.Boards.Requests;
using OleSync.Application.Utilities;
using OleSync.Domain.Boards.Core.Entities;
using System.Net;

namespace OleSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly IValidator<CreateOrUpdateBoardDto> _validator;
        private readonly IMediator _mediator;
        private readonly IValidator<AddBoardMemberDto> _memberValidator;
        public BoardController(IValidator<CreateOrUpdateBoardDto> validator, IValidator<AddBoardMemberDto> memberValidator, IMediator mediator)
        {
            _validator = validator;
            _memberValidator = memberValidator;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateBoardDto boardDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(boardDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new WebResponse<int>($"Validation failed: {errorMessages}", HttpStatusCode.BadRequest);
                }

                var request = new CreateBoardCommandRequest { Board = boardDto };
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return new WebResponse<int>("Failed to create board.", HttpStatusCode.InternalServerError);
                }

                return new WebResponse<int>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<int>($"An error occurred while creating the board : {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut("{id}")]
        public async Task<WebResponse<bool>> Update(int id, [FromBody] CreateOrUpdateBoardDto boardDto)
        {
            try
            {
                if (boardDto == null || id != boardDto.Id)
                    return new WebResponse<bool>("Invalid board data.", HttpStatusCode.BadRequest);

                var validationResult = await _validator.ValidateAsync(boardDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new WebResponse<bool>($"Validation failed: {errorMessages}", HttpStatusCode.BadRequest);
                }

                var command = new UpdateBoardCommandRequest
                {
                    Board = boardDto
                };

                var result = await _mediator.Send(command);
                if (!result)
                    return new WebResponse<bool>($"Board with id {id} not found.", HttpStatusCode.NotFound);

                return new WebResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new WebResponse<bool>($"An error occurred while updating the board: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("All")]
        public async Task<WebResponse<PaginatedResult<BoardListDto>>> GetPagenatedBoard(GetPaginatedBoardsCriteria criteria)
        {
            try
            {
                if (criteria == null)
                {
                    return new WebResponse<PaginatedResult<BoardListDto>>("Invalid criteria provided.", HttpStatusCode.BadRequest);
                }

                var request = new GetPaginatedBoardQueryRequest { Criteria = criteria };
                var result = await _mediator.Send(request);
                if (result == null || result.Items == null || !result.Items.Any())
                {
                    return new WebResponse<PaginatedResult<BoardListDto>>("No boards found for the given criteria.", HttpStatusCode.NotFound);
                }

                return new WebResponse<PaginatedResult<BoardListDto>>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<PaginatedResult<BoardListDto>>($"An error occurred while retrieving the boards: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<WebResponse<bool>> DeleteBoard(int id)
        {
            try
            {
                var command = new DeleteBoardCommandRequest
                {
                    Id = id,
                    UserId = 1,
                };

                var result = await _mediator.Send(command);
                if (!result)
                {
                    return new WebResponse<bool>("Board with id " + id + " not found or already deleted.", HttpStatusCode.NotFound);
                }

                return new WebResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new WebResponse<bool>($"An error occurred while deleting the board: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<WebResponse<Board>> GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new WebResponse<Board>("Invalid id provided.", HttpStatusCode.BadRequest);
                }

                var request = new GetBoardByIdQueryRequest { Id = id };
                var result = await _mediator.Send(request);

                if (result == null)
                {
                    return new WebResponse<Board>($"Board with id {id} not found.", HttpStatusCode.NotFound);
                }

                return new WebResponse<Board>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<Board>($"An error occurred while retrieving the board: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{boardId}/members")]
        public async Task<IActionResult> AddMember(int boardId, [FromBody] AddBoardMemberDto memberDto)
        {
            try
            {
                if (memberDto == null)
                {
                    return new WebResponse<int>("Invalid member data.", HttpStatusCode.BadRequest);
                }

                memberDto.BoardId = boardId;
                var validationResult = await _memberValidator.ValidateAsync(memberDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new WebResponse<int>($"Validation failed: {errorMessages}", HttpStatusCode.BadRequest);
                }

                var request = new AddBoardMemberCommandRequest { Member = memberDto };
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return new WebResponse<int>("Failed to add member.", HttpStatusCode.InternalServerError);
                }

                return new WebResponse<int>(result);
            }
            catch (Exception ex)
            {
                return new WebResponse<int>($"An error occurred while adding the member : {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}