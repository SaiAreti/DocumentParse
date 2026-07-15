using ExtractAPI.Extraction;
using ExtractAPI.Helper;
using ExtractAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExtractAPI.Services
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ITextExtractor _pdfExtractor;
        private readonly IDocumentRepository _documentRepository;

        public DocumentServices(
            IWebHostEnvironment environment,
            ITextExtractor pdfExtractor,
            IDocumentRepository documentRepository)
        {
            _environment = environment;
            _pdfExtractor = pdfExtractor;
            _documentRepository = documentRepository;
        }

        public async Task<UploadResponse> ProcessDocumentAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Please upload a valid file.");
            }

            // Create Uploads folder if it doesn't exist
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Save uploaded file
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Extract text
            string extractedText;

            switch (Path.GetExtension(file.FileName).ToLower())
            {
                case ".pdf":
                    extractedText = await _pdfExtractor.ExtractTextAsync(filePath);
                    break;

                default:
                    throw new NotSupportedException("Only PDF files are supported.");
            }

            // Generate Summary
            string summary = SummaryHelper.GenerateSummary(extractedText);

            // Save to SQL Server
            var document = new Document
            {
                FileName = file.FileName,
                FilePath = filePath,
                ExtractedText = extractedText,
                Summary = summary,
                UploadedOn = DateTime.UtcNow
            };

            await _documentRepository.AddAsync(document);

            // Return Response
            return new UploadResponse
            {
                FileName = document.FileName,
                ExtractedText = document.ExtractedText,
                Summary = document.Summary
            };
        }
    }
}