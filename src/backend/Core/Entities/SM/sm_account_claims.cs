namespace backend.Core.Entities.SM
{
    public class sm_account_claims : BaseEntity<int>
    {
        public string? claim_type { get; set; }
        public string? claim_value { get; set; }
        public int account_id { get; private set; }
        public sm_account_claims(int accountId)
        {
            account_id = accountId;
        }
    }
}
