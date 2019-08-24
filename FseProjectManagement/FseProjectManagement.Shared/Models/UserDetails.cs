using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Shared.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
            this.Tasks = new HashSet<TaskDetails>();
            this.Projects = new HashSet<ProjectDetails>();
        }
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmployeeId { get; set; }

        public virtual ICollection<TaskDetails> Tasks { get; set; }
        public virtual ICollection<ProjectDetails> Projects { get; set; }
    }
}
