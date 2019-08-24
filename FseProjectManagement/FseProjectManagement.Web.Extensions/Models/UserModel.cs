using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Web.Extensions.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public string DisplayName { get { return string.Join(" ", FirstName, LastName); } }
    }
}
