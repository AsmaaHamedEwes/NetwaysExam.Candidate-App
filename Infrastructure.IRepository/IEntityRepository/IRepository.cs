using Domain.Entities.Bases;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.IRepository.IEntityRepository
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        List<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        List<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);

        List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);

        List<TEntity> LoadHierarchy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        List<TEntity> GetAll();

        List<TEntity> GetAll(int count);

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        TEntity GetSingle(int id);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Add(TEntity entity);

        List<TEntity> AddRing(TEntity[] entities);

        TEntity Update(TEntity entity);

        List<TEntity> UpdateRing(TEntity[] entities);
        List<T> GetData<T>(string sp_name, Dictionary<string, object> parames) ;
        T ExecuteScalar<T>(string sp_name, Dictionary<string, object> parames);
        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        bool HasAny(TEntity entity);

        bool HasAny(int id);
        bool HasAny(Expression<Func<TEntity, bool>> predicate);

        void SoftDelete(TEntity entity);

        void SoftDeleteRing(TEntity[] entities);

        void SoftDeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
