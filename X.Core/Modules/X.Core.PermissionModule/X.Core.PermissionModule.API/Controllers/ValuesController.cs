using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.PermissionModule.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace X.Core.PermissionModule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ValuesController : ControllerBase
    {
        protected X.Core.Services.Core.StateMachine.StateMachine<XCoreHelloStateMachineEnum, XCoreHelloStateMachineTriggerEnum> _machine
        {
            get;
            set;
        }

        public ValuesController(
            X.Core.Services.Core.StateMachine.StateMachine<XCoreHelloStateMachineEnum, XCoreHelloStateMachineTriggerEnum
            > machine)
        {
            _machine = machine;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //throw new Exception("AMEL");
            return new string[] { "value1", "value2" };
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
