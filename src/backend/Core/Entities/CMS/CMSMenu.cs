namespace backend.Core.Entities.CMS
{
    public class CMSMenu : BaseEntity<int>
    {
        public string name { get; set; } = null!;
        public int? parent_menu_id { get; private set; }
        public string? avatar { get; set; }
        public int menu_type_id { get; private set; }
        public string? image_description { get; set; }
        public CMSMenu(int parentMenuId, int menuTypeId)
        {
           parent_menu_id = parentMenuId;
           menu_type_id = menuTypeId;
        }
    }
}
