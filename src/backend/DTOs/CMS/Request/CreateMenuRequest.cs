namespace backend.DTOs.CMS.Request
{
    public record CreateMenuRequest(string name, int? parent_menu_id, int? menu_type_id);
}
