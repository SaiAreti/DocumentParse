using ExtractAPI.Model;

namespace ExtractAPI.Services
{
    public interface IDocumentServices
    {
        Task<UploadResponse> ProcessDocumentAsync(IFormFile file);
    }
}
