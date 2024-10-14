using backend.Core.ValueObject;
using backend.Helper.Untils;

namespace backend.DTOs.CMS.Reponse
{
    public class NewsNormalReponse : NewsReponse
    {
        public string? news_description { get; set; }
        public string? image_description { get; set; }
        public string? content_html { get; set; }
        public string? title { get; set; }
    }
}
