namespace ExtractAPI.Model
{
    public class DocumentInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string ExtractedText { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    }
}