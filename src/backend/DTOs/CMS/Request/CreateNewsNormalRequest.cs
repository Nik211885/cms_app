using backend.Core.ValueObject;
using Microsoft.AspNetCore.Http;

namespace backend.DTOs.CMS.Request
{
    public record CreateNewsNormalRequest(string? news_description,
        bool significant, string? content_html, string? title, Status status, string? status_message,
        IEnumerable<int>? menu_id
        );
}
