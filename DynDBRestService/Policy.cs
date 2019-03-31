using Newtonsoft.Json;
using System.Collections.Generic;

namespace DynDBRestService
{
    public class Policy
    {
        [JsonProperty("policyId")] // Set the variable below to represent the json attribute
        public string PolicyId; //"id"

        [JsonProperty("policyType")]
        public string PolicyType;

        [JsonProperty("coverages")]
        public List<string> Coverages;

        //[JsonProperty("insured")]
        //public Insured Insured;

        public Policy(string policyId, string policyType, List<string> coverages)
        //, Insured insured)
        {
            PolicyId = policyId;
            PolicyType = policyType;
            Coverages = coverages;
            //Insured.FirstName = insured.FirstName;
            //Insured.LastName = insured.LastName;
        }
    }
}