using ExtractAPI.Extraction;
using ExtractAPI.Services;
using iText.Kernel.Pdf.Canvas.Parser;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register application services
builder.Services.AddScoped<IDocumentServices, DocumentServices>();

builder.Services.AddScoped<ITextExtractor, PdfExtraction>();

builder.Services.AddScoped<ISummaryService, SummaryService>();

builder.Services.AddSingleton<IDocumentRepository, DocumentRepository>();

builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
