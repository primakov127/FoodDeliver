using FoodDelivery.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("name=IdentityDbContext") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string userName = "admin";
            string password = "p@ssw0rd";
            string email = "admin@gmail.com";

            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                roleManager.CreateAsync(new AppRole("admin"));
            }

            if (!roleManager.RoleExistsAsync("cook").Result)
            {
                roleManager.CreateAsync(new AppRole("cook"));
            }

            if (!roleManager.RoleExistsAsync("call").Result)
            {
                roleManager.CreateAsync(new AppRole("call"));
            }

            AppUser user = userManager.FindByNameAsync(userName).Result;
            if (user != null)
            {
                userManager.CreateAsync(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByNameAsync(userName).Result;
            }

            if (!userManager.IsInRoleAsync(user.Id, "admin").Result)
            {
                userManager.AddToRoleAsync(user.Id, "admin");
            }
        }
    }
}
