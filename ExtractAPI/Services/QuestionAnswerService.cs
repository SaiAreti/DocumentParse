using System.Text.RegularExpressions;

namespace ExtractAPI.Services
{
    public class QuestionAnswerService : IQuestionAnswerService
    {
        public Task<string> GetAnswerAsync(string extractedText, string question)
        {
            if (string.IsNullOrWhiteSpace(extractedText))
                return Task.FromResult("Document is empty.");

            if (string.IsNullOrWhiteSpace(question))
                return Task.FromResult("Question is empty.");

            var sentences = Regex.Split(extractedText, @"(?<=[.!?])\s+")
                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                 .ToList();

            var keywords = question
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > 2)
                .ToList();

            string bestSentence = "No relevant answer found.";
            int bestScore = 0;

            foreach (var sentence in sentences)
            {
                int score = 0;

                foreach (var keyword in keywords)
                {
                    if (sentence.ToLower().Contains(keyword))
                    {
                        score++;
                    }
                }

                if (score > bestScore)
                {
                    bestScore = score;
                    bestSentence = sentence;
                }
            }

            return Task.FromResult(bestSentence);
        }
    }
}