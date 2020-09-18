using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using CandyShop.Services;
using CandyShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    public class CandyController : Controller
    {

        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository; 

        public CandyController(ICandyRepository candyRepository, ICategoryRepository categoryRepository)
        {
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository; 
        }


        public IActionResult List(string category)
        {

            IEnumerable<Candy> candies;
            string currentCategory; 

            if (string.IsNullOrEmpty(category))
            {
                candies = _candyRepository.GetAllCandy().OrderBy(c => c.CandyId);
                currentCategory = "All Candy"; 
            }

            else
            {
                candies = _candyRepository.GetAllCandy().Where(c => c.Category.CategoryName == category);

                currentCategory = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.CategoryName == category)?.CategoryName; 
            }

            var categoryView = new CandyListViewModel
            {
                Candies = candies,
                CurrentCategory = currentCategory
            };

            return View(categoryView); 


           
        }

        public IActionResult Details(int candyid)
        {
            var candy = _candyRepository.GetCandyById(candyid);
                if(candy == null)
            {
                return NotFound();
            }
            return View(candy); 
        }


    }
}
