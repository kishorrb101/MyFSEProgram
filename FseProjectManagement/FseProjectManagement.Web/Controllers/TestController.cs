using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FseProjectManagement.Web.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route("getStatus")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetStatus()
        {
            try
            {
                // prepare message text
                var msg = new StringBuilder();
                msg.Append("Web API is up and running");

                // instantiate an anonymous object
                var model = new
                {
                    msg = msg.ToString()
                };

                // serialize to JSON and return client
                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        [Route("get")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
