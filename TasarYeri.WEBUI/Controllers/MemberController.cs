using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasarYeri.DAL.Entities;
using TasarYeri.DAL.Contexts;
using TasarYeri.DAL.Repositories;

namespace TasarYeri.WEBUI.Controllers
{
    public class MemberController : Controller
    {
        
        Repository<Member> rMember;
        public MemberController(Repository<Member> _rMember)
        {
            rMember = _rMember;
        }
        public IActionResult Index()
        {
           
            return View(rMember.GetAll());
        }
       

        //public IActionResult UyeSil(int id)
        //{
        //    var uyelik = c.Member.Find(id);
        //    c.Member.Remove(uyelik);
        //    c.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //public IActionResult UyeGetir(int id)
        //{
        //    var uye = c.Member.Find(id);//idnin olduğu satırı komple tut
        //    return View("UyeGetir", uye);
        //}

       
        //public IActionResult UyeGuncelle(Member u)
        //{

        //    var uye = c.Member.Find(u.ID);
        //    uye.Name = u.Name;
        //    c.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}