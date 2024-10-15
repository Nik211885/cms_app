namespace backend.DTOs.CMS.Request
{
    public record CreateFeedBackRequest(string full_name, string? organization, string email, string title, string content);
}
