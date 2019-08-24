
using FseProjectManagement.DataAccessLayer.Actions;
using FseProjectManagement.DataAccessLayer.Helper;
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer.Repositories
{
    
    public class UserDetailsRepository : BaseRepository<UserDetails>, IUserDetailsRepository
    {
        public FilterReturnResult<UserDetails> Query(FilterConditions filterState)
        {
            var result = new FilterReturnResult<UserDetails>();
            IQueryable<UserDetails> query = context.Users;
            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new UserFilterHelper();
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
                        var purchaseOrderSort = new UserSortAction();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    var x = query
                                 .Skip(filterState.Skip)
                                 .Take(filterState.Take)
                                 .ToList();
                    result.Data = x;
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
