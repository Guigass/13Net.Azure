using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;

namespace CognitiveServices
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "002d9cf2be714679abeedb67f0bf137a";
            var imageUrl = "http://goldtrip.com.br/wp-content/uploads/2015/02/GT-Panama.jpg";

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey));
            client.Endpoint = "https://brazilsouth.api.cognitive.microsoft.com/";


            var features = new List<VisualFeatureTypes>
            {
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.Tags,
            };

            var result = client.AnalyzeImageAsync(imageUrl, features).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.Read();
        }
    }
}
