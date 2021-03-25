using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository.IUserRepository
{
    public interface IRoleRepository
    {
        Task<bool> CreateRoleAsync(string role);
    }
}
