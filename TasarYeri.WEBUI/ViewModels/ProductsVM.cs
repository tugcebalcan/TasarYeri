using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;

namespace TasarYeri.WEBUI.ViewModels
{
    public class ProductsVM : Controller
    {
        public List<Category> Categories { get; set; }
        public List<Category> SubCategories { get; set; } 
        public List<Member> Members { get; set; } 
        public List<Seller> Sellers { get; set; } 
        public List<Product> Products { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Basket> Baskets { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public Seller Seller { get; set; }
        public Member Member { get; set; }
        public Comment Comment { get; set; }
        public Basket Basket { get; set; }

    }
}
