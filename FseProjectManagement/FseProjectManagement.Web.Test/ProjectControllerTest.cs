using Moq;
using NBench;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.Results;
using AutoMapper;
using FseProjectManagement.Web.Extensions.Mapper;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Shared.Helper;

namespace FseProjectManagement.Web.Test
{
    [TestFixture]
    public class ProjectControllerTest
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
        public void GetProjects_ShouldReturn_AllProjects_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.GetAll()).Returns(testProjects);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.GetProjects() as OkNegotiatedContentResult<List<ProjectModel>>;

            //assert
            Assert.AreEqual(testProjects.Count(), result.Content.Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryProjects_ShouldReturn_AllProjects_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var queryResult = new FilterReturnResult<ProjectDetails>() { Data = testProjects, Total = testProjects.Count() };
            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Query(It.IsAny<FilterConditions>())).Returns(queryResult);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);
            var filterState = new FilterConditions();

            //act : no filters
            var x = projectController.Query(filterState);
            var result = x as OkNegotiatedContentResult<FilterReturnResult<ProjectModel>>;

            //assert
            Assert.AreEqual(testProjects.Count(), result.Content.Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryProjects_ShouldNotReturn_AnyProject_Test()
        {
            //arrange
            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Query(It.IsAny<FilterConditions>()));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act : no filters
            var result = projectController.Query(null) as OkNegotiatedContentResult<FilterReturnResult<ProjectModel>>;

            //assert
            Assert.Null(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryProjects_ShouldReturnTaskWithPriorityGreater_ThanAndEqualToZero_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails().Where(p=>p.Priority>=0);
            var queryResult = new FilterReturnResult<ProjectDetails>() { Data = testProjects, Total = testProjects.Count() };
            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Query(It.IsAny<FilterConditions>())).Returns(queryResult);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);
            var thisAssembly = Assembly.GetExecutingAssembly();
            var jsonFilePath = Path.Combine(Directory.GetParent(thisAssembly.Location).FullName, @"TestData\FilerStat.Json");
            var fileStatString = File.ReadAllText(jsonFilePath);
            var filterState = fileStatString.ToObject<FilterConditions>();

            var result = projectController.Query(filterState) as OkNegotiatedContentResult<FilterReturnResult<ProjectModel>>;

            //assert
            Assert.True(result.Content.Data.All(t => t.Priority >= 0));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetProject_ShouldReturn_CorrectProject_Test()
        {
            //arrange
            var ProjectIdToBeQueried = 1;
            var testProjects = GetTestProjectsDetails();

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBeQueried)).Returns(testProjects.First(u => u.Id == ProjectIdToBeQueried));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);
            var expectetProject = testProjects.First(u => u.Id == ProjectIdToBeQueried);

            //act
            var result = projectController.GetProject(ProjectIdToBeQueried) as OkNegotiatedContentResult<ProjectModel>;

            //assert
            Assert.AreEqual(expectetProject.Name, result.Content.Name);
            Assert.AreEqual(expectetProject.Priority, result.Content.Priority);
            Assert.AreEqual(expectetProject.ManagerId, result.Content.ManagerId);
        }


        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetProject_ShouldNotReturn_Project_Test()
        { //arrange
            var ProjectIdToBeQueried = 1000;

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBeQueried));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.GetProject(ProjectIdToBeQueried) as OkNegotiatedContentResult<ProjectModel>;

            //assert
            Assert.AreEqual(null, null);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetProject_ShouldNotReturn_Project_DB_Test()
        { //arrange
            var ProjectIdToBeQueried = -1;
            var projectController = new ProjectController();

            //act
            var result = projectController.GetProject(ProjectIdToBeQueried) as OkNegotiatedContentResult<ProjectModel>;

            //assert
            Assert.AreEqual(null, null);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAdd_NewProject_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var newProjectDto = new ProjectModel()
            {
                Id = 5,
                Name = "Project_5",
                StartDate = "20190101",
                EndDate = "20200901",
                Priority = 2,
                ManagerId = 2
            };
            var newProject = Mapper.Map<ProjectDetails>(newProjectDto);

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Add(newProject)).Returns(newProject);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.Update(newProjectDto) as OkNegotiatedContentResult<ProjectModel>;

            //assert
            Assert.AreEqual(newProjectDto.Name, result.Content.Name);
            Assert.AreEqual(newProjectDto.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdate_CorrectProject_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var projectDtoToBeUpdated = new ProjectModel()
            {
                Id = 4,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.Update(projectDtoToBeUpdated) as OkNegotiatedContentResult<ProjectModel>;

            //assert
            Assert.AreEqual(projectDtoToBeUpdated.Name, result.Content.Name);
            Assert.AreEqual(projectDtoToBeUpdated.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDelete_CorrectProject_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var ProjectIdToBDeleted = 4;

            var Project = testProjects.First(u => u.Id == ProjectIdToBDeleted);

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBDeleted)).Returns(Project);
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Remove(Project));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.Delete(ProjectIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDelete_WhenProjectNotFound_Test()
        {
            //arrange
            var ProjectIdToBDeleted = 4;
            
            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBDeleted));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.Delete(ProjectIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDelete_WhenProjectHasTasks_Test()
        {
            //arrange
            var projectToBeDeleted = new ProjectDetails() { Id = 4, Tasks = new List<TaskDetails>() { new TaskDetails() { Id = 1 } } };

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(projectToBeDeleted.Id)).Returns(projectToBeDeleted);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.Delete(projectToBeDeleted.Id) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_WhenProject_DoesNotExists_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var projectDtoToBeUpdated = new ProjectModel()
            {
                Id = -1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                ManagerId = 2
            };

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id));

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.UpdateProjectState(projectDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_WhenProjectAlready_Updated_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var projectDtoToBeUpdated = new ProjectModel()
            {
                Id = 1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                IsSuspended = false,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.UpdateProjectState(projectDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_ShouldUpdate_CorrectProject_Test()
        {
            //arrange
            var testProjects = GetTestProjectsDetails();
            var projectDtoToBeUpdated = new ProjectModel()
            {
                Id = 1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                IsSuspended = true,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectDetailsRepository>().Object;
            Mock.Get<IProjectDetailsRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var projectFacade = new ProjectControllerFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = projectController.UpdateProjectState(projectDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            //assert
            Assert.True(result.Content);
        }

        private IQueryable<ProjectDetails> GetTestProjectsDetails()
        {
            var testProjects = new List<ProjectDetails>
            {
            new ProjectDetails { Id =1, Name = "Project_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1, IsSuspended = false},
            new ProjectDetails { Id = 2, Name = "Project_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1, IsSuspended = false },
            new ProjectDetails { Id = 3, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 10, ManagerId = 1, IsSuspended = false },
            new ProjectDetails { Id =4, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 15, ManagerId = 1 , IsSuspended = false},
            new ProjectDetails { Id = 5, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 10, ManagerId = 1, IsSuspended = false },
            new ProjectDetails { Id =6, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 15, ManagerId = 1 , IsSuspended = false},
            };

            return testProjects.AsQueryable();
        }
    }
}