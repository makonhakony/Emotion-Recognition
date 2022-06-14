﻿using Microsoft.AspNetCore.Mvc;
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
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("23919de0b43e4dafb9c1c0f73a8bf151");
        private static readonly Uri endpoint = new Uri("https://ft-language-studio-service.cognitiveservices.azure.com/");

        private readonly ILogger<TextAnalysisController> _logger;
        TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);
        public TextAnalysisController(ILogger<TextAnalysisController> logger)
        {
            _logger = logger;
        }
        //TODO
        [HttpGet]
        [Route("ERGet")]
        public CategorizedEntityCollection TextAnalyticsGet()
        {
            return EntityRecognition("I had a wonderful trip to Seattle last week.");
        }

        [HttpPost]
        [Route("ERPost")]
        public TextAnalyticModel? TextAnalyticsPost(IFormCollection input)
        {
            TextAnalyticModel item = new TextAnalyticModel();
            item.NER = EntityRecognition(input["Text"]);
            foreach (var ner in item.NER)
            {
                if (ner.SubCategory == "GPE")
                {
                    item.location.Add(ner.Text);
                }
                else if (ner.SubCategory == "DateRange")
                {
                    item.time.Add(ner.Text);
                }
                //TODO
                else if (ner.SubCategory == "Person")
                {
                    item.name.Add(ner.Text);    
                }
            }

            item.DS = SentimentAnalysis(input["Text"]);
            item.reason = item.DS.Sentiment;
            
            return item;
        }

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

        public DocumentSentiment SentimentAnalysis ( string Text)
        {
            DocumentSentiment documentSentiment = client.AnalyzeSentiment(Text);
            Console.WriteLine($"Document sentiment: {documentSentiment.Sentiment}\n");

            foreach (var sentence in documentSentiment.Sentences)
            {
                Console.WriteLine($"\tText: \"{sentence.Text}\"");
                Console.WriteLine($"\tSentence sentiment: {sentence.Sentiment}");
                Console.WriteLine($"\tPositive score: {sentence.ConfidenceScores.Positive:0.00}");
                Console.WriteLine($"\tNegative score: {sentence.ConfidenceScores.Negative:0.00}");
                Console.WriteLine($"\tNeutral score: {sentence.ConfidenceScores.Neutral:0.00}\n");
            }

            return documentSentiment;
        }
    }
}
