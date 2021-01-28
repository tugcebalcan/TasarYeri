using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasarYeri.DAL.Entities;


namespace TasarYeri.WEBUI.ViewModels
{
        public class CartVM

        {

            public IEnumerable<Basket> ListBasket { get; set; }
            public IEnumerable<Product> Products { get; set; }

            public Order Order { get; set; } 
            public Product Product { get; set; } 
            public Seller Seller { get; set; } 
        }
 }
