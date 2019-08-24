using System;
using System.Collections.Generic;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Extensions.Interface
{
    public interface IProjectControllerFacade
    {
        ProjectModel Get(int Id);

        List<ProjectModel> GetAll();

        ProjectModel Update(ProjectModel user);

        bool Delete(int id);

        bool UpdateProjectState(int projectId, bool isSuspended);

        FilterReturnResult<ProjectModel> Query(FilterConditions filterState);
    }
}
