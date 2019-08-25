using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer.Helper
{
    internal class UserFilterHelper
    {
        public void CompositeFilter(CompositeFilterDescriptor root, ref IQueryable<UserDetails> query)
        {
            var filters = FilterConditionHelper.FlattenCompositeFilterDescriptor(root);

            foreach (var f in filters)
                Filter(f, ref query);
        }

        public  void Filter(FilterDescriptor filter, ref IQueryable<UserDetails> query)
        {
           // base.Filter(filter, ref query);

            if (filter != null && !string.IsNullOrWhiteSpace(filter.Field) && !string.IsNullOrWhiteSpace(filter.Operator))
            {
                switch (filter.Field.Trim().ToLower())
                {
                    case "id":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.Id.ToString().ToLower() == filter.Value.ToString().ToLower());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "firstname":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.Contains)
                        {
                            query = query.Where(q => q.FirstName.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.FirstName == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "lastname":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.Contains)
                        {
                            query = query.Where(q => q.LastName.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.LastName == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "employeeid":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.Contains)
                        {
                            query = query.Where(q => q.EmployeeId.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.EmployeeId == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                }
            }
        }
    }
}
