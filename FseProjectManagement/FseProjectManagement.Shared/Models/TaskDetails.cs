using System;

namespace FseProjectManagement.Shared.Models
{
    public class TaskDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }

        public int StatusId { get; set; }

        public int? ParentTaskId { get; set; }

        public virtual ParentTaskDetails ParentTask { get; set; }

        public int OwnerId { get; set; }

        public virtual UserDetails Owner { get; set; }

        public int ProjectId { get; set; }

        public virtual ProjectDetails Project { get; set; }
    }
}