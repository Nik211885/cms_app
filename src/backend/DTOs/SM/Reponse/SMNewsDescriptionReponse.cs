using backend.Core.ValueObject;

namespace backend.DTOs.SM.Reponse
{
    public class SMNewsDescriptionReponse
    {
        public int stt { get; set; }
        public int id { get; set; }
        public string? title { get; set; }
        public string? content_title { get; set; }
        public DateTime create_at { get; set; }
        public Status status { get; set; }
        public string? status_message { get; set; }
        public bool active { get; set; }
        public bool significant { get; set; }
        //public int create_by_id { get; set; }
        //public string create_by_full_name { get; set; } = null!;
    }
}
