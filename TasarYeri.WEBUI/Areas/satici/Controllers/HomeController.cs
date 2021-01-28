using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Repositories;
using TasarYeri.WEBUI.ViewModels;

namespace TasarYeri.WEBUI.Areas.satici.Controllers
{
    //public class HomeController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}


    [Area("satici"), Authorize(Roles = "satici")]
    public class HomeController : Controller
    {
        Repository<Member> rMember;
        Repository<Category> rCategory;
        Repository<Product> rProduct; 
        Repository<Seller> rSeller;
        MyContext myContext;
        public HomeController(Repository<Product> _rProduct, Repository<Seller> _rSeller, Repository<Member> _rMember, Repository<Category> _rCategory)
        {
            rSeller = _rSeller;
            rMember = _rMember;
            rProduct = _rProduct;
            rCategory = _rCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SatıcıSayfası()
        {
            int uyeid = Convert.ToInt32(User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
            Seller member = rSeller.GetBy(x => x.MemberID == uyeid);
            List<Product> products = rProduct.GetAll(x => x.SellerID == member.ID).ToList();
            List<Category> categories = rCategory.GetAll(x=>x.ParentID != null).ToList();
            ProductsVM productVM = new ProductsVM { Products = products, Categories = categories};

            return View(productVM);
        }
        [HttpPost]
        public IActionResult SatıcıSayfası(Product product)
        {
            if (ModelState.IsValid)
            {
                Product urun = new Product();
                if (product.Picture == null)
                {
                    for (int i = 0; i < HttpContext.Request.Form.Files.Count; i++)
                    {
                        string filrnsmr = HttpContext.Request.Form.Files[i].FileName;
                        var yeniresimad = Guid.NewGuid() + filrnsmr;
                        var yuklenecekyer = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/productimg/" + yeniresimad);
                        var stream = new FileStream(yuklenecekyer, FileMode.Create);
                        HttpContext.Request.Form.Files[i].CopyTo(stream);
                        urun.PictureWay = yeniresimad;
                    }
                }
                int uyeid = Convert.ToInt32(User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
                Seller seller = rSeller.GetBy(x => x.MemberID == uyeid);
                urun.SellerID = seller.ID;
                urun.CategoryID = product.CategoryID;
                urun.Category = rCategory.GetBy(x => x.ID == product.CategoryID);
                urun.Name = product.Name;
                urun.Price = product.Price;
                urun.Detail = product.Detail;
                rProduct.Add(urun);

                return RedirectToAction("SatıcıSayfası");
            }

            return View(product);
           
        }
         public IActionResult Urunlerim()
        {
            List<Product> products = myContext.Products.Include(x => x.Category).ToList();
            List<Category> categories = rCategory.GetAll().ToList();
            ProductsVM productVM = new ProductsVM { Products = products, Categories = categories };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Urunlerim( Product product)
        {
            
            rProduct.Add(product);
            return RedirectToAction("SatıcıSayfası");
        }
    
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult RolAta(int id)
        {
            Member member = rMember.GetBy(x => x.ID == id);
            member.RoleNumber = 2;
            rMember.Save();
            return RedirectToAction("SaticiKayit");
        }
        public IActionResult SaticiKayit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaticiKayit(Seller seller)
        {
            int uyeid = Convert.ToInt32(User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
            Member mer = rMember.GetBy(x => x.ID == uyeid);
            seller.MemberID = mer.ID;
            seller.LastName = mer.LastName;
            seller.Mail = mer.Mail;
            rSeller.Add(seller);
            return RedirectToAction("SatıcıSayfası", "Home", new { area = "satici" });
        }

       
        
    }
}
