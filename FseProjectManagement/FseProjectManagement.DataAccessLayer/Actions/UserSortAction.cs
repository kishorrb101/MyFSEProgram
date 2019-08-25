
using FseProjectManagement.Shared;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using System.Linq;

namespace FseProjectManagement.DataAccessLayer.Actions
{
    internal class UserSortAction
    {
        public void Sort(FilterSortDescriptor sort, ref IQueryable<UserDetails> query)
        {
            if (sort != null && !string.IsNullOrWhiteSpace(sort.Field) && !string.IsNullOrWhiteSpace(sort.Dir))
            {
                //base.Sort(sort, ref query);

                switch (sort.Field.Trim().ToLower())
                {
                    case "id":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(p => p.Id);
                        }
                        else
                        {
                            query = query.OrderByDescending(p => p.Id);
                        }

                        break;

                    case "firstname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.FirstName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.FirstName);
                        }
                        break;

                    case "lastname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.LastName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.LastName);
                        }
                        break;
                    case "employeeid":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.EmployeeId);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.EmployeeId);
                        }
                        break;
                }
            }
        }
    }
}