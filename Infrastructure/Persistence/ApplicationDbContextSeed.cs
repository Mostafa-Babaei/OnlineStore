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
using Microsoft.Extensions.DependencyInjection;
using infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            string[] roles = new string[] { "Admin", "Customer" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            var user = new ApplicationUser
            {
                Fullname = "XXXX",
                Email = "mostafababaee@gmail.com",
                NormalizedEmail = "MOSTAFABABAEE@GMAIL.COM",
                UserName = "mostafababaee@gmail.com",
                NormalizedUserName = "MOSTAFABABAEE@GMAIL.COM",
                PhoneNumber = "09196226887",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                RegisterAt = DateTime.Now,
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);

            }

            AssignRoles(serviceProvider, user.Email, roles);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> Roles = new List<IdentityRole>();
            Roles.Add(new IdentityRole() { Name = "Admin" });
            Roles.Add(new IdentityRole() { Name = "Customer" });

            foreach (var item in Roles)
                if (roleManager.Roles.All(r => r.Name != item.Name))
                    await roleManager.CreateAsync(item);

            var administrator = new ApplicationUser { UserName = "mostafababaee@gmail.com", Email = "mostafababaee@gmail.com", EmailConfirmed = true, RegisterAt = DateTime.Now, IsActive = true, Fullname = "مصطفی بابایی", Mobile = "09196226887" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "11223344");
                await userManager.AddToRolesAsync(administrator, new[] { "Admin" });
            }
        }

        public static async Task SeedDefaultBankAsync()
        {

        }


    }
}
