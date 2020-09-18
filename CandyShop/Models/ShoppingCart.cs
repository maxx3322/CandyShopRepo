using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class ShoppingCart
    {
        private readonly CandyShopDbContext _candyShopDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(CandyShopDbContext candyShopDbContext)
        {
            _candyShopDbContext = candyShopDbContext; 

        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext.Session;

            var context = services.GetService<CandyShopDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

        public void AddToCart(Candy candy, int amount)
        {
            var shoppingCartItem = _candyShopDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId); 

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Candy = candy,
                    Amount = amount

                };

                _candyShopDbContext.ShoppingCartItems.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.Amount++; 
            }

            _candyShopDbContext.SaveChanges();

        }

        public int RemoveFromCart(Candy candy)
        {
            var shoppingCartItem = _candyShopDbContext.ShoppingCartItems.SingleOrDefault(
               s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0; 

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;

                }
                else
                {
                    _candyShopDbContext.ShoppingCartItems.Remove(shoppingCartItem); 
                }

               

            }
            _candyShopDbContext.SaveChanges();
            return localAmount;

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _candyShopDbContext.ShoppingCartItems.Where(
                c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Candy)
                .ToList()); 
        }

        public void ClearCart()
        {
            var cartItems = _candyShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);
            _candyShopDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _candyShopDbContext.SaveChanges(); 
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _candyShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Candy.Price * c.Amount).Sum();
            return total; 

    }
    }

   
}
