namespace AcademicHub.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class HtmlContentModel
    {
        public string HtmlContent { get; set; }
    }

}