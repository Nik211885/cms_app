
using backend.Core.ValueObject;

namespace backend.DTOs.CMS.Request
{
    public record UpdateNewsServicesRequest(bool significant,
        List<UpdateNewsContentRequest> news_content, Status status, string? status_message, IEnumerable<int>? menu_id);
    public record UpdateNewsContentRequest(int id, string? content_html, string? title);
}
