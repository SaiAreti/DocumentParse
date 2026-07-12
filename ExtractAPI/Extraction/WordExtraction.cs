/*using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using System.Text;

namespace ExtractAPI.Extraction
{
    public class WordExtraction : ITextExtractor
    {
        public async Task<string> ExtractTextAsync(string filePath)
        {
            var text = new StringBuilder();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                var body = wordDoc.MainDocumentPart?.Document.Body;

                if (body != null)
                {
                    foreach (var paragraph in body.Elements<Paragraph>())
                    {
                        text.AppendLine(paragraph.InnerText);
                    }
                }
            }

            return await Task.FromResult(text.ToString());
        }
    }
}
*/