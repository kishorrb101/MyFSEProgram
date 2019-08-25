
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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FseProjectManagerDbContext, DataFeed.Configuration>());
        }

        public static FseProjectManagerDbContext Create()
        {
            return new FseProjectManagerDbContext();
        }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<ProjectDetails> Projects { get; set; }
        public DbSet<ParentTaskDetails> ParentTasks { get; set; }
        public DbSet<TaskDetails> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDetails>()
                .HasKey<int>(s => s.Id);

            modelBuilder.Entity<ParentTaskDetails>().HasKey<int>(s => s.Id);

            modelBuilder.Entity<TaskDetails>()
                .HasKey<int>(s => s.Id)
                .HasOptional<ParentTaskDetails>(s => s.ParentTask)
                .WithMany(p => p.Tasks)
                .HasForeignKey<int?>(t => t.ParentTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectDetails>()
              .HasKey<int>(s => s.Id)
              .HasRequired<UserDetails>(p => p.Manager)
              .WithMany(u => u.Projects)
              .HasForeignKey<int>(p => p.ManagerId)
              .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TaskDetails>()
            //    .HasKey<int>(s => s.TaskId)
            //    .HasRequired<ProjectDetails>(s => s.Project)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
