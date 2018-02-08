using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Week2OrdersWithTests.Models
{
    public class BusinessDBContext : DbContext
    {
        public BusinessDBContext() : base("Week2OrdersWithTests")
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}


