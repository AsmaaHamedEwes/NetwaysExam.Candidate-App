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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> DbSet;
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext context)
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
            reader.Close();
            
            return list;
        }
        public TEntity Add(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                DbSet.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> AddRing(TEntity[] entities)
        {
            try
            {
                DbSet.AddRange(entities);
                return entities.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SoftDeleteRing(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                SoftDelete(item);
            }
        }


        public void SoftDeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();
            entity.IsDeleted = true;
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Update(TEntity entity)
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

        public List<TEntity> UpdateRing(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }

            return entities.ToList();
        }

        public List<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.Where(e => e.IsDeleted == false);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count()
        {
            try
            {
                return DbSet.Where(e => e.IsDeleted == false).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = DbSet.Where(e => e.IsDeleted == false);
                return query.Where(predicate).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> GetAll()
        {
            try
            {

                return DbSet.Where(e => e.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = DbSet.Where(e => e.IsDeleted == false);
                return query.Where(predicate).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.Where(predicate);
                query = query.Where(e => e.IsDeleted == false);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public TEntity GetSingle(int pId)
        {
            try
            {
                return DbSet.Where(e => e.Id == pId && e.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(e => e.IsDeleted == false).Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.Where(predicate).Where(e => e.IsDeleted == false);
                
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HasAny(TEntity entity)
        {
            try
            {
                return DbSet.Where(e => e == entity).Where(e => e.IsDeleted == false).Any();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool HasAny(int id)
        {
            try
            {
                return DbSet.Where(e => e.Id == id).Where(e => e.IsDeleted == false).Any();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool HasAny(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).Where(e => e.IsDeleted == false).Any();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TEntity> LoadHierarchy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.AsQueryable();
                //query.Where(predicate);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                query.Where(e => e.IsDeleted == false).Where(predicate);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(e => e.IsDeleted == false);
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

        public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(e => e.IsDeleted == false);
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

        public List<TEntity> GetAll(int count)
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T ExecuteScalar<T>(string sp_name, Dictionary<string, object> parames)
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
           return (T)cmd.ExecuteScalar();
        }
    }
}

