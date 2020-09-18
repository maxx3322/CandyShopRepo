using CandyShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Services
{
    public class CandyRepository : ICandyRepository
    {
        private readonly CandyShopDbContext _candyShopDbContext;

        public CandyRepository(CandyShopDbContext candyShopDbContext)
        {
            _candyShopDbContext = candyShopDbContext;
        }
        public IEnumerable<Candy> GetAllCandy()
        {
           
            return _candyShopDbContext.Candies.Include(c => c.Category).ToList();

        }


        public IEnumerable<Candy> GetCandyOnSale()
        {
            return _candyShopDbContext.Candies.Include(c => c.Category).Where(p => p.IsOnSale);
        }
        public Candy GetCandyById(int candyId)
        {
            return _candyShopDbContext.Candies.FirstOrDefault(c => c.CandyId == candyId);
        }
    }
}
