using DocumentFormat.OpenXml.EMMA;
using ExtractAPI.Model;
using ExtractAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtractAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IDocumentRepository _repository;
        private readonly IQuestionAnswerService _questionService;

        public QuestionController(
            IDocumentRepository repository,
            IQuestionAnswerService questionService)
        {
            _repository = repository;
            _questionService = questionService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            var document = await _repository.GetByFileNameAsync(request.FileName);

            if (document == null)
            {
                return NotFound("Document not found.");
            }

            var answer = await _questionService.GetAnswerAsync(
                document.ExtractedText,
                request.Question);

            return Ok(new QuestionResponse
            {
                Question = request.Question,
                Answer = answer
            });
        }
    }
}