using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynDBRestService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public ActionResult<string> Get()
        {
            return "You invoked the GET method";
            // var policy = JsonConvert.DeserializeObject<Policy>(input);
            //return JsonConvert.SerializeObject(policy);
            //return await GetDataFromDb(policy);
        }

        //private static async Task<string> GetDataFromDb(Policy policy)
        //{
        //    var table = Table.LoadTable(DynamoDBClient, "policy");
        //    var scanFilter = new ScanFilter();
        //    scanFilter.AddCondition("coverages", ScanOperator.Contains, "AA");

        //    var item = await table.GetItemAsync("98722279");
        //    return item.ToJsonPretty();
        //}
        //[HttpPost]
        //public async Task<Book> PostAsync()
        //{
        //    //var book = JsonConvert.DeserializeObject<Book>(input);

        //    var context = new DynamoDBContext(DynamoDBClient);
        //    var book = await CreateBookAsync(context);
        //    return book;
        //}

        [HttpPost]
        public async Task<Book> PostAsync(Book book)
        {
            var context = new DynamoDBContext(DynamoDBClient);
            var retrievedBook = await RetrieveBookAsync(context, book.Id);
            return retrievedBook;
        }

        /*private static async Task<Book> TestCRUDOperationsAsync(DynamoDBContext context)
        {
            int bookID = await CreateAsync(context);

            // Update few properties.
            //bookRetrieved.ISBN = "222-2222221001";
            //bookRetrieved.BookAuthors = new List<string> { " Author 1", "Author x" }; // Replace existing authors list with this.
            //await context.SaveAsync(bookRetrieved);

            // Retrieve the updated book. This time add the optional ConsistentRead parameter using DynamoDBContextConfig object.
            //Book updatedBook = await context.LoadAsync<Book>(bookID, new DynamoDBContextConfig
            //{
            //    ConsistentRead = true
            //});

            // Delete the book.
            //await context.DeleteAsync<Book>(bookID);
            //// Try to retrieve deleted book. It should return null.
            //Book deletedBook = await context.LoadAsync<Book>(bookID, new DynamoDBContextConfig
            //{
            //    ConsistentRead = true
            //});
        }*/

        private static async Task<Book> RetrieveBookAsync(DynamoDBContext context, int bookID)
        {
            // Retrieve the book.
            return await context.LoadAsync<Book>(bookID);
        }

        private static async Task<int> CreateBookAsync(DynamoDBContext context)
        {
            Random number = new Random();
            int bookID = number.Next(); // Some unique value.
            Book myBook = new Book
            {
                Id = bookID,
                Title = "Some title",
                ISBN = "111-1111111001",
                BookAuthors = new List<string> { "Author 1", "Author 2" },
            };

            // Save the book.
            await context.SaveAsync(myBook);
            return bookID;
        }
    }
}