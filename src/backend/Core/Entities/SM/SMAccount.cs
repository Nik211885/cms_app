using backend.Core.ValueObject;
namespace backend.Core.Entities.SM
{
    public class SMAccount : BaseEntity<int>
    {
        public string user_name { get; set; } = null!;
        public string full_name { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password_hash { get; set; } = null!;
        public Gender gender { get; set; }
        public string address { get; set; } = null!;
        public DateTime create_at { get; private set; }
        public DateTime update_at { get; set; }
        public string? avatar { get; set; }
        public SMAccount()
        {
            create_at = DateTime.Now;
            update_at = DateTime.Now;
        }

    }
}
