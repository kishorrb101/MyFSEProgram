using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Extensions.Interface
{
    public interface ITaskControllerFacade
    {
        TaskModel Get(int Id);

        List<TaskModel> GetAll();

        TaskModel Update(TaskModel task);

        bool Delete(int id);

        FilterReturnResult<TaskModel> Query(FilterConditions filterState);

        bool UpdateTaskState(int taskId, int statusId);
    }
}
