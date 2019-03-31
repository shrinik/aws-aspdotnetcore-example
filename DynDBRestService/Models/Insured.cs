using Amazon.DynamoDBv2.DataModel;

namespace DynDBRestService
{
    public class Insured
    {
        [DynamoDBProperty("FirstName")]
        public string FirstName;

        [DynamoDBProperty("LastName")]
        public string LastName;
    }
}