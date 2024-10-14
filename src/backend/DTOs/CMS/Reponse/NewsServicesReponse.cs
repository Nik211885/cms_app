using backend.Core.ValueObject;
using backend.DTOs.CMS.Request;
using backend.Helper.Untils;

namespace backend.DTOs.CMS.Reponse
{
    public class NewsServicesReponse : NewsReponse
    {
        public IEnumerable<NewsContentReponse> content { get; set; } = [];
    }
    public record NewsContentReponse(int id, string? content_html, string? title)
        : CreateNewsContentRequest(content_html, title);
}
