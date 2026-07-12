using ExtractAPI.Extraction;
using ExtractAPI.Services;
using iText.Kernel.Pdf.Canvas.Parser;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register application services
builder.Services.AddScoped<IDocumentServices, DocumentServices>();
// Register PDF Text Extractor
builder.Services.AddScoped<ITextExtractor, PdfExtraction>();
// Register Word Text Extractor
///builder.Services.AddScoped<ITextExtractor, WordExtraction>();
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
