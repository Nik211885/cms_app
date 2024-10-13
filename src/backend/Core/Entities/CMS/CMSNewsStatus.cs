using backend.Core.ValueObject;
namespace backend.Core.Entities.CMS
{
    public class CMSNewsStatus
    {
        public Status status { get; private set; }
        public int news_id { get; private set; }
        public string? message { get; private set; }
        public DateTime create_at { get; private set; }
        public int create_by { get; private set; }
        public CMSNewsStatus(Status status, int newsId, string? message, int createdBy)
        {
            this.status = status;
            news_id = newsId;
            this.message = message;
            create_at = DateTime.Now;
            create_by = createdBy;
        }
    }
}
