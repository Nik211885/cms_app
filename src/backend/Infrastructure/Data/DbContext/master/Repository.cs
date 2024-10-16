﻿using System.Data.Common;
using UC.Core.Common;

namespace backend.Infrastructure.Data.DbContext.master
{
    public class Repository<TKeyId, TEntity> : IRepositoryBase<TKeyId, TEntity> where TEntity : BaseEntity<TKeyId>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Repository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public virtual async Task<List<TEntity>> GetEntitiesAsync(string? columnsQuery, string? orderQuery, DbTransaction dbTransaction = null)
        {
            var type = typeof(TEntity);
            string sql = SqlQueryBuilder.BuildGetListQuery(type, columnsQuery, orderQuery);
            List<TEntity> result = await _unitOfWork.Repository.QueryListAsync<TEntity>(sql, null, dbTransaction);
            if (result == null || result.Count == 0)
            {
                return default;
            }
            return result;
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(TKeyId id, DbTransaction dbTransaction = null)
        {
            var type = typeof(TEntity);
            object param;
            string sql = SqlQueryBuilder.BuildGetByIdQuery(type, out param, id);
            List<TEntity> result = await _unitOfWork.Repository.QueryListAsync<TEntity>(sql, param, dbTransaction);
            if (result == null || result.Count == 0)
            {
                return default;
            }
            return result[0];
        }

        public virtual async Task<TEntity> InsertEntityAsync(TEntity entity, DbTransaction dbTransaction = null)
        {
            var type = typeof(TEntity);
            object param;
            bool keyIdAuto = true;
            dynamic keyValue = null;
            if (typeof(TKeyId) == typeof(Guid))
            {
                keyIdAuto = false;
                keyValue = Guid.NewGuid();
            }
            else if (typeof(TKeyId) == typeof(string))
            {
                keyIdAuto = false;
                keyValue = StringHelpers.ShortId();
            }
            string sql = SqlQueryBuilder.BuildInsertQuery(type, out param, entity, keyIdAuto, keyValue);
            int result = await _unitOfWork.Repository.ExecuteAsync(sql, param, dbTransaction);
            if (result == 0)
            {
                throw new Exception(Consts.Message.DATABASE_INSERT_ERROR);
            }
            sql = SqlQueryBuilder.BuildGetMaxKeyIdQuery(type);
            if (keyIdAuto)
            {
                entity.id = await _unitOfWork.Repository.QueryFirstAsync<TKeyId>(sql, null);
            }
            else
            {
                entity.id = keyValue;
            }
            return entity;
        }

        public virtual async Task<TEntity> UpdateEntityAsync(TEntity entity, DbTransaction dbTransaction = null)
        {
            var type = typeof(TEntity);
            object param;
            string sql = SqlQueryBuilder.BuildUpdateQuery(type, out param, entity);
            int result = await _unitOfWork.Repository.ExecuteAsync(sql, param, dbTransaction);
            if (result == 0)
            {
                throw new Exception(Consts.Message.DATABASE_UPDATE_ERROR);
            }
            return entity;
        }

        public virtual async Task DeleteEntityAsync(TKeyId id, DbTransaction dbTransaction = null)
        {
            var type = typeof(TEntity);
            object param;
            string sql = SqlQueryBuilder.BuildDeleteQuery(type, out param, id);
            int result = await _unitOfWork.Repository.ExecuteAsync(sql, param, dbTransaction);
            if (result == 0)
            {
                throw new Exception(Consts.Message.DATABASE_DELETE_ERROR);
            }
        }
    }
}
