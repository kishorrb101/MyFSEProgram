using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Web.Extensions.Models
{
    public class UserModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public string DisplayName { get { return string.Join(" ", FirstName, LastName); } }
    }
}
