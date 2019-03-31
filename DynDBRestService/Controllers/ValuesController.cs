using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DynDBRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static IAmazonDynamoDB DynamoDBClient { get; set; }

        public ValuesController(IAmazonDynamoDB dynamoDBClient)
        {
            DynamoDBClient = dynamoDBClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await GetDataFromDb();
        }

        private static async Task<string> GetDataFromDb()
        {
            var table = Table.LoadTable(DynamoDBClient, "policy");
            var item = await table.GetItemAsync("98722279");
            return item.ToJsonPretty();
        }
    }
}