using Microsoft.AspNetCore.Mvc;
using Azure;
using System;
using System.Globalization;
using Azure.AI.TextAnalytics;
using System.Text.Json;
using EmotionRecognition_FunTime.Models;

namespace EmotionRecognition_FunTime.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("4f3b6ced45c147f1a444ecae676f72d0");
        private static readonly Uri endpoint = new Uri("https://ft-text-analytics-2.cognitiveservices.azure.com/");

        private readonly ILogger<TextAnalysisController> _logger;
        TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);
        public TextAnalysisController(ILogger<TextAnalysisController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ERGet")]
        public CategorizedEntityCollection EntityRecognitionGet()
        {
            var response = client.RecognizeEntities("I had a wonderful trip to Seattle last week.");
            Console.WriteLine("Named Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                Console.WriteLine($"\t\tScore: {entity.ConfidenceScore:F2},\tLength: {entity.Length},\tOffset: {entity.Offset}\n");
            }
            return response.Value;
        }

        [HttpPost]
        [Route("ERPost")]
        public CategorizedEntityCollection EntityRecognition(string Text)
        {
            var response = client.RecognizeEntities(Text);
            Console.WriteLine("Named Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                Console.WriteLine($"\t\tScore: {entity.ConfidenceScore:F2},\tLength: {entity.Length},\tOffset: {entity.Offset}\n");
            }
            return response.Value;
        }

        [HttpGet("test")]
        public string Test()
        {
            Console.WriteLine("test");
            return "test";
        }
    }
}
