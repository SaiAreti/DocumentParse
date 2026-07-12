namespace ExtractAPI.Helper
{
    public static class SummaryHelper
    {
        public static string GenerateSummary(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "No text found.";

            var sentences = text.Split('.', StringSplitOptions.RemoveEmptyEntries);

            if (sentences.Length <= 3)
                return text;

            return string.Join(". ", sentences.Take(3)) + ".";
        }
    }
}
