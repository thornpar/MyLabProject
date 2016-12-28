using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyLabProject.Controllers
{
    //[RoutePrefix("special")]
    public class TestController : ApiController
    {
        [HttpGet]
        [ActionName("test")]
        //[Route("test")]
        public IHttpActionResult Test()
        {
            return Ok("hello world");
        }
        [HttpGet]
        [ActionName("some")]
        //[Route("some")]
        public IHttpActionResult somethingelse()
        {
            return Ok("Something else");
        }

    }
}
