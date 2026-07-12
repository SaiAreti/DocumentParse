namespace ExtractAPI.Extraction
{
    public interface ITextExtractor
    {
        Task<string> ExtractTextAsync(string filePath);
    }
}
