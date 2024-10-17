using backend.Core.ValueObject;

namespace backend.DTOs.CMS.Reponse
{
    public abstract class NewsReponse
    {
        public int id { get; set; }
        public long views { get; set; }
        public bool significant { get; set; }
        public bool active { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public Status status { get; set; }
        public string? status_message { get; set; }
        public int create_by_id { get; set; }
        public string? create_by_full_name { get; set; }
        public IEnumerable<MenuReponse> menu { get; set; } = [];
    }
}
