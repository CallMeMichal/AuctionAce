using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class CategoryService
    {
        public readonly CategoryRepository _categoryRespository;

        public CategoryService(CategoryRepository categoryRespository)
        {
            _categoryRespository = categoryRespository;
        }

        public async Task<List<CategoriesDomain>> GetCategories()
        {
            List<CategoriesDomain> categoriesDomain = new List<CategoriesDomain>();
            var categories = await _categoryRespository.GetCategories();

            foreach (var category in categories)
            {
                categoriesDomain.Add(new CategoriesDomain
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }
            return categoriesDomain;
        }

        /*public async Task<int> GetCategoryIdByName(string categoryName)
        {
            var categoryId = await _categoryRespository.GetCategoryIdByName(categoryName);
            return categoryId;
        }*/

        public async Task<string> GetCategoryNameById(int? categoryId)
        {
            var categoryName = await _categoryRespository.GetCategoryNameById(categoryId);
            return categoryName;
        }

    }
}
