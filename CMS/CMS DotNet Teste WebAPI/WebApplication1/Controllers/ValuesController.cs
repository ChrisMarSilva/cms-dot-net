using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {

        //Get = Select
        //Post = Insert
        //Put = Update
        //Delete = Delete

        // GET api/values
        public IEnumerable<string> Get() //public string Get()
        {
            return new string[] { "value1", "value2" };
            //var lista = new List<string>();
            //lista.Add("value1");
            //lista.Add("value2");
            //return lista; // new string[] { "value1", "value2" };
            //return JsonConvert.SerializeObject(lista);
        }

        // GET api/values/5
        [CacheOutput(ServerTimeSpan = 120)] //120 segundos
        //[ResponseCache(Duration = 3600)]
        // [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        // [ResponseCache(CacheProfileName = "Default30")]
        public string Get(int id)
        {
            //return "value";
            return DateTime.Now.ToString();
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
