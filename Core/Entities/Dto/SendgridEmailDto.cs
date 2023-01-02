namespace Core.Entities.Dto
{
    public class SendgridEmailDto
    {
        public string? DefaultEmailFrom { get; set; }
        public string? ApiKey { get; set; }
        public string? TemplateId { get; set; }
    }
}
