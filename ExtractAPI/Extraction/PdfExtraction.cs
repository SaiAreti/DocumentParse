using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace ExtractAPI.Extraction
{
    public class PdfExtraction : ITextExtractor
    {
        public async Task<string> ExtractTextAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("PDF file not found.", filePath);
            }

            return await Task.Run(() =>
            {
                var extractedText = new System.Text.StringBuilder();

                using (PdfReader reader = new PdfReader(filePath))
                using (PdfDocument pdfDocument = new PdfDocument(reader))
                {
                    int totalPages = pdfDocument.GetNumberOfPages();

                    for (int page = 1; page <= totalPages; page++)
                    {
                        string pageText = PdfTextExtractor.GetTextFromPage(
                            pdfDocument.GetPage(page));

                        extractedText.AppendLine(pageText);
                    }
                }

                return extractedText.ToString();
            });

        }
    }
}
