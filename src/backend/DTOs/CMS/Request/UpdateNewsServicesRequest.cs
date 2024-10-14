
namespace backend.DTOs.CMS.Request
{
    public record UpdateNewsServicesRequest(bool significant, 
        List<UpdateNewsContentRequest> news_content, CreateStatusRequest status, 
        IEnumerable<int>? menu_id);
    public record UpdateNewsContentRequest(int id, string? content_html, string? title);
}
