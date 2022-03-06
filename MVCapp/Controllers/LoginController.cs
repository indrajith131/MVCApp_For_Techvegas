using MVCapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCapp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public UserContext Context;
        public ActionResult Register()
        {

            DisplayDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            try
            {
                DisplayDropDown();
                if (ModelState.IsValid)
                {   
                    using (Context = new UserContext())
                    {
                        
                        var isUserAlreadyExists = Context.Users.Any(x => x.Email == user.Email);
                        if(isUserAlreadyExists)
                        {
                            ModelState.AddModelError("Email", "User with this email already exists");
                            DisplayDropDown();
                            return View(user);
                        }
                        Context.Users.Add(user);
                        Context.SaveChanges();
                        TempData["msg"] = "<script>alert('Change succesfully');</script>";
                        ModelState.Clear();
                        
                    }
                    return RedirectToAction("Login", "Login");
                }
                
                return View(user);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error","Home");
            }
            
                
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                Context = new UserContext();

                if (ModelState.IsValid)

                {

                    var user = Context.Users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                    if (user != null)
                    {
                        var Ticket = new FormsAuthenticationTicket(login.Email, true, 3000);
                        string Encrypt = FormsAuthentication.Encrypt(Ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        if (user.RoleId == 1)
                        {
                            return RedirectToAction("AdminArea", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("UserArea", "User");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserId/Password");
                    }
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(login);
        }
        public ActionResult LogOff()
        {  
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        public void DisplayDropDown()
        {
            Context = new UserContext();
            var items = new SelectList(Context.Roles.ToList(), "RoleId", "RoleName");
            if (items != null)
            {
                ViewBag.RoleId = items;
            }
        }
    }
}