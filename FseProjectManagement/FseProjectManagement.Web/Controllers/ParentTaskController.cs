using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Repositories;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Controllers
{
    [RoutePrefix("api/parentTask")]
    public class ParentTaskController : BaseApiController
    {
        private readonly IParentTaskControllerFacade _parentTaskFacade;
        public ParentTaskController(IParentTaskControllerFacade taskFacade)
        {
            _parentTaskFacade = taskFacade;
        }
        public ParentTaskController()
        {
            _parentTaskFacade = new ParentTaskControllerFacade(new ParentTaskRepository());
        }


        [Route("getTasks")]
        [ResponseType(typeof(List<ParentTaskModel>))]
        [HttpGet]
        // GET: api/parentTask/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_parentTaskFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentTaskModel))]
        [HttpGet]
        // GET: api/parentTask/5
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_parentTaskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ParentTaskModel))]
        [HttpPost]
        // POST: api/parentTask/update
        public IHttpActionResult Update(ParentTaskModel task)
        {
            return Try(() =>
            {
                return Ok(_parentTaskFacade.Update(task));
            });
        }
    }
}