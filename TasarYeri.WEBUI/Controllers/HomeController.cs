using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Repositories;
using TasarYeri.DAL.Entities;
using TasarYeri.WEBUI.ViewModels;
using IdentityServer3.Core.ViewModels;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace TasarYeri.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        Repository<Member> rMember;
        Repository<Category> rCategory;
        Repository<Product> rProduct;
        Repository<Admin> rAdmin;
        Repository<Seller> rSeller;
        Repository<Comment> rComment;
        Repository<Image> rImage;
        MyContext myContext;
        public HomeController(Repository<Product> _rProduct, Repository<Image> _rImage, Repository<Admin> _rAdmin, Repository<Member> _rMember, Repository<Comment> _rComment, Repository<Category> _rcategory, Repository<Seller> _rSeller, MyContext _db)
        {
            rMember = _rMember;
            rAdmin = _rAdmin;
            myContext = _db;
            rCategory = _rcategory;
            rProduct = _rProduct;
            rSeller = _rSeller;
            rComment = _rComment;
            rImage = _rImage;
        }

        public IActionResult Index()
        {
            List<Product> products = myContext.Products.Include(x => x.Category).ToList();
            List<Category> categories = rCategory.GetAll().ToList();
            ProductsVM productVM = new ProductsVM { Products = products, Categories = categories };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Index(Product product)
        {
            rProduct.Add(product);
            return RedirectToAction("Index");
        }

        [Route("/Urunler/{name?}/{id?}/{catid?}")]
        public IActionResult ProductPage( int id)
        {
           
            Product product = rProduct.GetAll().Include(i => i.Category).FirstOrDefault(x => x.ID == id);
            int sellerid = product.SellerID;
            List<Comment> comments = rComment.GetAll(x => x.ProductID == id).ToList();
            Seller sellers = rSeller.GetBy(x => x.ID == sellerid);
            List<Product> urun = rProduct.GetAll(x => x.ID == id).Include(x => x.Seller).ToList();

            ProductsVM productsVM = new ProductsVM { Product = product, Seller = sellers, Category = product.Category, Comments = comments};

            return View(productsVM);
        }

        //[HttpPost, Route("/Urunler/{name?}/{id?}/{catid?}")]
        //public IActionResult ProductPage(string comment, int id)
        //{
        //    Comment newcomment = new Comment();
        //    int uyeid = Convert.ToInt32(User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
        //    Member members = rMember.GetBy(x => x.ID == uyeid);
        //    //Comment.Username = Member.Name + Member.Lastanem  
        //    newcomment.MemberID = uyeid;
        //    newcomment.UserName = members.Name + members.LastName;
        //    newcomment.Comments = comment;
        //    newcomment.Date = DateTime.Now;
        //    newcomment.ProductID = id;
        //    rComment.Add(newcomment);
        //    return RedirectToAction("ProductPage", new { id });
        //} 

        public IActionResult YeniUye()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> YeniUyeAsync(Member member)
        {
            member.ImageWay= "17.png";
            Image image = new Image();
            
            rMember.Add(member);
            image.MemberID = member.ID;
            image.ImageWay = "17.png";
            rImage.Add(image);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("TasarYeri");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, member.Mail));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, member.ID.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "uye")); //Enum.GetName(typeof(ERole), ERole.uye))
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "satici")); //Enum.GetName(typeof(ERole), ERole.uye))
                                                                           //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,"admin"));

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsPrincipal), new AuthenticationProperties() { IsPersistent = true });
            return RedirectToAction("Index");
        }


        public IActionResult Products(int id)
        {
            List<Product> products = rProduct.GetAll(x => x.CategoryID == id).ToList();
            Category category = rCategory.GetBy(x=>x.ID == id);
            ProductsVM productVM = new ProductsVM { Products = products, Category = category };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult YorumYap(string comment,int id)
        {

            int uyeid = Convert.ToInt32(User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
            Member members = rMember.GetBy(x=>x.ID == uyeid);
            //model.Comment.MemberID = members.ID;
            string productname = rProduct.GetBy(x => x.ID == id).Name;
            rComment.Add(new Comment { Comments=comment,ProductID=id,MemberID= uyeid,Date=DateTime.Now, UserName = members.Name + members.LastName });
            return RedirectToAction("ProductPage", new { id= id, name = "s", catid = 1} );
        }

        [Route("/Satici/{name?}/{id?}")]
        public IActionResult SatıcıProfil(int id)
        {
           
            Seller seller = rSeller.GetBy(x => x.ID == id);
            List<Product> products = rProduct.GetAll(x => x.SellerID == seller.ID).ToList();
            List<Category> categories = rCategory.GetAll(x => x.ParentID != null).ToList();
            ProductsVM productVM = new ProductsVM { Products = products, Categories = categories, Seller = seller };

            return View(productVM);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/giris")]
        public IActionResult Giris(string ReturnUrl)
        {
            // Login sayfası
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost, Route("/giris")]
        public async Task<IActionResult> Giris(Member member, string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl) && ReturnUrl.Contains("Admin"))
            {
                Admin admin = rAdmin.GetBy(a => a.Mail == member.Mail && a.Password == member.Password) ?? null;

                if (admin != null)
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity("AdminIdentity");
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, admin.Mail));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(ERole), ERole.Admin)));

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                    claimsPrincipal.AddIdentity(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsPrincipal), new AuthenticationProperties() { IsPersistent = true });
                    return RedirectToAction("AdminSayfa", "Home", new { area = "Admin" });
                }
                else
                {
                    ViewBag.Hata = "Admin adı veya Şifre Hatalı";
                    return View();
                }

            }
            
            Member uye = rMember.GetBy(f => f.Mail == member.Mail && f.Password == member.Password) ?? null;
            if (uye != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("TasarYeri");
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, uye.Mail));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, uye.ID.ToString()));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "uye")); //Enum.GetName(typeof(ERole), ERole.uye))
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "satici")); //Enum.GetName(typeof(ERole), ERole.uye))
                                                                               //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,"admin"));

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsPrincipal), new AuthenticationProperties() { IsPersistent = true });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Hata = "Mail Adresi veya Şifre Hatalı";
                return View();
            }
            ViewBag.Hata = "Mail Adresi veya Şifre Hatalı";
            return View();
        }



        [Route("/cikis")]
        public IActionResult Cikis()
        {
            return View();
        }


        [Route("/yetkilendirme")]
        public async Task<IActionResult> AccessDenied()
        {
            if (HttpContext.User.Identity.IsAuthenticated) await HttpContext.SignOutAsync();
            return Redirect("/giris");
        }

    }
}

