using System;
using System.Data.Entity;
using System.Linq;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.DataAccessLayer.Repositories;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using DataAccess.Filters;
using FseProjectManagement.DataAccessLayer.Actions;

namespace DataAccess.Repositories
{
    public class TaskRepository : BaseRepository<TaskDetails>, ITaskDetailsRepository
    {
        public FilterReturnResult<TaskDetails> Query(FilterConditions filterState)
        {
            var result = new FilterReturnResult<TaskDetails>();
            IQueryable<TaskDetails> query = context.Tasks.Include(t=>t.ParentTask);
            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new TaskFilterHelper();
                    if (filterState.Filter.Logic.ToLower() == "and")
                    {
                        filter.CompositeFilter(filterState.Filter, ref query);
                    }
                    else
                    {
                        throw new NotImplementedException("Logic not handled");
                    }
                }

                // Sorting
                if (filterState.Sort != null)
                {
                    foreach (var sort in filterState.Sort)
                    {
                        var purchaseOrderSort = new TaskSortAction();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    result.Data =  query
                                 .Skip(filterState.Skip)
                                 .Take(filterState.Take)
                                 .ToList(); 
                }
                else
                {
                    result.Data = query.ToList();
                }
            }
            else
            {
                result.Data = query.ToList();
            }

            // Get total records count
            result.Total = query.Count();

            return result;
        }
    }
}
