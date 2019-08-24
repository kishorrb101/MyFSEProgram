using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer.Interfaces
{
    public interface IUserDetailsRepository: IBaseRepository<UserDetails>
    {
        FilterReturnResult<UserDetails> Query(FilterConditions filterState);
    }
}
