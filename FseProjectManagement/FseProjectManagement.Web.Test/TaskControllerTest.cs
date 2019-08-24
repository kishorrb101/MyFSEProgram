using AutoMapper;
using DataAccess.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Moq;
using NBench;
using NUnit.Framework;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Mapper;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Test
{
    [TestFixture]
    public class TaskControllerTest
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
        public void GetTasks_ShouldReturn_AllTasks_Test()
        {
            //arrange
            var testTasks = GetTestTaskDetails();
            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);
            
            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.GetTasks() as OkNegotiatedContentResult<List<TaskModel>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        [Test]
        public void QueryTasks_ShouldReturn_AllProjects_Test()
        {
            //arrange
            var testProjects = GetTestTaskDetails();
            var queryResult = new FilterReturnResult<TaskDetails>() { Data = testProjects, Total = testProjects.Count() };
            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Query(It.IsAny<FilterConditions>())).Returns(queryResult);

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);
            var filterState = new FilterConditions();

            //act : no filters
            var x = taskController.Query(filterState);
            var result = x as OkNegotiatedContentResult<FilterReturnResult<TaskModel>>;

            //assert
            Assert.AreEqual(testProjects.Count(), result.Content.Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryTasks_ShouldNotReturn_AnyTask_Test()
        {
            //arrange
            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Query(It.IsAny<FilterConditions>()));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);
            var filterState = new FilterConditions();

            //act : no filters
            var x = taskController.Query(filterState);
            var result = x as OkNegotiatedContentResult<FilterReturnResult<TaskModel>>;

            //assert
            Assert.IsNull(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void  GetTask_ShouldReturn_CorrectTask_Test()
        {
            //arrange
            var TaskIdToBeQueried = 1;
            var testTasks = GetTestTaskDetails();

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried)).Returns(testTasks.First(u=>u.TaskId == TaskIdToBeQueried));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);
            var expectetTask = testTasks.First(u => u.TaskId == TaskIdToBeQueried);

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskModel>;

            //assert
            Assert.AreEqual(expectetTask.Name, result.Content.Name);
            Assert.AreEqual(expectetTask.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTask_ShouldNotReturnTask_Test()
        {
            //arrange
            var TaskIdToBeQueried = 1000; 

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
             var taskController = new TaskController(taskControllerFacade); 

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskModel>;

            //assert
            Assert.AreEqual(null, result); 
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTask_ShouldNotReturnTask_FROMDB_Test()
        {
            //arrange
            var TaskIdToBeQueried = -1;
            var taskController = new TaskController();

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskModel>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAddNewTask_Test()
        {
            //arrange
            var testTasks = GetTestTaskDetails();
            var newTaskDto = new TaskModel() {
                TaskId = 1,
                Name = "Task_5",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };
            var newTask = Mapper.Map<TaskDetails>(newTaskDto);

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Add(newTask)).Returns(newTask);

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.Update(newTaskDto) as OkNegotiatedContentResult<TaskModel>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name);
            Assert.AreEqual(newTaskDto.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdateCorrectTask_Test()
        {
            //arrange
            var testTasks = GetTestTaskDetails();
            var TaskDtoToBeUpdated = new TaskModel()
            {
                TaskId = 4,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };

            var oldTask = testTasks.First(u => u.TaskId == TaskDtoToBeUpdated.TaskId);

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.TaskId)).Returns(oldTask);

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.Update(TaskDtoToBeUpdated) as OkNegotiatedContentResult<TaskModel>;

            //assert
            Assert.AreEqual(TaskDtoToBeUpdated.Name, result.Content.Name);
            Assert.AreEqual(TaskDtoToBeUpdated.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDeleteCorrectTask_Test()
        {
            //arrange
            var testTasks = GetTestTaskDetails();
            var TaskIdToBDeleted = 4;

            var task = testTasks.First(u => u.TaskId == TaskIdToBDeleted);

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted)).Returns(task);
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Remove(task));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.Delete(TaskIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content); 
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenNoTaskFound_Test()
        {
            //arrange
            var TaskIdToBDeleted = 4;

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.Delete(TaskIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.IsNull(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_WhenTaskDoesNotExist_Test()
        {
            //arrange  
            var TaskDtoToBeUpdated = new TaskModel()
            {
                TaskId = -1,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.TaskId));

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.UpdateTaskState(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_WhenTaskAlreadyUpToDate_Test()
        {
            //arrange  
            var TaskDtoToBeUpdated = new TaskModel()
            {
                TaskId = 1,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1,
                StatusId = 2
            };

            var testTasks = GetTestTaskDetails();
            var oldTask = testTasks.First(u => u.TaskId == TaskDtoToBeUpdated.TaskId);

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.TaskId)).Returns(oldTask);

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.UpdateTaskState(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_ShouldUpdateCorrectTask_Test()
        {
            //arrange  
            var TaskDtoToBeUpdated = new TaskModel()
            {
                TaskId = 4,
                Name = "Task_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1,
                StatusId = 2
            };

            var testTasks = GetTestTaskDetails();
            var oldTask = testTasks.First(u => u.TaskId == TaskDtoToBeUpdated.TaskId);

            var mockTaskRepository = new Mock<ITaskDetailsRepository>().Object;
            Mock.Get<ITaskDetailsRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.TaskId)).Returns(oldTask);

            var taskControllerFacade = new TaskControllerFacade(mockTaskRepository);
            var taskController = new TaskController(taskControllerFacade);

            //act
            var result = taskController.UpdateTaskState(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content);
        }

        #region "private helper methods"
        private IQueryable<TaskDetails> GetTestTaskDetails()
        {
            var testTasks = new List<TaskDetails>
            {
            new TaskDetails { TaskId = 1, Name = "Task 1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1, StatusId = 2},
            new TaskDetails { TaskId = 2, Name = "Task 2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1, StatusId = 2},
            new TaskDetails { TaskId = 2, Name = "Task 3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1, StatusId = 2 },
            new TaskDetails { TaskId = 4, Name = "Task 4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1, StatusId = 2 },
            new TaskDetails { TaskId = 2, Name = "Task 5",  StartDate = DateTime.Parse("2019-05-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5 , OwnerId = 5, ProjectId = 5, ParentTaskId = 1, StatusId = 2 },
            new TaskDetails { TaskId = 4, Name = "Task 6",  StartDate = DateTime.Parse("2019-06-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 6 , OwnerId = 6, ProjectId = 6, ParentTaskId = 1, StatusId = 2 },
            };

            return testTasks.AsQueryable();
        }
        #endregion "private helper methods"
    }
}