namespace ExtractAPI.Services
{
    public interface IQuestionAnswerService
    {
        Task<string> GetAnswerAsync(string extractedText, string question);
    }
}