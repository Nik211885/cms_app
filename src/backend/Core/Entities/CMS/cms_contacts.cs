namespace backend.Core.Entities.CMS
{
    public class cms_contacts : BaseEntity<int>
    {
        public string name_organization { get; set; } = null!;
        public string address { get; set; } = null!;
        public string tel { get; set; } = null!;
        public string fax { get; set; } = null!;
        public string email { get; set; } = null!;
        public bool is_work { get; set; }
    }
}
