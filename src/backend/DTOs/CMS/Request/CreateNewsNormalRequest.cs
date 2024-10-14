using backend.Core.ValueObject;

namespace backend.DTOs.CMS.Request
{
    public record CreateNewsNormalRequest(string? news_description, 
        string? image_description, bool significant, string? content_html, string? title, CreateStatusRequest status,
        IEnumerable<int>? menu_id
        );
    public record CreateStatusRequest(Status status, string? message);
}
