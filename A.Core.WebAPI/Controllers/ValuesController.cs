using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using A.Core.Extensions;

namespace A.Core.WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            var obj = new
            {
                DT = DateTime.Today, DT2 = DateTime.Now, DT3 = new DateTime(DateTime.Now.Ticks, DateTimeKind.Utc)
                , DT4 = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified)
            };

            var s = obj.ToKeyValue();

            var c1 = DateTime.Now.ToString("u");
            var c3 = DateTime.Now.ToUniversalTime().ToString("u");
            var c2 = DateTime.Today.ToString("o");
            var c4 = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified).ToString("o");

            var d1 = DateTime.Parse(c1);
            var d2 = DateTime.Parse(c2);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
