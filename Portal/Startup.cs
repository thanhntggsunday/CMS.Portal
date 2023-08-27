using System;
using System.Data.Entity;
using Common.Classes;
using Common.Model;
using Common.Model.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portal.Startup))]
namespace Portal
{
    // --------------- READ ME -----------------------------
    // sqllocaldb stop mssqllocaldb
    // sqllocaldb delete mssqllocaldb
    // sqllocaldb start "MSSQLLocalDB"


    public partial class Startup
    {
        public readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();

            // Logging:
            log.Info("Web App Started");
        }
        private void CreateRolesandUsers()
        {
            CommonDbContext context = new CommonDbContext();

            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
            var UserManager = new UserManager<AppUser>(new UserStore<AppUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("ADMIN"))
            {
                // first we create Admin rool
                var role = new AppRole();
                role.Name = "ADMIN";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website

                var user = new AppUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";

                string userPWD = "abcde.A1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "ADMIN");
                }
            }
            else
            {
                //Here we create a Admin super user who will maintain the website

                var user = new AppUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";

                string userPWD = "abcde.A1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "ADMIN");
                }
            }

            // creating Creating Manager role
            if (!roleManager.RoleExists("MANAGER"))
            {
                var role = new AppRole();
                role.Name = "MANAGER";
                roleManager.Create(role);
            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("EMPLOYEE"))
            {
                var role = new AppRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            // Create customer:

            var customer = new AppUser();
            customer.UserName = "customer@gmail.com";
            customer.Email = "customer@gmail.com";

            string pwd = "abcde.A1";

            var result = UserManager.Create(customer, pwd);


        }
    }
}
