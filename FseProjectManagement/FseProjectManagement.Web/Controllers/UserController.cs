using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using FseProjectManagement.DataAccessLayer.Repositories;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Controller;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserControllerFacade _userFacade;
        public UserController(IUserControllerFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public UserController()
        {
            _userFacade = new UserControllerFacade(new UserDetailsRepository());
        }

        [Route("query")]
        [HttpPost()]
        [ResponseType(typeof(List<UserModel>))]
        public IHttpActionResult QueryUser([FromBody]FilterConditions filterState)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Query(filterState));
            });
        }

        [Route("getUsers")]
        [ResponseType(typeof(List<UserModel>))]
        [HttpGet]
        // GET: api/users/getUsers
        public IHttpActionResult GetUsersDetails()
        {
            return Try(() =>
            {
                return Ok(_userFacade.GetAll());
            });
        }


        [Route("{id}")]
        [ResponseType(typeof(UserModel))]
        [HttpGet]
        // GET: api/user/5
        public IHttpActionResult GetUserDetails(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(UserModel))]
        [HttpPost]
        // POST: api/user/update
        public IHttpActionResult UpdateUser(UserModel user)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Update(user));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/user/5
        public IHttpActionResult DeleteUser(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Delete(id));
            });
        }
    }
}
