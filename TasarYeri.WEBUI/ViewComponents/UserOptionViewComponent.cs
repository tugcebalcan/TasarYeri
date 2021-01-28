using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Repositories;

namespace TasarYeri.WEBUI.ViewComponents
{
    public class UserOptionViewComponent : ViewComponent
    {
        Repository<Member> rMember;
        Repository<Seller> rSeller;
        public UserOptionViewComponent(Repository<Seller> _rSeller, Repository<Member> _rMember)
        {
            rMember = _rMember;
            rSeller = _rSeller;
        }
        
        [HttpPost]
        public IViewComponentResult Invoke(Seller seller)
        {
            
            rSeller.Add(seller);
            return View();
        }
    }
}

