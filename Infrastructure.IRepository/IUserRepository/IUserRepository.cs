using Domain.Entities.Bases;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository.IUserRepository
{
    public interface IUserRepository<T> where T : IdentityBase
    {
        Task<T> FindByNameAsync(string Name);
        Task<T> FindByEmailAsync(string Email);
        Task<T> FindByIdAsync(string Id);

        Task<T> FindUser(Expression<Func<T, bool>> predicate);
        bool HasAny(Expression<Func<T, bool>> predicate);

        string GetRolesAsync(T pUser);

        void Delete(T entity);

        void DeleteRing(T[] entities);

        void DeleteWhere(Expression<Func<T, bool>> predicate);

        void SoftDelete(T entity);

        void SoftDeleteRing(T[] entities);

        void SoftDeleteWhere(Expression<Func<T, bool>> predicate);

        Task<IdentityResult> CreateUserAsync(T user, string password);

        Task<IdentityResult> AddUserToRole(T user, string role);

        Task<bool> CheckPasswordAsync(T user, string password);

        Task<IdentityResult> UpdateUser(T user);

        Task<IdentityResult> ChangePassword(T user, string password);
    }
}
