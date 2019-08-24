using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FseProjectManagement.Web.Extensions.Models
{
    public class TaskModel
    {
        [DataMember(Name = "id")]
        public int TaskId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [Required]
        public int Priority { get; set; }

        public int? ParentTaskId { get; set; }

        public string ParentTaskName { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public int StatusId { get; set; }
    }
}