using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Interfaces;

namespace Motorcycle_Group_Rides_Management_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateAsync(IdentityUser user)
        {
            await _userManager.CreateAsync(user);
        }


        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return true; 
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
