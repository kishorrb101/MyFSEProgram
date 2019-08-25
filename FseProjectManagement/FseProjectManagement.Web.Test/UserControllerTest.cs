using AutoMapper;
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Mapper;
using FseProjectManagement.Web.Extensions.Models;
using Moq;
using NBench;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;


namespace FseProjectManagement.Web.Test
{
    [TestFixture]
    public class UserControllerTest
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
        public void GetUsersDetails_ShouldReturn_AllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.GetAll()).Returns(testUsers);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.GetUsersDetails() as OkNegotiatedContentResult<List<UserModel>>;

            //assert
            Assert.AreEqual(testUsers.Count(), result.Content.Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryUsersDetails_ShouldReturn_AllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var queryResult = new FilterReturnResult<UserDetails>() { Data = testUsers, Total = testUsers.Count() };
            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Query(It.IsAny<FilterConditions>())).Returns(queryResult);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userControler = new UserController(userFacade);
            var filterState = new FilterConditions();

            //act : no filters
            var x = userControler.QueryUser(filterState);
            var result = x as OkNegotiatedContentResult<FilterReturnResult<UserModel>>;

            //assert
            Assert.AreEqual(testUsers.Count(), result.Content.Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldReturn_CorrectUser()
        {
            //arrange
            var userIdToBeQueried = 1;
            var testUsers = GetTestUsers();

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userIdToBeQueried)).Returns(testUsers.First(u => u.Id == userIdToBeQueried));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);
            var expectetUser = testUsers.First(u => u.Id == userIdToBeQueried);

            //act
            var result = userController.GetUserDetails(userIdToBeQueried) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.AreEqual(expectetUser.FirstName, result.Content.FirstName);
            Assert.AreEqual(expectetUser.LastName, result.Content.LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldNotReturn_User()
        {
            //arrange
            var userIdToBeQueried = 1000;

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userIdToBeQueried));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.GetUserDetails(userIdToBeQueried) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldNotReturn_UserDB()
        {
            //arrange
            var userIdToBeQueried = -1;
            var userController = new UserController();

            //act
            var result = userController.GetUserDetails(userIdToBeQueried) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAdd_NewUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var newUserDto = new UserModel()
            {
                FirstName = "FirstName_Mocked",
                LastName = "LastName_Mocked",
                EmployeeId = "Mocker_Employee"
            };
            var newUser = Mapper.Map<UserDetails>(newUserDto);

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Add(newUser)).Returns(newUser);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.UpdateUser(newUserDto) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.AreEqual(newUserDto.FirstName, result.Content.FirstName);
            Assert.AreEqual(newUserDto.LastName, result.Content.LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldNotUpdate_If_FirstNameAndLastName_NotProvided()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userModelToBeUpdated = new UserModel()
            {
                Id = 5,
                EmployeeId = "Mocker_Employee"
            };

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userModelToBeUpdated.Id));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.UpdateUser(userModelToBeUpdated) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.IsNull(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
              TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdateTo_CorrectUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userModelToBeUpdated = new UserModel()
            {
                Id = 4,
                FirstName = "First Name Mocked",
                LastName = "Last Name_Mocked",
                EmployeeId = "Employee Mocker"
            };

            var oldUser = testUsers.First(u => u.Id == userModelToBeUpdated.Id);

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userModelToBeUpdated.Id)).Returns(oldUser);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.UpdateUser(userModelToBeUpdated) as OkNegotiatedContentResult<UserModel>;

            //assert
            Assert.AreEqual(userModelToBeUpdated.FirstName, result.Content.FirstName);
            Assert.AreEqual(userModelToBeUpdated.LastName, result.Content.LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
           TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryUser_ShouldNotReturn_AnyProject()
        {
            //arrange
            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Query(It.IsAny<FilterConditions>()));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act : no filters
            var result = userController.QueryUser(null) as OkNegotiatedContentResult<FilterReturnResult<ProjectDetails>>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
           TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDelete_CorrectUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userIdToBDeleted = 4;

            var user = testUsers.First(u => u.Id == userIdToBDeleted);

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userIdToBDeleted)).Returns(user);
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Remove(user));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.DeleteUser(userIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDelete_WhenUserNotFound()
        {
            //arrange
            var userToBeDeleted = 4;

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted));

            var userFacade = new UserControllerFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.DeleteUser(userToBeDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDelete_WhenUserHasProjects()
        {
            //arrange
            var userToBeDeleted = new UserDetails() { Id = 4, Projects = new List<ProjectDetails>() { new ProjectDetails() { Id = 1 } } };

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted.Id)).Returns(userToBeDeleted);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var projectController = new UserController(userFacade);

            //act
            var result = projectController.DeleteUser(userToBeDeleted.Id) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDelete_WhenUserHasTasks()
        {
            //arrange
            var userToBeDeleted = new UserDetails() { Id = 4, Tasks = new List<TaskDetails>() { new TaskDetails() { Id = 1 } } };

            var mockUserRepository = new Mock<IUserDetailsRepository>().Object;
            Mock.Get<IUserDetailsRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted.Id)).Returns(userToBeDeleted);

            var userFacade = new UserControllerFacade(mockUserRepository);
            var projectController = new UserController(userFacade);

            //act
            var result = projectController.DeleteUser(userToBeDeleted.Id) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        private IQueryable<UserDetails> GetTestUsers()
        {
            var testUsers = new List<UserDetails>
            {
            new UserDetails{Id = 1, FirstName="First Name 1",LastName="Last Name 1", EmployeeId = "1" },
            new UserDetails{Id = 2, FirstName="First Name 2",LastName="Last Name 2", EmployeeId = "2"},
            new UserDetails{Id = 3, FirstName="First Name 3",LastName="Last Name 3" ,EmployeeId = "3"},
            new UserDetails{Id = 4, FirstName="First Name 4",LastName="Last Name 4" ,EmployeeId = "4"},
            new UserDetails{Id = 5, FirstName="First Name 5",LastName="Last Name 5" ,EmployeeId = "5"},
            new UserDetails{Id = 6, FirstName="First Name 6",LastName="Last Name 6" ,EmployeeId = "6"},
            };

            return testUsers.AsQueryable();
        }
    }
}