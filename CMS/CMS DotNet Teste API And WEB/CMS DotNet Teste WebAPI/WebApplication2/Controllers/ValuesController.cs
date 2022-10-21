using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {

        //Get = Select
        //Post = Insert
        //Put = Update
        //Delete = Delete

        // GET api/values
        public IEnumerable<string> Get() //public string Get() //
        {
            return new string[] { "value1", "value2" };
           // var lista = new List<string>();
            //lista.Add("value1");
            //lista.Add("value2");
            //return lista; // new string[] { "value1", "value2" };
            //return JsonConvert.SerializeObject(lista);
        }
   
    }
}
