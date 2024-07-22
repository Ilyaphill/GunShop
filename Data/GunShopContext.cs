using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GunShop.Models;

namespace GunShop.Data
{
    public class GunShopContext : DbContext
    {
        public GunShopContext (DbContextOptions<GunShopContext> options)
            : base(options)
        {
        }

        public DbSet<GunShop.Models.Gun> Gun { get; set; } = default!;
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
