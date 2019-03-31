using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace DynDBRestService.Models
{
    [DynamoDBTable("ProductCatalog")]
    public class Book
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty("Title")]
        public string Title { get; set; }

        [DynamoDBProperty("ISBN")]
        public string ISBN { get; set; }

        [DynamoDBProperty("Authors")]
        public List<string> BookAuthors { get; set; }
    }
}