using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CandyShop.Models;
using CandyShop.Services;
using CandyShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ICandyRepository candyRepository, ShoppingCart shoppingCart)
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            var shoppingcartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingcartViewModel);



        }

        public RedirectToActionResult AddCandyToCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy().FirstOrDefault(c => c.CandyId == candyId);

            if (selectedCandy != null)
            {
                _shoppingCart.AddToCart(selectedCandy, 1);
            }

            return RedirectToAction("Index");


        }
        public RedirectToActionResult RemoveFromShoppingCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy().FirstOrDefault(c => c.CandyId == candyId);

            if (selectedCandy != null)
            {
                _shoppingCart.RemoveFromCart(selectedCandy);
            }

            return RedirectToAction("Index");
        }
    }
}
