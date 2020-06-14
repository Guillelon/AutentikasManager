namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.autentikasDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Models.autentikasDBContext context)
        {
            if(!context.Package.Any())
            {
                context.Package.Add(new Models.Package
                {
                    Name = "Bolsa de 2",
                    Description = "Bolsa de 2",
                    CookiesQuantity = 2,
                    Price = 1.25m,
                    Cost = 1.00m
                });
                context.Package.Add(new Models.Package
                {
                    Name = "Bolsa de 4",
                    Description = "Bolsa de 4",
                    CookiesQuantity = 4,
                    Price = 2.25m,
                    Cost = 2.00m
                });
                context.Package.Add(new Models.Package 
                {
                    Name = "Caja de 6",
                    Description = "Caja de 6",
                    CookiesQuantity = 6,
                    Price = 4.25m,
                    Cost = 4.00m
                });
                context.Package.Add(new Models.Package
                {
                    Name = "Caja de 8",
                    Description = "Caja de 8",
                    CookiesQuantity = 8,
                    Price = 6.25m,
                    Cost = 6.00m
                });
                context.Package.Add(new Models.Package
                {
                    Name = "Cerealito",
                    Description = "Cerealito",
                    CookiesQuantity = 0,
                    Price = 6.25m,
                    Cost = 6.00m
                });
                context.SaveChanges();
            }
        }
    }
}
