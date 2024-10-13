namespace backend.Core.Entities.SM
{
    public class SMRoleClaim : BaseEntity<int>
    {
        public int role_id { get; private set; }
        public string? claim_type { get; set; }
        public string? claim_value { get; set; }
        public SMRoleClaim(int roleId)
        {
            role_id = roleId;
        }
    }
}
