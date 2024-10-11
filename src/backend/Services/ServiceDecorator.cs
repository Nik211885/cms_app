using UC.Core.Interfaces;

namespace uc.api.cms.Services
{
    class ServiceDecorator<TKeyId, TEntity>
    {
        private IRepositoryBase<TKeyId, TEntity> _repositoryBase;
        public ServiceDecorator(IServiceWrapper repository)
        {
            if (_repositoryBase == null)
            {
                throw new Exception("Class ServiceDecorator not configured yet");
            }
        }
        #region base service
        public async Task<List<TEntity>> GetEntitiesAsync(string? columnsQuery, string? orderQuery)
        {
            return await _repositoryBase.GetEntitiesAsync(columnsQuery, orderQuery, null);
        }
        public async Task<TEntity> GetEntityByIdAsync(TKeyId id)
        {
            return await _repositoryBase.GetEntityByIdAsync(id, null);
        }
        public async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            return await _repositoryBase.InsertEntityAsync(entity, null);
        }
        public async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            return await _repositoryBase.UpdateEntityAsync(entity, null);
        }
        public async Task DeleteEntityAsync(TKeyId id)
        {
            await _repositoryBase.DeleteEntityAsync(id, null);
        }
        #endregion
    }
}
