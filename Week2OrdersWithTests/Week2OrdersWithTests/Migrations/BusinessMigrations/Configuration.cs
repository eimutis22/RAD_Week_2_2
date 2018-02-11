namespace Week2OrdersWithTests.Migrations.BusinessMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Week2OrdersWithTests.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week2OrdersWithTests.Models.BusinessDBContext>
    {
        Random rnd = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\BusinessMigrations";
        }

        protected override void Seed(Week2OrdersWithTests.Models.BusinessDBContext context)
        {
            SeedCustomers(context);
        }

        private void SeedCustomers(BusinessDBContext c)
        {
            c.Customers.AddOrUpdate(
              p => p.Name,
              new Customer { Name = "Andrew Peters", Address = "2 House Street", CreditRating = RndCredit(2000, 4000) },
              new Customer { Name = "Brice Lambson", Address = "5 House Lane", CreditRating = RndCredit(2000, 4000) },
              new Customer { Name = "Rowan Miller", Address = "6 House Town", CreditRating = RndCredit(2000, 4000) },
              new Customer { Name = "John Smith", Address = "3 Apartment Block", CreditRating = RndCredit(2000, 4000) }
            );
        }

        private float RndCredit(int a, int b)
        {
            return rnd.Next(a, b);
        }
    }
}
