using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Model;
using Dapper.Service;
using DapperFrame.Entity;
using DapperFrame.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DapperWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Default")]
    public class DefaultController : Controller
    {
        private readonly Sys_DapperTestService service;
        public DefaultController()
        {
            service = new Sys_DapperTestService();
        }

        [HttpGet]
        public JsonResult Get()
        {

            var person = JsonConfigurationHelper.GetAppSettings<List<TableConfiguration>>("TableConfigCollection");
            return Json(person);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = service.GetDapperList();
            return Json(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}