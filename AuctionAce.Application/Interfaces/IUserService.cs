using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Application.Interfaces
{
    public interface IUserService
    {
        public Task<string> UserLogin(string email, string password);
    }
}
