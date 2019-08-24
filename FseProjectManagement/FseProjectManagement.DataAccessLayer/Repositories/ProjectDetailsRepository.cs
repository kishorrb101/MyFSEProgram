using System;
using System.Data.Entity;
using System.Linq;
using DataAccess.Filters;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.DataAccessLayer.Actions;
using FseProjectManagement.DataAccessLayer.Repositories;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;

namespace DataAccess.Repositories
{
    public class ProjectDetailsRepository : BaseRepository<ProjectDetails>, IProjectDetailsRepository
    {
        public FilterReturnResult<ProjectDetails> Query(FilterConditions filterState)
        {
            var result = new FilterReturnResult<ProjectDetails>();
            IQueryable<ProjectDetails> query = context.Projects;
            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new ProjectFilterHelper();
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
                        var purchaseOrderSort = new ProjectSortAction();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    result.Data = query
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
