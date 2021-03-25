using Domain.Entities.Bases;
using Domain.Persistance;
using Domain.Persistance.EntitiesProperties;
using Infrastructure.IRepository;
using Infrastructure.IRepository.IEntityRepository;
using Infrastructure.Repository.EntityRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// Constructor For Db Context (movies)
        /// </summary>
        /// <param name="Dbcontext"></param>
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(context);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryArc<TEntity> GetRepositoryArc<TEntity>() where TEntity : EntityBase
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryArc<TEntity>(context);
            return (IRepositoryArc<TEntity>)_repositories[type];
        }

        public async Task<long> SaveAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (((SqlException)ex.InnerException).Number == 2627)//dublicate Key
                {
                    return 10000002627;
                }
                else if (((SqlException)ex.InnerException).Number == 547)//// Foreign Key violation
                {
                    return 1000000547;
                }

                else if (((SqlException)ex.InnerException).Number == 2601)//// Primary key violation
                {
                    return 10000002601;
                }
            }
            return -1;
        }

        public async Task<long> SaveAsyncTransaction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = await context.SaveChangesAsync();

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public long SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (((SqlException)ex.InnerException).Number == 2627)//dublicate Key
                {
                    return 10000002627;
                }
                else if (((SqlException)ex.InnerException).Number == 547)//// Foreign Key violation
                {
                    return 1000000547;
                }

                else if (((SqlException)ex.InnerException).Number == 2601)//// Primary key violation
                {
                    return 10000002601;
                }
            }
            return -1;
        }

        public long SaveChangesTransaction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = context.SaveChanges();

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
