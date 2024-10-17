namespace backend.DTOs.CMS.Reponse
{
    public class CMSNewsDescriptionReponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string? news_description { get; set; }
        public string? image_description { get; set; }
        public long views { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
    }
}
