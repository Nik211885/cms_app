namespace backend.Services.SM.Role
{
    public interface IRoleServices
    {
        Task<int> CreateNewRoleAsync();
        Task<int> DeleteRoleAsync(int roleId);
        Task<int> UpdateRoleAsync();
    }
}
