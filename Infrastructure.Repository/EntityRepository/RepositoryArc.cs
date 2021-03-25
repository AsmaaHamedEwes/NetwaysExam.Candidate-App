using Domain.Entities.Bases;
using Domain.Persistance;
using Domain.Persistance.EntitiesProperties;
using Infrastructure.IRepository.IEntityRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Infrastructure.Repository.EntityRepository
{
    public class RepositoryArc<TEntity> : IRepositoryArc<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> DbSet;

        private readonly AppDbContext dbContext;

        public RepositoryArc(AppDbContext context)
        {
            DbSet = context.Set<TEntity>();
            dbContext = context;
        }
        public List<T> GetData<T>(string sp_name, Dictionary<string, object> parames)
        {
            using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sp_name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
            foreach (var item in parames)
            {
                var param1 = new SqlParameter
                {
                    ParameterName = item.Key,
                    SqlValue = item.Value
                };

                cmd.Parameters.Add(param1);
            }
            var reader = cmd.ExecuteReader();
            if (reader == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            while (reader.Read())
            {
                T obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, reader[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public virtual void DeleteArc(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRingArc(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                DeleteArc(item);
            }
        }

        public void DeleteWhereArc(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entites = DbSet.Where(predicate);
                foreach (var entity in entites)
                {
                    DbSet.Attach(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> AllIncludingArc(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.AsNoTracking();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountArc()
        {
            try
            {
                return DbSet.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountArc(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetAllArc()
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetAllArc(int count)
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetAllArc(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetAllIncludingArc
            (Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.Where(predicate);
                //query.Where(predicate);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public TEntity GetSingleArc(int pId)
        {
            try
            {
                return DbSet.Where(e => e.Id == pId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TEntity GetSingleArc(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TEntity GetSingleArc(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.AsNoTracking();
                query.Where(predicate);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool HasAnyArc(TEntity entity)
        {
            try
            {
                return DbSet.Where(e => e == entity).Any();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool HasAnyArc(int id)
        {
            try
            {
                return DbSet.Where(e => e.Id == id).Any();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool HasAnyArc(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).Any();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> LoadHierarchyArc
            (Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.AsQueryable();
                //query.Where(predicate);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                query.Where(predicate);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TEntity GetFirstOrDefaultArc(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public List<TEntity> ToListArc(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity UpdateArc(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> UpdateRingArc(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                UpdateArc(item);
            }

            return entities.ToList();
        }
    }
}
