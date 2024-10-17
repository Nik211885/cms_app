namespace backend.Core.Entities.CMS
{
    public class cms_menu : BaseEntity<int>
    {
        public string name { get; set; } = null!;
        public int? parent_menu_id { get; private set; }
        public string? avatar { get; set; }
        public DateTime create_at { get; set; }
        public int menu_type_id { get; private set; }
        public string? image_description { get; set; }
        private List<cms_menu>? _menuChild;
        public void GetMenu(out List<cms_menu>? menuChild)
        {
            menuChild = _menuChild;
        }
        public void RaiseMenuChild(IEnumerable<cms_menu> menuChild)
        {
            _menuChild ??= [];
            _menuChild.AddRange(menuChild);
        }
        public cms_menu()
        {

        }
        public cms_menu(int? parentMenuId, int menuTypeId, string name)
        {
            this.create_at = DateTime.Now;
            this.name = name;
            parent_menu_id = parentMenuId;
            menu_type_id = menuTypeId;
        }
    }
}
