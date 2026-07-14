using ExtractAPI.Extraction;
using ExtractAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExtractAPI.Services
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ITextExtractor _pdfExtractor;
        private readonly ISummaryService _summaryService;
        private readonly IDocumentRepository _documentRepository;

        public DocumentServices(
            IWebHostEnvironment environment,
            ITextExtractor pdfExtractor,
            ISummaryService summaryService,
            IDocumentRepository documentRepository)
        {
            _environment = environment;
            _pdfExtractor = pdfExtractor;
            _summaryService = summaryService;
            _documentRepository = documentRepository;
        }

        public async Task<UploadResponse> ProcessDocumentAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Please upload a valid PDF file.");
            }

            // Create Uploads folder if it doesn't exist
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Generate unique filename
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Full file path
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            // Save file
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Validate file type
            string extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".pdf")
            {
                throw new NotSupportedException("Only PDF files are supported.");
            }

            // Extract text
            string extractedText = await _pdfExtractor.ExtractTextAsync(filePath);

            // Generate summary
            string summary = await _summaryService.GenerateSummaryAsync(extractedText);

            // Save document into repository
            var document = new DocumentInfo
            {
                FileName = file.FileName,
                ExtractedText = extractedText,
                Summary = summary,
                UploadedOn = DateTime.UtcNow
            };

            await _documentRepository.AddAsync(document);

            // Return response
            return new UploadResponse
            {
                FileName = file.FileName,
                FileSize = file.Length,
                FileType = extension,
                UploadedOn = DateTime.UtcNow,
                ExtractedText = extractedText,
                Summary = summary
            };
        }
    }
}