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
        public async Task<IActionResult> Create([Bind("UserName,PassWord,Email")] User newuser)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", newuser);
            }
            // Check if username and email already is in DB;
            var dupeName = _context.User.Any(el => el.UserName == newuser.UserName);
            var dupeEmail =  _context.User.Any(el => el.Email == newuser.Email);
            if (dupeName) {
                
                ViewBag.Result = $"User with username {newuser.UserName}  already exists!";
                return View("Register", newuser);
            }
            if (dupeEmail) {
                ViewBag.Result = $"Email:  {newuser.Email}  already exists!";
                return View("Register", newuser);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newuser.PassWord); // Bcrypt password hashing and later in login Verifying
            newuser.PassWord = hashedPassword;
            if (ModelState.IsValid)
            {
                var result = _context.Add(newuser);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("user", newuser.UserName);
                return RedirectToAction("Index", "Home");
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
           
            var finduser = _context.User.SingleOrDefault(x => x.UserName == user.UserName);
            var decodePassword =  BCrypt.Net.BCrypt.Verify(user.PassWord, finduser.PassWord);
            if (finduser == null || !decodePassword)
            {
                ViewBag.Message = "Wrong password or Email";

            }

            else {
                HttpContext.Session.SetString("user", user.UserName);
                return RedirectToAction("Index", "Home");

            }
            return View();
            

        }
        
            [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOut()
        {
            // Clears Session and cookies.
            HttpContext.Session.Clear();
           Response.Cookies.Delete("Logged");
           
            return RedirectToAction("Index", "Home"); 
        }

    }
}
