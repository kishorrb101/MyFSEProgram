
using FseProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer
{
    public partial class FseProjectManagerDbContext : DbContext
    {
        public FseProjectManagerDbContext() :
          base("ConnectionString")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<FseProjectManagerDbContext, Migrations.Configuration>());
        }

        public static FseProjectManagerDbContext Create()
        {
            return new FseProjectManagerDbContext();
        }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<ProjectDetails> Projects { get; set; }
        public DbSet<ParentTaskDetails> ParentTasks { get; set; }
        public DbSet<TaskDetails> Tasks { get; set; }
    }
}
