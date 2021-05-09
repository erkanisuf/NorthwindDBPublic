using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class UserController : Controller
    {
        private readonly NorthwindDB _context;

        public UserController(NorthwindDB context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,PassWord")] Login newuser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
           
            return View(newuser);
        }

       
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,PassWord")] Login user)
        {
            
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var finduser = _context.Logins.Where(el=>el.UserName == user.UserName).FirstOrDefault();
            if (finduser == null)
            {
                ViewBag.Message = "User Not found !";
                
            }
            else {
                if (finduser.PassWord != user.PassWord)
                {
                    ViewBag.Message = "Wrong password!";

                }
                else {
                   
                    HttpContext.Session.SetString("user", user.UserName);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
            

        }
        
            [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Logged");
            return View();
            return RedirectToAction("Index", "Home"); 
        }

    }
}
