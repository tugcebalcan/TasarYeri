
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Repositories;
using TasarYeri.WEBUI.ViewModels;

namespace TasarYeri.WEBUI.Areas.uye
{
    [Area("uye"), Authorize(Roles = "uye")]


    public class CartController : Controller
    {
        Repository<Basket> rBasket;
        Repository<Member> rMember;
        Repository<Product> rProduct;
        Repository<ProductMember> rProductMember;
        Repository<Order> rOrder;
        Repository<Seller> rSeller;
        private object selectedProduct;

        public CartController(Repository<ProductMember> _rProductMember, Repository<Seller> _rSeller, Repository<Product> _rProduct, Repository<Order> _rOrder, Repository<Member> _rMember, Repository<Basket> _rBasket)

        {
            rBasket = _rBasket;
            rProductMember = _rProductMember;
            rOrder = _rOrder;
            rMember = _rMember;
            rProduct = _rProduct;
            rSeller = _rSeller;

        }

        public CartVM CartVM { get; set; }
        public IActionResult MyCard()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "ID"));

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(ERole), ERole.Seller)));

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            claimsPrincipal.AddIdentity(claimsIdentity);

            string uyeid = User.Claims.FirstOrDefault(f => f.Type == System.Security.Claims.ClaimTypes.Sid).Value;

            var fromClaim = Convert.ToInt32(uyeid);

            CartVM = new CartVM()

            {
                Order = new Order(),
                ListBasket = rBasket.GetAllLazy(u => u.MemberID == fromClaim, includeProperties: "Product").ToList()

            };

            //CartVM.Order.OrderTotal = 0;

            CartVM.Order.Member = rMember.GetBy(u => u.ID == fromClaim);

            foreach (var cart in CartVM.ListBasket)

            {
                cart.Price = GetPriceBaseOnQuantity(cart.Product);

                //CartVM.Order.OrderTotal += cart.Price;

                //cart.Product.Detail = ConvertToRawHtml(cart.Product.Detail);

                //if (cart.Course.Description.Length > 50)

                //{
                //    cart.Course.Description = cart.Course.Description.Substring(0, 49) + "...";
                //}
            }
            return View(CartVM);

        }

        public IActionResult RemoveFromCart(int productid)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "ID"));

            string uyeid = User.Claims.FirstOrDefault(f => f.Type == System.Security.Claims.ClaimTypes.Sid).Value;

            var selectedProduct = rBasket.GetBy(x => x.ProductID == productid && x.MemberID == Convert.ToInt32(uyeid));

            if (selectedProduct == null)
            {
                return NotFound();
            }
            selectedProduct.Count = 0;

            rBasket.Remove(selectedProduct);
            return RedirectToAction("MyCard");
        }



        private static double GetPriceBaseOnQuantity(Product product)
        {
            return product.Price;
        }

    }
}
