using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Repositories;

namespace TasarYeri.WEBUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        Repository<Category> _rCategory;
        public ProductsViewComponent(Repository<Category> rCategory)
        {
            _rCategory = rCategory;
        }
        public IViewComponentResult Invoke()
        {

            return View(_rCategory.GetAll());

        }
    }
}
