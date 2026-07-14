namespace ExtractAPI.Services
{
    public interface ISummaryService
    {
        Task<string> GenerateSummaryAsync(string extractedText);
    }
}
