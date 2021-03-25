using Domain.Entities.Bases;
using Infrastructure.IRepository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        long SaveChanges();
        long SaveChangesTransaction();

        /// <summary>
        /// SaveAsync Inteface
        /// </summary>
        Task<long> SaveAsync();

        Task<long> SaveAsyncTransaction();

        /// <summary>
        /// Get Reposatory InterFace
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;

        IRepositoryArc<TEntity> GetRepositoryArc<TEntity>() where TEntity : EntityBase;
    }
}
