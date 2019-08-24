using FseProjectManagement.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FseProjectManagement.Shared.SearchFilters.FilterCriteria
{
    public static class FilterConditionHelper
    {
        #region Extension Methods

        public static string ToOperatorStr(this FilterCriteriaDescriptor value)
        {
            switch (value)
            {
                case FilterCriteriaDescriptor.Contains: return "contains";

                case FilterCriteriaDescriptor.LessThanEqual: return "lte";

                case FilterCriteriaDescriptor.GreaterThanEqual: return "gte";

                case FilterCriteriaDescriptor.EqualTo: return "eq";

                default:
                    throw new NotImplementedException($"[{value.ToString()}] Operator not handled");
            }
        }

        public static void AddFilter(this FilterConditions FilterConditions, string fieldName, object value, FilterCriteriaDescriptor fieldOperator)
        {
            if (FilterConditions.Filter == null)
            {
                FilterConditions.Filter = new CompositeFilterDescriptor()
                {
                    Logic = "and",
                    Filters = new List<dynamic>()
                };
            }

            var found = SearchFilterDescriptors(FilterConditions.Filter, fieldName).FirstOrDefault();
            if (found != null)
            {
                throw new InvalidOperationException($"filter [${fieldName}] already exists");
            }

            var filterJson = new FilterDescriptor()
            {
                Field = fieldName,
                Value = value,
                Operator = fieldOperator.ToOperatorStr(),
            }.ToJson();

            FilterConditions.Filter.Filters.Add(filterJson);
        }

        #endregion Extension Methods

        public static IEnumerable<FilterDescriptor> SearchFilterDescriptors(CompositeFilterDescriptor root, string fieldName)
        {
            if (root == null) return new List<FilterDescriptor>();

            return FlattenCompositeFilterDescriptor(root)
                .Where(x => x.Field.Trim().ToLower() == fieldName.Trim().ToLower());
        }

        /// <summary>
        /// Flatten out a CompositeFilterDescriptor to FilterDescriptor[]
        /// </summary>
        public static IEnumerable<FilterDescriptor> FlattenCompositeFilterDescriptor(CompositeFilterDescriptor root)
        {
            var result = new List<FilterDescriptor>();

            if (root?.Filters != null)
            {
                // current system only support 'and'
                if (root.Logic.ToLower() == "and")
                {
                    foreach (var filterCriteria in root.Filters)
                    {
                        string json = filterCriteria.ToString();

                        if (json.IndexOf("logic") > -1)
                        {
                            var filterDescriptor = json.ToObject<CompositeFilterDescriptor>();
                            // recursive loop
                            result.AddRange(FlattenCompositeFilterDescriptor(filterDescriptor));
                        }
                        else
                        {
                            var filterDescriptor = json.ToObject<FilterDescriptor>();
                            result.Add(filterDescriptor);
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException("Logic not handled");
                }
            }

            return result;
        }
    }
}