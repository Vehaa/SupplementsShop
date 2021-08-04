using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class SupplementsContext:DbContext
    {

        public SupplementsContext(DbContextOptions<SupplementsContext>options):base(options)
        {

        }


        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductSubCategories> ProductSubCategories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Evaluations> Evaluations { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }

    }
}
