using System.Text.RegularExpressions;

namespace ExtractAPI.Services
{
    public class SummaryService : ISummaryService
    {
        public Task<string> GenerateSummaryAsync(string extractedText)
        {
            if (string.IsNullOrWhiteSpace(extractedText))
                return Task.FromResult("No content available.");

            // Split text into sentences
            var sentences = Regex.Split(extractedText, @"(?<=[.!?])\s+")
                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                 .Select(s => s.Trim())
                                 .ToList();

            if (sentences.Count == 0)
                return Task.FromResult("No summary available.");

            // Remove duplicate sentences
            sentences = sentences.Distinct().ToList();

            // Ignore very short sentences
            sentences = sentences
                .Where(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length >= 5)
                .ToList();

            if (sentences.Count == 0)
                return Task.FromResult("No summary available.");

            // Take the first 5 meaningful sentences
            var summary = string.Join(" ", sentences.Take(5));

            return Task.FromResult(summary);
        }
    }
}