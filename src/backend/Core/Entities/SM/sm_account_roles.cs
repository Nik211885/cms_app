namespace backend.Core.Entities.SM
{
    public class sm_account_roles : BaseEntity<int>
    {
        public int account_id { get; private set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public sm_account_roles()
        {

        }
        public sm_account_roles(int accountId)
        {
            create_at = DateTime.Now;
            update_at = DateTime.Now;
            account_id = accountId;
        }
    }
}
