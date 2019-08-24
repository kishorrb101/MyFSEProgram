using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FseProjectManagement.DataAccessLayer.DataFeed
{
    internal sealed class Configuration : DbMigrationsConfiguration<FseProjectManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FseProjectManagerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            ProjectManagerInitializer.Seed(context);
        }
    }
}
