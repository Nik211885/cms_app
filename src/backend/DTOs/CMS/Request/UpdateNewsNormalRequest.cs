
namespace backend.DTOs.CMS.Request
{
    public record UpdateNewsNormalRequest : CreateNewsNormalRequest
    {
        public UpdateNewsNormalRequest(string? news_description, string? image_description, bool significant, string? content_html, string? title, CreateStatusRequest status, IEnumerable<int>? menu_id) : base(news_description, image_description, significant, content_html, title, status, menu_id)
        {
        }
    }
}
