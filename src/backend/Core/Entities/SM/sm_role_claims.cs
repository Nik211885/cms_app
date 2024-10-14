namespace backend.Core.Entities.SM
{
    public class sm_role_claims : BaseEntity<int>
    {
        public int role_id { get; private set; }
        public string? claim_type { get; set; }
        public string? claim_value { get; set; }
        public sm_role_claims()
        {

        }
        public sm_role_claims(int roleId)
        {
            role_id = roleId;
        }
    }
}
