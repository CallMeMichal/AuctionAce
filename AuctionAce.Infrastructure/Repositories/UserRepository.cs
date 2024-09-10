using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> UserLoginAsync(string email, string password)
        {
            var userUser = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (userUser != null)
            {
                userUser.IsLogined = true;
                _context.SaveChanges();
            }
            return userUser;
        }

        public async Task<User> GetUserByIdAsync(int idUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == idUser);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> isLoginedAsync(int idUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == idUser);
            if (user != null)
            {
                if (user.IsLogined == true)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> LogoutUserAsync(int idUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == idUser);
            if (user != null)
            {
                if (user.IsLogined == true)
                {
                    user.IsLogined = false;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CreateUser(User user)
        {
            user.IsLogined = true;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}