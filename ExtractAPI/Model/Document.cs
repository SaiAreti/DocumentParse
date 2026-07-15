namespace ExtractAPI.Model
{
    public class Document
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string ExtractedText { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    }
}
