namespace backend.Core.Entities.CMS
{
    public class cms_news_content : BaseEntity<int>
    {
        public int news_id { get; private set; }
        public string? content_html { get; set; }
        public string? title { get; set; }
        public cms_news_content()
        {

        }
        public cms_news_content(int newsId, string? contentHtml, string? title)
        {
            content_html = contentHtml;
            this.title = title;
            this.news_id = newsId;
        }
    }
}
