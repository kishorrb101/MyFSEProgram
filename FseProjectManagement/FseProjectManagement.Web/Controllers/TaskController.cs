using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Repositories;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;

namespace FseProjectManagement.Web.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : BaseApiController
    {
        private readonly ITaskControllerFacade _taskFacade;
        public TaskController(ITaskControllerFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }

        public TaskController()
        {
            _taskFacade = new TaskControllerFacade(new TaskRepository());
        }

        [Route("query")]
        [HttpPost()]
        [ResponseType(typeof(List<UserModel>))]
        public IHttpActionResult Query([FromBody]FilterConditions filterState)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Query(filterState));
            });
        }

        [Route("getTasks")]
        [ResponseType(typeof(List<TaskModel>))]
        [HttpGet]
        // GET: api/task/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }
        [Route("{id}")]
        [ResponseType(typeof(TaskModel))]
        [HttpGet]
        // GET: api/task/5
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(TaskModel))]
        [HttpPost]
        // POST: api/task/update
        public IHttpActionResult Update(TaskModel task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/task/delete/5
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Delete(id));
            });
        }

        [Route("updateTaskState")]
        [ResponseType(typeof(bool))]
        [HttpPost()]
        // POST: api/task/complete
        public IHttpActionResult UpdateTaskState([FromBody] TaskModel task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.UpdateTaskState(task.Id, task.StatusId));
            });
        }
    }
}