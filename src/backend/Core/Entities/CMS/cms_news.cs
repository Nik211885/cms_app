using System.Text.Json.Serialization;

namespace backend.Core.Entities.CMS
{
    public class cms_news : BaseEntity<int>
    {
        public DateTime create_at { get; private set; }
        public DateTime update_at { get; set; }
        public long views { get; set; }
        public int create_by { get; private set; }
        public string? news_description { get; set; }
        public string? image_description { get; set; }
        public bool significant { get; set; }
        public bool active { get; set; }
        public cms_news()
        {

        }
        public cms_news(int createBy)
        {
            create_at = DateTime.Now;
            update_at = DateTime.Now;
            create_by = createBy;
        }
    }
}
