using Microsoft.AspNetCore.Mvc;
using System;

namespace Max.SkeletonProject.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : Controller
    { 
        [HttpGet]
        [Route("")]
        public Core.Entities.Event Get()
        {
            return "This is an example string generated on the server, fetched using VueResource";
        }
    }
}
