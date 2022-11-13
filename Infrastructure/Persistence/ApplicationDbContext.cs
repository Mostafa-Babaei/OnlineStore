using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using infrastructure.Models;
using Domain.Models;

namespace infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        #region Public
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MainMenu> MainMenus { get; set; }

        #endregion




        #region Shop

        public DbSet<Product> Products { get; set; }

        #endregion


        #region Order
        public DbSet<Order> Orders{ get; set; }
        #endregion


        public DbSet<MainMenu> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
