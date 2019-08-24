using FseProjectManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer.DataFeed
{
    public class ProjectManagerInitializer
    {
        public static void Seed(FseProjectManagerDbContext context)
        {
            var users = new List<UserDetails>
            {
            new UserDetails{UserId = 1, FirstName="FirstName_1",LastName="LastName_1", EmployeeId = "1" },
            new UserDetails{UserId = 2, FirstName="FirstName_2",LastName="LastName_2", EmployeeId = "2"},
            new UserDetails{UserId = 3, FirstName="FirstName_3",LastName="LastName_3" ,EmployeeId = "3"},
            new UserDetails{UserId = 4, FirstName="FirstName_4",LastName="LastName_4" ,EmployeeId = "4"},
            };

            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            var projects = new List<ProjectDetails>
            {
            new ProjectDetails { ProjectId =1, Name = "Project_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1},
            new ProjectDetails { ProjectId = 2, Name = "Project_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1 },
            new ProjectDetails { ProjectId = 3, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2, ManagerId = 1 },
            new ProjectDetails { ProjectId =4, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 0, ManagerId = 1 },
            };
            projects.ForEach(p => context.Projects.AddOrUpdate(p));
            context.SaveChanges();

            var parentTask = new List<ParentTaskDetails>
            {
            new ParentTaskDetails { ParentTaskId =1, Name = "ParentTask_1"},
            new ParentTaskDetails { ParentTaskId =2, Name = "parentTask_2" },
            };
            parentTask.ForEach(pt => context.ParentTasks.AddOrUpdate(pt));
            context.SaveChanges();

            var tasks = new List<TaskDetails>
            {
            new TaskDetails { TaskId = 1, Name = "Task_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1},
            new TaskDetails { TaskId = 2, Name = "Task_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1},
            new TaskDetails { TaskId = 2, Name = "Task_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new TaskDetails { TaskId = 4, Name = "Task_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1 },
            };
            tasks.ForEach(t => context.Tasks.AddOrUpdate(t));
            context.SaveChanges();
        }
    }
}
