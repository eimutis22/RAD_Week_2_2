namespace Week2OrdersWithTests.Migrations.BusinessMigrations
{
    using CsvHelper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Week2OrdersWithTests.Models;
    using Week2OrdersWithTests.Models.BusinessModels;

    internal sealed class Configuration : DbMigrationsConfiguration<Week2OrdersWithTests.Models.BusinessDBContext>
    {
        Random rnd = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\BusinessMigrations";
        }

        protected override void Seed(Week2OrdersWithTests.Models.BusinessDBContext c)
        {
            //SeedCustomers(c);
            //SeedProducts(c); -- Not enough fields in CSV file
            //AssignOrders(c); -- Cant assign to product that doesnt exist
        }

        private void AssignOrders(BusinessDBContext c)
        {
            // OrderLines
            c.OrderLines.AddOrUpdate(
              p => p.ID,
              new OrderLine { OrderID = 1, ProductID = 1 },
              new OrderLine { OrderID = 2, ProductID = 1 },
              new OrderLine { OrderID = 3, ProductID = 1 },
              new OrderLine { OrderID = 4, ProductID = 1 }
            );

            // Orders
            c.Orders.AddOrUpdate(
              p => p.ID,
              new Order { OrderDate = DateTime.Now, CustomerID = 1 },
              new Order { OrderDate = DateTime.Now, CustomerID = 2 },
              new Order { OrderDate = DateTime.Now, CustomerID = 3 },
              new Order { OrderDate = DateTime.Now, CustomerID = 4 }
            );
        }

        public void SeedProducts(BusinessDBContext ctx)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string resourceName = "Week2OrdersWithTests.Migrations.BusinessMigrations.Products.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.HasHeaderRecord = false;
                    var productData = csvReader.GetRecords<ProductDataImport>().ToArray();
                    foreach (var dataItem in productData)
                    {
                        ctx.Products.AddOrUpdate(c =>
                                new Product
                                {
                                    Description = dataItem.Description,
                                    UnitPrice = dataItem.UnitPrice,
                                    StockOnHand = dataItem.StockOnHand,
                                    ReorderLevel = 1, // Missing field in CSV file
                                    ReorderQuantity = 1 // Missing field in CSV file
                                });
                        // Missing field exception
                    }
                }
            }
            ctx.SaveChanges();
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
