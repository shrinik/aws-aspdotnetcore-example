using Newtonsoft.Json;

namespace DynDBRestService
{
    public class Insured
    {
        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("lastName")]
        public string LastName;
    }
}