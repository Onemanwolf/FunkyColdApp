using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Funk
{
    public static class HttpTriggerCSharpFunk
    {
        [FunctionName("HttpTriggerCSharpFunk")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

           
            string Id = req.Query["Id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            dynamic person = JsonConvert.DeserializeObject<Person>(requestBody);
            
            Id = Id ?? data?.Id;
           if(Id != null){
               
               person =  GetPerson(Id); } 

            return person != null
            
                ? (ActionResult)new OkObjectResult($"Hello, {person.Name} {person.Id}")
                    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        
        
        }

        private  static  Person GetPerson(string id)
        {
             var peps =  new Person() {Name="Billy", Id="1"};
           if(id == peps.Id)
             return peps;
             else{ return null;}
            
               
        }
    }
}
        
    
