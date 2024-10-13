namespace backend.Core.Entities.CMS
{
    public class CMSNewsContent : BaseEntity<int>
    {
        public int news_id { get; private set; }
        public string? content_html { get; set; }
        public string? title { get; set; }
        public CMSNewsContent(int newsId)
        {
            this.news_id = newsId;
        }
    }
}
