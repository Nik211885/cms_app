using backend.Core.ValueObject;

namespace backend.DTOs.CMS.Reponse
{
    public class NewsDescriptionReponse
    {
        public int stt { get; set; }
        public int id { get; set; }
        public string? title { get; set; }
        public string? content_title { get; set; }
        public DateTime create_at { get; set; }
        public Status status { get; set; }
        public string? status_message { get; set; }
    }
}
