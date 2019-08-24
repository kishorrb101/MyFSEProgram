using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FseProjectManagement.Web.Extensions.Models
{
    public class ParentTaskModel
    {
        public int ParentTaskId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}