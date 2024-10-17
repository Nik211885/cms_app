namespace backend.DTOs.CMS.Request
{
    public record UpdateMenuRequest(int id, string name, int? parent_menu_id);
}
