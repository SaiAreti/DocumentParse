using ExtractAPI.Model;

namespace ExtractAPI.Services
{
    public interface IDocumentRepository
    {
        Task<Document> AddAsync(Document document);

        Task<Document?> GetByFileNameAsync(int id);

        Task<List<Document>> GetAllAsync();

        Task DeleteAsync(int id);
    }
}