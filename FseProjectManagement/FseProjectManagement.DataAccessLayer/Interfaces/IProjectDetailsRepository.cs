
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;

namespace DataAccess.Repositories.Intefaces
{
    public interface IProjectDetailsRepository : IBaseRepository<ProjectDetails>
    {
        FilterReturnResult<ProjectDetails> Query(FilterConditions filterState);
    }
}
