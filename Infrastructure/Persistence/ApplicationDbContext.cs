using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using infrastructure.Models;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

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

        #endregion

        #region Shop

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion

        #region Order
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShopOrder> Orders { get; set; }

        #endregion



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
