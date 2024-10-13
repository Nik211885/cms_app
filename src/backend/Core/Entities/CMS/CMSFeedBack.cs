namespace backend.Core.Entities.CMS
{
    public class CMSFeedBack
    {
        public string full_name { get; private set; } = null!;
        public string? organization { get; private set; }
        public string email { get; private set; } = null!;
        public string title { get; private set; } = null!;
        public string content { get; private set; } = null!;
        public DateTime create_at { get; private set; }
        public CMSFeedBack(string fullName, string organization, string email, string title, string content)
        {
            full_name = fullName;
            this.organization = organization;
            this.email = email;
            this.title = title;
            this.content = content;
            create_at = DateTime.Now;
        }

    }
}
