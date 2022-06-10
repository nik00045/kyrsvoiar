using kyrsvoiar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kyrsar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class testController : ControllerBase
    {
       

        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            using (var contex = new mobilearContext()) {
                return contex.Owner.ToList();


            }
        }
    }
}
