using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Startup.Abstract;

namespace DependencyInjection
{
    public static class DIExampleFunction
    {
        [FunctionName("DISample")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, IWelcomeService welcomeService)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return welcomeService != null
                ? (ActionResult)new OkObjectResult($"Hello, {welcomeService.WelcomeMessage()}")
                : new BadRequestObjectResult("WelcomeService not resolved");
        }
    }
}
