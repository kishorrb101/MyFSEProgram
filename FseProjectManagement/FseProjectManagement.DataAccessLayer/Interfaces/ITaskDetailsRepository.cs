using System;
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;

namespace DataAccess.Repositories.Intefaces
{
    public interface ITaskDetailsRepository : IBaseRepository<TaskDetails>
    {
        FilterReturnResult<TaskDetails> Query(FilterConditions filterState);
    }
}
