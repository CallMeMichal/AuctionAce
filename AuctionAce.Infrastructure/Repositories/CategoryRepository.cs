using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<int> GetCategoryIdByName(string categoryName)
        {
            var categoryId = await _context.Categories.FirstOrDefaultAsync(x=>x.Name == categoryName);
            return categoryId.Id;
        }

        public async Task<string> GetCategoryNameById(int? categoryId)
        {
            var categoryName = await _context.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId);
            return categoryName.Name;
        }
    }
}
