using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Repositories;
using TasarYeri.WEBUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Routing;

namespace TasarYeri.WEBUI.Areas.uye.Controllers
{


    [Area("uye"), Authorize(Roles = "uye")]
    public class HomeController : Controller
    { 
        Repository<Product> rProduct;
        Repository<Member> rMember;
        Repository<Category> rCategory;
        Repository<Seller> rSeller;
        Repository<Basket> rBasket;
        Repository<Image> rImage;
        MyContext myContext;


        public HomeController(Repository<Member> _rMember, Repository<Image> _rImage, Repository<Product> _rProduct, MyContext _myContext, Repository<Category> _rCategory, Repository<Seller> _rSeller, Repository<Basket> _rBasket)
        {
            rProduct = _rProduct;
            rMember = _rMember;
            rCategory = _rCategory;
            rSeller = _rSeller;
            rBasket = _rBasket;
            rImage = _rImage;
            myContext = _myContext;
        }
        //public IActionResult Index()
        //{
        //    return View();

        //}



        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Favorites()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(Basket cartObj, int id)
        {

            var product = rProduct.GetBy(c => c.ID == id);

            cartObj.ID = 0;

            cartObj.Product = product;

            cartObj.ProductID = product.ID;

            cartObj.Date = DateTime.Now;

            if (ModelState.IsValid)

            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "id"));

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(ERole), ERole.Member)));

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

                claimsPrincipal.AddIdentity(claimsIdentity);

                string uyeid = User.Claims.FirstOrDefault(f => f.Type == System.Security.Claims.ClaimTypes.Sid).Value;

                cartObj.MemberID = Convert.ToInt32(uyeid);

                rBasket.Update(cartObj);

                return RedirectToAction("MyCard", "Cart");
            }
            else
            {
                Basket basket = new Basket()
                {
                    Product = product,
                    ProductID = product.ID
                };
                return View(basket);
            };
        }

    public IActionResult Profil()
    {
        string uyeid = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value;
        return View(rMember.GetBy(r => r.ID.ToString() == uyeid));

    }
    //Kişisel Sayfam
    public IActionResult BirimGetir()
    {
        string uyeid = (User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
        Member members = rMember.Bul(Convert.ToInt32(uyeid));
        return View(members);
    }
    [HttpPost]
    public IActionResult BirimGetir(Member d)
    {
        string uyeid = (User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
        Member members = rMember.Bul(Convert.ToInt32(uyeid));
        members.Name = d.Name;
        members.LastName = d.LastName;
        members.Mail = d.Mail;
        //string filrnsmr = HttpContext.Request.Form.Files[1].FileName;
        //var yeniresimad = Guid.NewGuid() + filrnsmr;
        //var yuklenecekyer = Path.Combine(Directory.GetCurrentDirectory(),
        //    "wwwroot/img/memberimg" + yeniresimad);
        //var stream = new FileStream(yuklenecekyer, FileMode.Create);
        //HttpContext.Request.Form.Files[1].CopyTo(stream);
        //members.ImageWay = yeniresimad;
        rMember.Save();
        return RedirectToAction("UpdateImage");
    }

    public IActionResult UpdateImage()
    {
        string uyeid = (User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
        Member memb = rMember.Bul(Convert.ToInt32(uyeid));
        Image image = rImage.Bul(memb.ID);
        if (ModelState.IsValid)
        {
            foreach (var file in Request.Form.Files)
            {

                string filrnsmr = file.FileName.Replace(" ", "_");
                var yeniresimad = Guid.NewGuid() + filrnsmr;
                var yuklenecekyer = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/memberimg/" + yeniresimad);
                var stream = new FileStream(yuklenecekyer, FileMode.Create);
                file.CopyTo(stream);
                memb.ImageWay = yeniresimad;
                image.ImageWay = yeniresimad;
            }

        }
        rImage.Update(image);
        rMember.Update(memb);

        return RedirectToAction("BirimGetir");
    }

}


    }



