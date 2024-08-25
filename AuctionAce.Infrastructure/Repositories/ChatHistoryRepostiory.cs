using AuctionAce.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Infrastructure.Repositories
{
    public class ChatHistoryRepostiory
    {
        private readonly ApplicationDbContext _context;

        public ChatHistoryRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessage()
        {
        }
    }
}