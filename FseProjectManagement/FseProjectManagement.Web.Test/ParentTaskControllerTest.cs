using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using AutoMapper;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Mapper;
using FseProjectManagement.Web.Extensions.Models;
using Moq;
using NBench;
using NUnit.Framework;

namespace FseProjectManagement.Web.Controllers
{
    [TestFixture]
    public class ParentTaskControllerTest
    {
        [SetUp]
        [PerfSetup]
        public void InitializeOneTimeData()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Initialize();
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTasks_ShouldReturn_AllParenTasks_Test()
        {
            //arrange
            var testTasks = GetTestTasksDetails();
            var mockTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);
            
            var taskFacade = new ParentTaskControllerFacade(mockTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.GetTasks() as OkNegotiatedContentResult<List<ParentTaskModel>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void  GetparentTask_ShouldReturn_CorrectparentTask_Test()
        {
            //arrange
            var taskIdToBeQueried = 1;
            var testTasks = GetTestTasksDetails();

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried)).Returns(testTasks.First(u=>u.Id == taskIdToBeQueried));

            var taskFacade = new ParentTaskControllerFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);
            var expectetparentTask = testTasks.First(u => u.Id == taskIdToBeQueried);

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskModel>;

            //assert
            Assert.AreEqual(expectetparentTask.Name, result.Content.Name);
        }


        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
              TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetparentTask_ShouldNot_ReturnTask_Test()
        {
            //arrange
            var taskIdToBeQueried = 1000;

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried));

            var taskFacade = new ParentTaskControllerFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskModel>;

            //assert
            Assert.AreEqual(result, null);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetparentTask_ShouldNot_ReturnTask_DB_Test()
        {
            //arrange
            var taskIdToBeQueried = -1; 
            var taskController = new ParentTaskController();

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskModel>;

            //assert
            Assert.AreEqual(result, null);
        }


        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAdd_NewparentTask_Test()
        {
            //arrange
            var testTasks = GetTestTasksDetails();
            var newTaskDto = new ParentTaskModel() {
                Name = "Name_Mocked",
            };
            var newparentTask = Mapper.Map<ParentTaskDetails>(newTaskDto);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Add(newparentTask)).Returns(newparentTask);

            var taskFacade = new ParentTaskControllerFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(newTaskDto) as OkNegotiatedContentResult<ParentTaskModel>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name); 
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdate_CorrectParentTask_Test()
        {
            //arrange
            var testTasks = GetTestTasksDetails();
            var parentTaskDtoToBeUpdated = new ParentTaskModel()
            {
                Id = 2,
                Name = "Name_updated"
            };

            var oldTask = testTasks.First(u => u.Id == parentTaskDtoToBeUpdated.Id);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(parentTaskDtoToBeUpdated.Id)).Returns(oldTask);

            var taskFacade = new ParentTaskControllerFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(parentTaskDtoToBeUpdated) as OkNegotiatedContentResult<ParentTaskModel>;

            //assert
            Assert.AreEqual(parentTaskDtoToBeUpdated.Name, result.Content.Name); 
        }


        private IQueryable<ParentTaskDetails> GetTestTasksDetails()
        {
            var parentTask = new List<ParentTaskDetails>
            {
            new ParentTaskDetails { Id =1, Name = "ParentTask_1"},
            new ParentTaskDetails { Id =2, Name = "parentTask_2" },
            };
            return parentTask.AsQueryable();
        }
    }
}