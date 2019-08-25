using System;
using System.Collections.Generic;

namespace FseProjectManagement.Shared.Models
{
    public class ParentTaskDetails
    {
        public ParentTaskDetails()
        {
            this.Tasks = new HashSet<TaskDetails>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskDetails> Tasks { get; set; }
    }
}