using MediatR;
using OleSync.Application.Boards.Requests;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.Shared.Services;

namespace OleSync.Application.Boards.Commands
{
    public class UploadBoardFileCommandHandler : IRequestHandler<UploadBoardFileCommandRequest, bool>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IFileService _fileService;

        public UploadBoardFileCommandHandler(
            IBoardRepository boardRepository,
            IFileService fileService)
        {
            _boardRepository = boardRepository;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UploadBoardFileCommandRequest request, CancellationToken cancellationToken)
        {
            // Validate file
            if (request.File == null || request.File.Length == 0)
                ArgumentNullException.ThrowIfNull(request.File);

            // Validate file type
            var allowedExtensions = new[] { ".pdf" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Only PDF files are allowed");

            // Check file size (e.g., 10MB limit)
            if (request.File.Length > 10 * 1024 * 1024)
                throw new ArgumentException("File size cannot exceed 10MB");

            // Get board
            var board = await _boardRepository.GetByIdAsync(request.BoardId) ?? throw new Exception("Board not found");
            try
            {
                // Delete old file if exists
                if (!string.IsNullOrEmpty(board?.DocumentUrl))
                {
                    _fileService.DeleteFileAsync(board.DocumentUrl);
                }

                // Save new file
                var filePath = await _fileService.SaveFileAsync(request.File, "boards");

                // Update board with file information
                board.UploadFile(filePath);
                await _boardRepository.UpdateAsync(board);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
