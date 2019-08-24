using System.Collections.Generic;

namespace FseProjectManagement.Shared.SearchFilters.FilterCriteria
{
    public class FilterReturnResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int? Total { get; set; }

        public object Auxiliary { get; set; }
    }
}