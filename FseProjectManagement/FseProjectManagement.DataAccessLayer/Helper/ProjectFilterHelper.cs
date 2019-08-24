using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;

namespace DataAccess.Filters
{
    internal class ProjectFilterHelper
    {
        public void CompositeFilter(CompositeFilterDescriptor root, ref IQueryable<ProjectDetails> query)
        {
            var filters = FilterConditionHelper.FlattenCompositeFilterDescriptor(root);

            foreach (var f in filters)
                Filter(f, ref query);
        }

        public  void Filter(FilterDescriptor filter, ref IQueryable<ProjectDetails> query)
        {
           // base.Filter(filter, ref query);

            if (filter != null && !string.IsNullOrWhiteSpace(filter.Field) && !string.IsNullOrWhiteSpace(filter.Operator))
            {
                switch (filter.Field.Trim().ToLower())
                {
                    case "name":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.Contains)
                        {
                            query = query.Where(q => q.Name.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.Name == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "managerdisplayname":
                        if (filter.FilterOperator == FilterCriteriaDescriptor.Contains)
                        {
                            query = query.Where(q => q.Manager.FirstName.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.Manager.FirstName == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "priority":
                        int.TryParse(filter.Value.ToString(), out int filterValue);
                        if (filter.FilterOperator == FilterCriteriaDescriptor.GreaterThanEqual)
                        {
                            query = query.Where(q => q.Priority >= filterValue);
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.LessThanEqual)
                        {
                            query = query.Where(q => q.Priority <= filterValue);
                        }
                        else if (filter.FilterOperator == FilterCriteriaDescriptor.EqualTo)
                        {
                            query = query.Where(q => q.Priority == filterValue);
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                }
            }
        }
    }
}
