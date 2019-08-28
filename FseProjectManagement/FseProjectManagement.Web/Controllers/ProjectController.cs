
using DataAccess.Repositories;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Controllers;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FseProjectManagement.Web.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : BaseApiController
    {
        private readonly IProjectControllerFacade _projectFacade;
        public ProjectController(IProjectControllerFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }
        public ProjectController()
        {
            _projectFacade = new ProjectControllerFacade(new ProjectDetailsRepository());
        }

        [Route("query")]
        [HttpPost()]
        [ResponseType(typeof(List<ProjectModel>))]
        public IHttpActionResult Query([FromBody]FilterConditions filterState)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Query(filterState));
            });
        }

        [Route("getProjects")]
        [ResponseType(typeof(List<ProjectModel>))]
        [HttpGet]
        // GET: api/project/getProjects
        public IHttpActionResult GetProjects()
        {
            return Try(() =>
            {
                return Ok(_projectFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ProjectModel))]
        [HttpGet]
        // GET: api/project/5
        public IHttpActionResult GetProject(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Get(id));
            });
        }

        [Route("updateProjectState")]
        [ResponseType(typeof(bool))]
        [HttpPost()]
        // POST: api/project/suspend/?id=10
        public IHttpActionResult UpdateProjectState(ProjectModel project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.UpdateProjectState(project.Id, project.IsSuspended));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ProjectModel))]
        [HttpPost]
        // POST: api/project/update
        public IHttpActionResult Update(ProjectModel project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Update(project));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/project/5
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Delete(id));
            });
        }
    }
}
