namespace backend.Core.Entities.SM
{
    public class sm_roles : BaseEntity<int>
    {
        public string name { get; set; } = null!;
        public sm_roles()
        {

        }
    }
}
