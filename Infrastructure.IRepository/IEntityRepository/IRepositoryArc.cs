using Domain.Entities.Bases;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.IRepository.IEntityRepository
{
    public interface IRepositoryArc<TEntity>
        where TEntity : EntityBase
    {
        List<TEntity> AllIncludingArc(params Expression<Func<TEntity, object>>[] includeProperties);

        List<TEntity> GetAllIncludingArc(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetFirstOrDefaultArc(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);

        List<TEntity> ToListArc(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);

        List<TEntity> LoadHierarchyArc(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        List<TEntity> GetAllArc();

        List<TEntity> GetAllArc(int count);

        List<TEntity> GetAllArc(Expression<Func<TEntity, bool>> predicate);

        TEntity GetSingleArc(int id);

        TEntity GetSingleArc(Expression<Func<TEntity, bool>> predicate);

        TEntity GetSingleArc(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity UpdateArc(TEntity entity);
        List<T> GetData<T>(string sp_name, Dictionary<string, object> parames);
        List<TEntity> UpdateRingArc(TEntity[] entities);

        int CountArc();

        int CountArc(Expression<Func<TEntity, bool>> predicate);

        bool HasAnyArc(TEntity entity);

        bool HasAnyArc(int id);
        bool HasAnyArc(Expression<Func<TEntity, bool>> predicate);

        void DeleteArc(TEntity entity);

        void DeleteRingArc(TEntity[] entities);

        void DeleteWhereArc(Expression<Func<TEntity, bool>> predicate);
    }
}
