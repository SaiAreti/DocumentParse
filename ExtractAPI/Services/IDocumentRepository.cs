using ExtractAPI.Model;

namespace ExtractAPI.Services
{
    public interface IDocumentRepository
    {
        Task AddAsync(DocumentInfo document);

        Task<DocumentInfo?> GetByFileNameAsync(string fileName);

        Task<List<DocumentInfo>> GetAllAsync();

        Task DeleteAsync(string fileName);
    }
}