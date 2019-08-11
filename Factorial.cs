using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace test
{
    public static class Factorial
    {
        [FunctionName("Factorial")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
			log.LogInformation("name = " + name);

			string number = req.Query["number"];
            log.LogInformation("number = " + number);

            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(number))
            {
                return new BadRequestObjectResult("Please pass a name and an interger number on the query string.");
            }

            int numbertoshow;
            try
            {
                numbertoshow = Int32.Parse(number);
            }
            catch(FormatException e)
            {
                return new BadRequestObjectResult("The number must be a Interger");
            }

            if(numbertoshow < 0 || numbertoshow > 59)
            {
                return new BadRequestObjectResult("The numbwer must be greater than 0 or lower than 59");
            }

            string partialResponse = "Hello " + name + ", the factorial of " + number + " is ";
            string response = partialResponse + calculateFactorial(numbertoshow);
            var finalResponse = new OkObjectResult(response);
            log.LogInformation("finalresponse:" + finalResponse);
            return finalResponse;
        }

        public static string calculateFactorial(int number)
        {
            long result = 1;

            
            for (int i = 1; i <= number; i++)
            {
                result = result * i;
            }

            return result.ToString();
        }

    }
}
