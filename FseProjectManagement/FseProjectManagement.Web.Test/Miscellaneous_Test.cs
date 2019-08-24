using NBench;
using NUnit.Framework;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp;

namespace FseProjectManagement.Web.Test
{
    [TestFixture]
    public class Miscellaneous_Test
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Test_JsonObject()
        {
            //Arrange
            var dto = new ProjectDto()
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
        public void Test_StringToJson()
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
        public void Test_InvalidDateFromDate()
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
        public void UserDto_DisplayName()
        {
            //Arrange
            var userDto = new UserDto() {FirstName = "FirstName", LastName = "LastName" };

            //Assert
            Assert.AreEqual("FirstName LastName", userDto.DisplayName);
        }
    }
}
