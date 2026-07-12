using ExtractAPI.Extraction;
using ExtractAPI.Helper;
using ExtractAPI.Model;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExtractAPI.Services
{
    public class DocumentServices : IDocumentServices
    {

        private readonly IWebHostEnvironment _environment;
        private readonly ITextExtractor _pdfExtractor;
        //private readonly ITextExtractor _wordExtractor;

        public DocumentServices(
            IWebHostEnvironment environment,
            ITextExtractor pdfExtractor
            //ITextExtractor wordExtractor
            )
        {
            _environment = environment;
            _pdfExtractor = pdfExtractor;
            //_wordExtractor = wordExtractor;
        }
        public async Task<UploadResponse> ProcessDocumentAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            string uploadsFolder = Path.Combine( _environment.WebRootPath,"uploads");
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique file name
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Full path to save file
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save uploaded file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Extract text based on file type
            string extractedText;
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            switch (extension)
            {
                case ".pdf":
                    extractedText = await _pdfExtractor.ExtractTextAsync(filePath);
                    break;
/*
                case ".docx":
                    extractedText = await _wordExtractor.ExtractTextAsync(filePath);
                    break;*/

                default:
                    throw new NotSupportedException("Only PDF and DOCX files are supported.");
            }

            // Generate summary
            string summary = SummaryHelper.GenerateSummary(extractedText);

            // Return response
            return new UploadResponse
            {
                FileName = file.FileName,
                ExtractedText = extractedText,
                Summary = summary
            };



        }
    }
}
