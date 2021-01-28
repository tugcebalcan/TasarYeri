using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using TasarYeri.DAL.Contexts;
using TasarYeri.WEBUI.ViewModels;
using IdentityServer3.Core.ViewModels;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace TasarYeri.WEBUI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        Repository<Member> rMember;
        Repository<Category> rCategory;
        MyContext myContext;
        public HeaderViewComponent(MyContext _myContext, Repository<Member> _rMember, Repository<Category> _rCategory)
        {
            rMember = _rMember;
            rCategory = _rCategory;
            myContext = _myContext;
        }

        public IViewComponentResult Invoke()
        {
            
            return View(myContext.Categories.Include(i => i.SubCategories).ToList());

        }
    }
}
 