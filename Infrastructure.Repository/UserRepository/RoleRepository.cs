using Infrastructure.IRepository.IUserRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.UserRepository
{
    public class RoleRepository : IRoleRepository
    {
        private RoleManager<IdentityRole> roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string role)
        {
            try
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                    return true;
                else
                {
                    foreach (var item in result.Errors)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return false;
        }
    }
}
