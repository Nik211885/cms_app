namespace backend.Core.Entities.CMS
{
    public class CMSMenuType : BaseEntity<int>
    {
        public string name_type { get; set; } = null!;
        public DateTime create_at { get; private set; }
        public DateTime update_at { get; set; }
        public CMSMenuType()
        {
            create_at = DateTime.Now;
            update_at = DateTime.Now;
        }
    }
}
