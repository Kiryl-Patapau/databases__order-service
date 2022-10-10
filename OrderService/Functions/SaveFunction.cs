using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderService.Models;

namespace OrderService.Functions;

public static class SaveFunction
{
    [FunctionName("SaveFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "save")] HttpRequest request,
        [CosmosDB(
            databaseName: "Catalog", 
            collectionName: "Orders", 
            ConnectionStringSetting = "OrderServiceDatabase")] IAsyncCollector<Order> orders,
        ILogger logger)
    {
        try
        {
            using var bodyReader = new StreamReader(request.Body);
            var body = await bodyReader.ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(body);
            if (order is null)
            {
                logger.LogWarning("Null or empty request body is received by {function}.", nameof(SaveFunction));
                return new BadRequestResult();
            }

            await orders.AddAsync(order);
        }
        catch (JsonException exception)
        {
            logger.LogWarning(exception, "Invalid request body is received by {function}.", nameof(SaveFunction));
            return new BadRequestResult();
        }

        return new OkResult();
    }
}
