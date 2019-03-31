using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace DynDBRestService
{
    [DynamoDBTable("Policy")]
    public class Policy
    {
        [DynamoDBHashKey] // Set the variable below to represent the json attribute
        public string PolicyId; //"id"

        [DynamoDBProperty("PolicyType")]
        public string PolicyType;

        [DynamoDBProperty("Coverages")]
        public List<string> Coverages;

        [DynamoDBProperty("Insured")]
        public Insured Insured;

        public bool coveredInd;
    }
}