using System;
using FseProjectManagement.Shared.Helper;
using FseProjectManagement.Web.Extensions.Models;
using NBench;
using NUnit.Framework;

namespace FseProjectManagement.Web.Test
{
    [TestFixture]
    public class MiscellaneousTest
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void JsonObject_Test()
        {
            //Arrange
            var dto = new ProjectModel()
            {
                Name = "Test"
            };

            //Act
            var json = dto.ToJson();

            //Assert
            Assert.IsNotNull(json);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void StringToJson_Test()
        {
            //Arrange
            var str = @"{ 'id': 0, 'Name': 'Test'}";


            //Act
            var json = str.ToJson();

            //Assert
            Assert.IsNotNull(json);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Test_InvalidDateFromString()
        {
            //Arrange
            var invaliDate = string.Empty;

            //Act
            var date = invaliDate.YYYYMMDDToDate();

            //Assert
            Assert.IsNull(date);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void InvalidDateFromDate_Test()
        {
            //Arrange
            DateTime? invaliDate = null;

            //Act
            var date = invaliDate.DateToYYYYMMDD();

            //Assert
            Assert.IsEmpty(date);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UserDisplayName_Test()
        {
            //Arrange
            var userDto = new UserModel() {FirstName = "FirstName", LastName = "LastName" };

            //Assert
            Assert.AreEqual("FirstName LastName", userDto.DisplayName);
        }
    }
}
