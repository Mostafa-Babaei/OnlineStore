using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using infrastructure.Models;
using infrastructure.Service;
using Application.Model;
using Domain.Entities;

namespace infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> Roles = new List<IdentityRole>();
            Roles.Add(new IdentityRole() { Name = "Administrator" });
            Roles.Add(new IdentityRole() { Name = "Customer" });

            foreach (var item in Roles)
                if (roleManager.Roles.All(r => r.Name != item.Name))
                    await roleManager.CreateAsync(item);

            var administrator = new ApplicationUser { UserName = "mostafababaee@gmail.com", Email = "mostafababaee@gmail.com", EmailConfirmed = true, RegisterAt = DateTime.Now, IsActive = true, Fullname = "مصطفی بابایی", Mobile = "09196226887" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "11223344");
                await userManager.AddToRolesAsync(administrator, new[] { "Administrator" });
            }
        }

        public static async Task SeedDefaultBankAsync()
        {

        }


    }
}
