namespace backend.DTOs.CMS.Request
{
    public record CreateMenuChildRequest(string name, int parent_menu_id);
}
