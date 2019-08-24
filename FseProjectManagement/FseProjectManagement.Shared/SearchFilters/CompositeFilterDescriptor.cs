using System.Collections.Generic;

namespace FseProjectManagement.Shared.SearchFilters.FilterCriteria
{
    public class CompositeFilterDescriptor
    {
        /// <summary>
        /// The logical operation to use when the `filter.filters` option is set.
        ///
        /// The supported values are:
        /// * "and"
        /// * "or"
        /// </summary>
        public string Logic { get; set; } //: 'or' | 'and';

        /// <summary>
        /// The nested filter expression -- either
        /// [`IFilterDescriptor`] or [`ICompositeFilterDescriptor`]
        /// Supports the same options as `filter`. You can nest filters indefinitely.
        /// </summary>
        public List<dynamic> Filters { get; set; }
    }
}