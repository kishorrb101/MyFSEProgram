using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FseProjectManagement.Web.Extensions.Models
{
    public class ParentTaskModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}