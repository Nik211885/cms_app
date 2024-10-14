using backend.Core.ValueObject;

namespace backend.DTOs.CMS.Request
{
    public record CreateNewsServicesRequest(bool significant,
        IEnumerable<CreateNewsContentRequest> news_content, Status status, string? status_message,
        IEnumerable<int>? menu_id);
    public record CreateNewsContentRequest(string? content_html, string? title);
}
