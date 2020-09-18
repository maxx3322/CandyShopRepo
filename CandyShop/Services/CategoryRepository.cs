using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Services
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly CandyShopDbContext _candyShopDbContext;

        public CategoryRepository(CandyShopDbContext candyShopDbContext)
        {
            _candyShopDbContext = candyShopDbContext;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _candyShopDbContext.Categories.ToList(); 
           
        }
    } 
}
