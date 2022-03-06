using MVCapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCapp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        

      
        protected void Application_Start()
        {
            UserContext context = new UserContext();
            if(context.Roles.Count()==0)
            {
                List<UserRole> roles = new List<UserRole>
               {
                   new UserRole{RoleName="User"},
                   new UserRole{RoleName="Admin"},

               };

                context.Roles.AddRange(roles);
                context.SaveChanges();
                
            }
            if(context.Users.Count() == 0)
            {
                Users IntialAdminUser = new Users();
                IntialAdminUser.Name = "Indrajith";
                IntialAdminUser.Email = " indrajith@gmail.com";
                IntialAdminUser.Password = "abc@123";
                IntialAdminUser.RoleId = 1;
                context.Users.Add(IntialAdminUser);
                context.SaveChanges();
            };
            
           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
