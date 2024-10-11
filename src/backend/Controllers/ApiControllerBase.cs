using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UC.Core.Interfaces;
using UC.Core.Models;
using uc.api.cms.Infrastructure.Authorization;
using Microsoft.Extensions.Caching.Memory;
using UC.Core.Common;
using backend.Infrastructure.Repository;

namespace uc.api.cms.Controllers
{
    [ApiController]
    [AuthorizeFilter]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase<TKeyId, TEntity> : ControllerBase
    {
        private readonly RepositoryDecorator<TKeyId, TEntity> _serviceDecorator;
        private readonly ILogger _logger;
        private readonly IUserProvider _userProvider;
        private readonly IMemoryCache _cache;
        public ApiControllerBase(IRepositoryWrapper service, ILogger logger, IUserProvider userProvider, IMemoryCache cache)
        {
            _userProvider = userProvider;
            _logger = logger;
            _serviceDecorator = new RepositoryDecorator<TKeyId, TEntity>(service);
            _cache = cache;
        }
        #region base actions
        [HttpGet("get-items")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> GetEntitiesAsync(bool? caching, bool? clearCache, string? columnsQuery, string? orderQuery)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                Type t = typeof(TEntity);
                List<TEntity> items = null;
                if(clearCache == true)
                {
                    _cache.Remove(t.Name);
                }
                if (_cache.TryGetValue(t.Name, out List<TEntity> data) && caching.HasValue && caching.Value)
                {
                    items = data;
                }
                else
                {
                    items = await _serviceDecorator.GetEntitiesAsync(columnsQuery, orderQuery);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(1));
                    _cache.Set(t.Name, items, cacheEntryOptions);
                }
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("get-item-by-id/{id}")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> GetEntityByIdAsync(TKeyId id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var item = await _serviceDecorator.GetEntityByIdAsync(id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("insert-item")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> InsertEntityAsync([FromBody] TEntity model)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var item = await _serviceDecorator.InsertEntityAsync(model);
                Type t = typeof(TEntity);
                _cache.Remove(t.Name);
                return ResponseMessage.Success(item, Consts.Message.DATABASE_INSERT_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPut("update-item")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> UpdateEntityAsync([FromBody] TEntity model)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var item = await _serviceDecorator.UpdateEntityAsync(model);
                Type t = typeof(TEntity);
                _cache.Remove(t.Name);
                return ResponseMessage.Success(item, Consts.Message.DATABASE_UPDATE_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpDelete("delete-item/{id}")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> DeleteEntityAsync(TKeyId id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                await _serviceDecorator.DeleteEntityAsync(id);
                Type t = typeof(TEntity);
                _cache.Remove(t.Name);
                return ResponseMessage.Success(Consts.Message.DATABASE_DELETE_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }
        #endregion
    }
}
