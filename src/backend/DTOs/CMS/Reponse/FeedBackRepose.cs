namespace backend.DTOs.CMS.Reponse
{
    public record FeedBackRepose(string full_name, string? organization, string email, string title, string content, DateTime create_at);
}
