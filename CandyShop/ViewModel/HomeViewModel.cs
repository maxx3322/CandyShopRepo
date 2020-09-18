using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.ViewModel
{
    public class HomeViewModel
    {
       public IEnumerable<Candy> CandyOnSale { get; set;}
    }
}
