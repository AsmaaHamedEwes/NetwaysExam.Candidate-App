using Domain.Entities.Bases;
using Domain.Persistance;
using Domain.Persistance.EntitiesProperties;
using Infrastructure.IRepository.IUserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.UserRepository
{
    public class UserRepository<T> : IUserRepository<T> where T : IdentityBase
    {
        protected readonly DbSet<T> DbSet;
        private readonly UserManager<T> userManager;

        public UserRepository(UserManager<T> userManager, AppDbContext context)
        {
            DbSet = context.Set<T>();
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserToRole(T user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> ChangePassword(T user, string password)
        {
            var newPassword = userManager.PasswordHasher.HashPassword(user, password);
            user.PasswordHash = newPassword;
            return userManager.UpdateAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(T user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(T user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public void Delete(T entity)
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

        public void DeleteRing(T[] entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();

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

        public async Task<T> FindByEmailAsync(string Email)
        {
            return await userManager.FindByEmailAsync(Email);
        }

        public async Task<T> FindByIdAsync(string Id)
        {
            return await userManager.FindByIdAsync(Id);
        }

        public async Task<T> FindByNameAsync(string Name)
        {
            return await userManager.FindByNameAsync(Name);
        }

        public async Task<T> FindUser(Expression<Func<T, bool>> predicate)
        {
            return await this.DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public string GetRolesAsync(T pUser)
        {
            return userManager.GetRolesAsync(pUser).Result.FirstOrDefault();
        }

        public bool HasAny(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).Any();
        }

        public void SoftDelete(T entity)
        {

            entity.IsDeleted = true;
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

        public void SoftDeleteRing(T[] entities)
        {
            foreach (var item in entities)
            {
                SoftDelete(item);
            }
        }

        public void SoftDeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();
            entity.IsDeleted = true;
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

        public Task<IdentityResult> UpdateUser(T user)
        {
            return userManager.UpdateAsync(user);
        }
    }
}
