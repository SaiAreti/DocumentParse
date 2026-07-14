using ExtractAPI.Model;

namespace ExtractAPI.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private static readonly Dictionary<string, DocumentInfo> _documents = new();

        public Task AddAsync(DocumentInfo document)
        {
            _documents[document.FileName] = document;

            return Task.CompletedTask;
        }

        public Task<DocumentInfo?> GetByFileNameAsync(string fileName)
        {
            _documents.TryGetValue(fileName, out var document);

            return Task.FromResult(document);
        }

        public Task<List<DocumentInfo>> GetAllAsync()
        {
            return Task.FromResult(_documents.Values.ToList());
        }

        public Task DeleteAsync(string fileName)
        {
            _documents.Remove(fileName);

            return Task.CompletedTask;
        }
    }
}