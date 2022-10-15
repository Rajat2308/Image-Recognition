using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ConsoleApp1
{
    class program
    {
        static string key = "18d498c7fcb341798676cdd09f99c297";
        static string endpoint = "https://rajatjoshi23.cognitiveservices.azure.com/";

        static void Main(string[] args)
        {
            List<string> imagepaths = new List<string>
            {
                @"C:\Users\Pulkit Garg\Pictures\Screenshots\Screenshot (126).png"
 
            };
            // client objrct
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
            foreach (var imagePath in imagepaths)
            {
                AnalyzeImage(client, imagePath).Wait();
            }
        }

        private static async Task AnalyzeImage(ComputerVisionClient client, string imagePath)
        {
            var features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Color
            };

            using (Stream stream = File.OpenRead(imagePath))
            {
                var results = await client.AnalyzeImageInStreamAsync(stream, visualFeatures: features);

                Console.WriteLine("\nDescription:");
                foreach (var caption in results.Description.Captions)
                {
                    Console.WriteLine($"{caption.Text} and confidence {caption.Confidence}");
                }

                Console.WriteLine("\nTags:");
                foreach (var tag in results.Tags)
                {
                    Console.WriteLine($"{tag.Name}");

                }

                Console.WriteLine("\nCategories:");
                foreach (var category in results.Categories)
                {
                    Console.WriteLine($"{category.Name} confidence {category.Score}");
                }

            }
        }
    }
}
