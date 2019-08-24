using System;
using System.Collections;
using System.Collections.Generic;

namespace FseProjectManagement.Shared.Models
{
    public class ProjectDetails
    {
        public ProjectDetails()
        {
            this.Tasks = new HashSet<TaskDetails>();
        }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsSuspended { get; set; }

        public int Priority { get; set; }

        //Foreign key for user
        public int ManagerId { get; set; }

        public virtual UserDetails Manager { get; set; }

        public virtual ICollection<TaskDetails> Tasks { get; set; }
    }
}