using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    /// <summary>
    /// Setup the data in the database just for testing purposes.
    /// Not for production.
    /// </summary>
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        /// <summary>
        /// Populates the platform data collection.
        /// </summary>
        /// <param name="context">PlatformService.Data.AppDbContext</param>
        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            //If there is no data available, then create data.
            if (!context.PlatForms.Any())
            {
                Console.WriteLine("...  Seeding Data ...");
                context.PlatForms.AddRange(
                    new Platform()
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                     new Platform()
                     {
                         Name = "SQL Server Express",
                         Publisher = "Microsoft",
                         Cost = "Free"
                     },
                     new Platform()
                     {
                         Name = "Kubernetes",
                         Publisher = "Cloud Native Computing Foundation",
                         Cost = "Free"
                     }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}