using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Shared
{
    public enum SortDirection
    {
        Undefined = 0,
        ASC = 1,
        DSC = 2
    }

    public enum TaskStatusEnum
    {
        Undefined,
        Pending,
        InProgress,
        Completed
    }
}
