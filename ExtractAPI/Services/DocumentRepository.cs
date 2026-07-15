using DocumentFormat.OpenXml.Office2010.Excel;
using ExtractAPI.Data;
using ExtractAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ExtractAPI.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Document> AddAsync(Document document)
        {
            _context.Documents.Add(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<Document?> GetByFileNameAsync(int id)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents
                .OrderByDescending(x => x.UploadedOn)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
                return;

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();
        }
    }
}