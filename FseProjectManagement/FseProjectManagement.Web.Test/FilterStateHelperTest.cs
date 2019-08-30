using System;
using System.Collections.Generic;
using System.Linq;
using FseProjectManagement.Shared;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using NBench;
using NUnit.Framework;

namespace FseProjectManagement.Web.Controllers
{
    [TestFixture]
    public class FilterStateHelperTest
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void ToOperatorString_Test()
        {
            //arrange
            var containsOperator = FilterCriteriaDescriptor.Contains;
            var lessThanOperator = FilterCriteriaDescriptor.LessThanEqual;
            var greaterThanOperator = FilterCriteriaDescriptor.GreaterThanEqual;
            var eqOperator = FilterCriteriaDescriptor.EqualTo;
            var invalidOperator = FilterCriteriaDescriptor.Undefined;

            //act
            var containsOperatorStr = containsOperator.ToOperatorStr();
            var lessThanOperatorStr = lessThanOperator.ToOperatorStr();
            var greaterThanOperatorStr = greaterThanOperator.ToOperatorStr();
            var eqOperatorStr = eqOperator.ToOperatorStr();


            //assert
            Assert.AreEqual("contains", containsOperatorStr);
            Assert.AreEqual("lte", lessThanOperatorStr);
            Assert.AreEqual("gte", greaterThanOperatorStr);
            Assert.AreEqual("eq", eqOperatorStr);

            Assert.Throws<NotImplementedException>(() => invalidOperator.ToOperatorStr());
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FilterDescriptor_Test()
        {
            //arrange
            var containsOperator = new FilterDescriptor() { Operator = "contains" };
            var lessThanOperator = new FilterDescriptor() { Operator = "lte" };
            var greaterThanOperator = new FilterDescriptor() { Operator = "gte" };
            var eqOperator = new FilterDescriptor() { Operator = "eq" };
            var neqOperator = new FilterDescriptor() { Operator = "neq" };
            var invalidOperator = new FilterDescriptor() { Operator = "test" };
            Func<FilterCriteriaDescriptor> func = () => { return invalidOperator.FilterOperator; };

            //assert
            Assert.AreEqual(FilterCriteriaDescriptor.Contains, containsOperator.FilterOperator);
            Assert.AreEqual(FilterCriteriaDescriptor.LessThanEqual, lessThanOperator.FilterOperator);
            Assert.AreEqual(FilterCriteriaDescriptor.GreaterThanEqual, greaterThanOperator.FilterOperator);
            Assert.AreEqual(FilterCriteriaDescriptor.EqualTo, eqOperator.FilterOperator);
            Assert.AreEqual(FilterCriteriaDescriptor.NotEqualTo, neqOperator.FilterOperator);
            Assert.Throws<NotImplementedException>(() => func());
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SortDescriptor_Test()
        {
            //arrange
            var ascSortDescriptor = new FilterSortDescriptor() { Dir = "asc" };
            var descSortDescriptor = new FilterSortDescriptor() { Dir = "desc" };
            var emptySortDescriptor = new FilterSortDescriptor() { Dir = "" };
            var invalidSortDescriptor = new FilterSortDescriptor() { Dir = "test " };
            Func<SortDirection> func = () => { return invalidSortDescriptor.Direction; }; 

            //assert
            Assert.AreEqual(SortDirection.ASC, ascSortDescriptor.Direction);
            Assert.AreEqual(SortDirection.DSC, descSortDescriptor.Direction); 
            Assert.AreEqual(SortDirection.Undefined, emptySortDescriptor.Direction);
            Assert.Throws<InvalidOperationException>(() => func());
        }


        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Add_Filter_Test()
        {
            //arrange
            var filterState = new FilterConditions();

            //act
            filterState.AddFilter("priority", 1, FilterCriteriaDescriptor.EqualTo);

            //assert
            Assert.NotNull(filterState.Filter.Filters[0]);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void AddFilter_ShouldThrowErrorIfAddingSameFilter()
        {
            //arrange
            var filterState = new FilterConditions();

            //act
            filterState.AddFilter("priority", 1, FilterCriteriaDescriptor.EqualTo);

            //assert
            Assert.NotNull(filterState.Filter.Filters[0]);
            Assert.Throws<InvalidOperationException>(() => filterState.AddFilter("priority", 1, FilterCriteriaDescriptor.EqualTo));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchFilterDescriptors_Test()
        {
            //arrange
            CompositeFilterDescriptor compositeFilter = null;

            //act
            var result = FilterConditionHelper.SearchFilterDescriptors(compositeFilter, "priority");

            //assert
            Assert.NotNull(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FlattenCompositeFilterDescriptor_ShouldThrowError_IfUnsupportedOperatorUsed_Test()
        {
            //arrange
            var compositeFilter = new CompositeFilterDescriptor() { Filters = new List<dynamic>() };

            //act
            compositeFilter.Logic = "OR";

            //assert
            Assert.Throws<NotImplementedException>(() => FilterConditionHelper.FlattenCompositeFilterDescriptor(compositeFilter));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FlattenComposite_FilterDescriptor_Test()
        {
            //arrange
            var compositeFilter = new CompositeFilterDescriptor() { Filters = new List<dynamic>() { new { logic = "and" } } };

            //act
            compositeFilter.Logic = "and";

            //assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => FilterConditionHelper.FlattenCompositeFilterDescriptor(compositeFilter));
        }
    }
}
