using backend.Core.ValueObject;

namespace backend.DTOs.SM.Reponse
{
    public class AccountReponse
    {
        public int id { get; set; }
        public string full_name { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public string email { get; set; } = null!;
        public Gender gender { get; set; }
        public string address { get; set; } = null!;
        public DateTime create_at { get; private set; }
        public DateTime update_at { get; set; }
        public string? avatar { get; set; }
    }
}
