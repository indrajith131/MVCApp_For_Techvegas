using MVCapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCapp.Controllers
{
    public class RoleController : Controller
    {   public readonly UserContext Context = new UserContext();
        // GET: Role
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        { 
           var roles = Context.Roles.ToList();
            return View(roles);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var role = new UserRole();
            return View(role);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(UserRole Role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var IsRoleAlreadyPresent = Context.Roles.Any(x => x.RoleName.ToLower() == Role.RoleName.ToLower());
                    if (IsRoleAlreadyPresent)
                    {
                        ModelState.AddModelError("", "Role Already Exists");
                        return View(Role);
                    }
                    Context.Roles.Add(Role);
                    Context.SaveChanges();
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}