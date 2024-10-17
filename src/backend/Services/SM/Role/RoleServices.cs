using backend.Infrastructure.Repository;

namespace backend.Services.SM.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly IRepositoryWrapper _repository;
        public RoleServices(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public Task<int> CreateNewRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRoleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
