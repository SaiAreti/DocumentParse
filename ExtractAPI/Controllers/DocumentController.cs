using ExtractAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExtractAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentServices _documentService;

        public DocumentController(IDocumentServices documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("Please upload a PDF file.");
            }

            var result = await _documentService.ProcessDocumentAsync(file);

            return Ok(result);

        }
    }
}
