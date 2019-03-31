using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynDBRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private static IAmazonDynamoDB DynamoDBClient { get; set; }

        public PolicyController(IAmazonDynamoDB dynamoDBClient)
        {
            DynamoDBClient = dynamoDBClient;
        }

        [HttpPost]
        public async Task<Policy> PostAsync(Policy policy)
        {
            var context = new DynamoDBContext(DynamoDBClient);
            var retrievedPolicy = await RetrieveAsync(context, policy);
            return retrievedPolicy;
        }

        private static async Task<Policy> RetrieveAsync(DynamoDBContext context, Policy policyToFind)
        {
            var policySearch = context.ScanAsync<Policy>(new List<ScanCondition> {
                new ScanCondition("PolicyId", ScanOperator.Equal, policyToFind.PolicyId)
                ,new ScanCondition("Coverages", ScanOperator.Contains, policyToFind.Coverages[0])
            });

            var policies = await policySearch.GetNextSetAsync();

            if (policies.Count == 1)
            {
                var pols = policies.ToArray();
                pols[0].coveredInd = true;
                return pols[0];
            }
            else
            {
                policyToFind.coveredInd = false;
                return policyToFind;
            }
        }
    }
}