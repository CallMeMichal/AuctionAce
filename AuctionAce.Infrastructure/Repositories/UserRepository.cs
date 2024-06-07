

using AuctionAce.Infrastructure.Data.AuctionAceDbContext;

namespace AuctionAce.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AuctionAceContext _context;

        public UserRepository(AuctionAceContext context)
        {
            _context = context;
        }

        public async Task<string> UserLogin(string email, string password)
        {

            var userUser = _context.Users.Where(x => x.Email == email && x.Password == password && x.IdRoles == 1).FirstOrDefault();
            var userAuctioner = _context.Users.Where(x => x.Email == email && x.Password == password && x.IdRoles == 2).FirstOrDefault();

            if (userUser.IdRoles == 1)
                return "user";
            else if (userUser.IdRoles == 2)
                return "auctioner";
            else
                return "unknow";

        }
    }
}
