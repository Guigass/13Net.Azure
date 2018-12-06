using System;
using Microsoft.Azure.CognitiveServices.Search;
using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
using Newtonsoft.Json;

namespace Bing
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "5783dbc5562e4297a79a3e9f2f0aca2c";

            Console.WriteLine("Digite o nome de uma Personalidade");
            var nome = Console.ReadLine();

            var cliente = new WebSearchClient(new ApiKeyServiceClientCredentials(subscriptionKey));

            var result = cliente.Web.SearchAsync(nome).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.Read();
        }
    }
}
