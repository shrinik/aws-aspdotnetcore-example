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
            var scanConditions = new List<ScanCondition>();

            if (!string.IsNullOrEmpty(policyToFind.PolicyId))
            {
                scanConditions.Add(new ScanCondition("PolicyId", ScanOperator.Equal, policyToFind.PolicyId));
            }

            if (!string.IsNullOrEmpty(policyToFind.Coverages[0]))
            {
                scanConditions.Add(new ScanCondition("Coverages", ScanOperator.Contains, policyToFind.Coverages[0]));
            }

            var policySearch = context.ScanAsync<Policy>(scanConditions);

            var policies = await policySearch.GetNextSetAsync();

            policyToFind.coveredInd = policies.Count > 0 ? true : false;
            return policyToFind;
        }
    }
}