using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace CoursesAPI.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values
        [Authorize(Roles = "student")]
		public IEnumerable<string> Get()
		{
            System.Security.Claims.ClaimsPrincipal userClaims = new ClaimsPrincipal(User.Identity);

            if (userClaims.IsInRole("student"))
            {

            }

            var principal = User as ClaimsPrincipal;
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
