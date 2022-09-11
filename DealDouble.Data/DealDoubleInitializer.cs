using DealDouble.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Data
{
    public class DealDoubleInitializer : CreateDatabaseIfNotExists<DealDoubleDBContext>
    {
        protected override void Seed(DealDoubleDBContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        }

        public void SeedRoles(DealDoubleDBContext context)
        {
            List<IdentityRole> DealDoubleRoles = new List<IdentityRole>();
            DealDoubleRoles.Add(new IdentityRole() { Name = "Administrator" });
            DealDoubleRoles.Add(new IdentityRole() { Name = "User" });
            DealDoubleRoles.Add(new IdentityRole() { Name = "Moderator" });
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (IdentityRole role in DealDoubleRoles)
            {
                if (!roleManager.RoleExists(role.Name))
                {
                    var result = roleManager.Create(role);
                    if (result.Succeeded)
                    {
                        continue;
                    }
                }
            }
        }

        public void SeedUsers(DealDoubleDBContext context)
        {
            var usersStore = new UserStore<DealDoubleUser>(context);
            var userManager = new UserManager<DealDoubleUser>(usersStore);
            DealDoubleUser admin = new DealDoubleUser();
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin";
            var Passward = "admin123";

            if (userManager.FindByEmail(admin.Email) == null)
            {
                var result = userManager.Create(admin, Passward);

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Administrator");
                    userManager.AddToRole(admin.Id, "User");
                    userManager.AddToRole(admin.Id, "Moderator");
                }
            }
        }
    }
}
