using backend.Core.Entities.SM;
using backend.Core.Exceptions;
using backend.DTOs.SM.Request;
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

        public async Task<int> CreateNewRoleAsync(CreateRoleRequest request)
        {
            var role = new sm_roles(request.name);
            role = await _repository.RoleRepository.InsertEntityAsync(role, default!);
            return role.id;
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _repository.RoleRepository.GetEntityByIdAsync(roleId, default!);
            if(role is null)
            {
                throw new NotFoundException(roleId);
            }
            await _repository.RoleRepository.DeleteEntityAsync(roleId,default!);
        }

        public async Task<IReadOnlyCollection<sm_roles>> GetAllRolesAsync()
        {
            var result = await _repository.RoleRepository.GetEntitiesAsync()
        }


        public async Task<int> UpdateRoleAsync(int roleId, UpdateRoleRequest request)
        {
            var role = await _repository.RoleRepository.GetEntityByIdAsync(roleId,default!);
            if (role is null)
            {
                throw new NotFoundException(roleId);
            }
            role.name = request.name;
            await _repository.RoleRepository.UpdateEntityAsync(role,default!);
            return role.id;
        }
    }
}
