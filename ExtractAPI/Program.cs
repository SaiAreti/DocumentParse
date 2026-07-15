using ExtractAPI.Data;
using ExtractAPI.Extraction;
using ExtractAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Configure SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IDocumentServices, DocumentServices>();

builder.Services.AddScoped<ITextExtractor, PdfExtraction>();

builder.Services.AddScoped<ISummaryService, SummaryService>();

// Repository (must be Scoped)
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();