using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Repositories;
using System.Security.Claims;

namespace TasarYeri.WEBUI.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        Repository<Member> rMember;
        public UserViewComponent(Repository<Member> _rMember)
        {
            rMember = _rMember;
        }
        public IViewComponentResult Invoke()
        {
            var user = User as ClaimsPrincipal;
            string uyeid = Request.HttpContext.User.Claims.FirstOrDefault(f => f.Type == System.Security.Claims.ClaimTypes.Sid).Value;
            return View(rMember.GetBy(x => x.ID == Convert.ToInt32(uyeid)));

            //string uyeid = UserViewComponent.Claims.FirstOrDefault(f => f.Type == System.Security.Claims.ClaimTypes.Sid).Value;
            //return View(rMember.GetBy(x => x.ID == Convert.ToInt32(uyeid)));

        }
    }
}
