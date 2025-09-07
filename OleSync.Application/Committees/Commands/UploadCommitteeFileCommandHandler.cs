using MediatR;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.Shared.Services;

namespace OleSync.Application.Committees.Commands
{
    public class UploadCommitteeFileCommandHandler : IRequestHandler<UploadCommitteeFileCommandRequest, bool>
    {
        private readonly ICommitteeRepository _committeeRepository;
        private readonly IFileService _fileService;

        public UploadCommitteeFileCommandHandler(
            ICommitteeRepository committeeRepository,
            IFileService fileService)
        {
            _committeeRepository = committeeRepository;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UploadCommitteeFileCommandRequest request, CancellationToken cancellationToken)
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

            // Get committee
            var committee = await _committeeRepository.GetByIdAsync(request.CommitteeId) ?? throw new Exception("Committee not found");
            try
            {
                // Delete old file if exists
                if (!string.IsNullOrEmpty(committee?.DocumentUrl))
                {
                    _fileService.DeleteFileAsync(committee.DocumentUrl);
                }

                // Save new file
                var filePath = await _fileService.SaveFileAsync(request.File, "committees");

                // Update committee with file information
                committee.UploadFile(filePath);
                await _committeeRepository.UpdateAsync(committee);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
