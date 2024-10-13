namespace backend.Core.Entities.SM
{
    public class SMAccountClaim : BaseEntity<int>
    {
        public string? claim_type { get; set; }
        public string? claim_value { get; set; }
        public int account_id { get; private set; }
        public SMAccountClaim(int accountId)
        {
            account_id = accountId;
        }
    }
}
