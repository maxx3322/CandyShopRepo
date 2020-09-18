using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Services
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(); 
    }
}
