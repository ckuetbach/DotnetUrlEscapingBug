using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EscapeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get0(string id)
        {
            var du = Request.GetDisplayUrl();
            var ab = Request.GetEncodedPathAndQuery();
            return id;
        }

        // GET api/values/5
        [HttpGet("({id})")]
        public ActionResult<string> Get1(string id)
        {
            var du = Request.GetDisplayUrl();
            var ab = Request.GetEncodedPathAndQuery();
            return id;
        }

    }
}