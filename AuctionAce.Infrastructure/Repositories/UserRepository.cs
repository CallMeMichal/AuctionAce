using AuctionAce.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AuctionAceDbContext _context;

        public UserRepository(AuctionAceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserLogin(string email, string password)
        {

            var user = _context.Users.Where(x => x.Email == email).Where(x => x.Password == password).FirstOrDefault();
            
            if(user == null)
                return false;
            else
                return true;

        }
    }
}
