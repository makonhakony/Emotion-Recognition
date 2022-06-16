﻿using Microsoft.AspNetCore.Mvc;
using Azure;
using System;
using System.Globalization;
using Azure.AI.TextAnalytics;
using System.Text.Json;
using EmotionRecognition_FunTime.Models;
using Microsoft.EntityFrameworkCore;

namespace EmotionRecognition_FunTime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("23919de0b43e4dafb9c1c0f73a8bf151");
        private static readonly Uri endpoint = new Uri("https://ft-language-studio-service.cognitiveservices.azure.com/");

        private readonly ILogger<TextAnalysisController> _logger;
        private readonly FunTimeDbContext _dbContext;

        TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);
        public TextAnalysisController(
            ILogger<TextAnalysisController> logger,
            FunTimeDbContext context)
        {
            _dbContext = context;
            _logger = logger;
        }
        //TODO
        [HttpGet]
        [Route("Get")]
        public CategorizedEntityCollection TextAnalyticsGet()
        {
            String result = "How was {0} in {1} at {2}";
            Console.WriteLine(String.Format(result, "An", "DisneyLand","today"));
            return EntityRecognition("I had a wonderful trip to Seattle last week.");
        }

        [HttpPost]
        [Route("PostAnalytics")]
        public UserQuestion? TextAnalyticsPost(IFormCollection input)
        {
            UserQuestion question;
            
            if( input["QuestionId"] == Guid.Empty || !input.ContainsKey("QuestionId"))
            {
                if (input["UserId"] == Guid.Empty || input["UserId"].ToString() == null)
                {
                    User user = new User();
                    question = new UserQuestion(user.Id);
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                }
                else
                {
                    question = new UserQuestion(new Guid(input["UserId"]));
                }

            }
            else
            {
                UserQuestion oldQuestion = GetOldAnalytics( new Guid(input["QuestionId"]));
                question = new UserQuestion(
                    new Guid(input["QuestionId"]),
                    new Guid(input["UserId"]),
                    oldQuestion.QuestionAnalytics
                    );
            }
            question.QuestionText = input["Text"];

            var NER = EntityRecognition(question.QuestionText);
            foreach (var ner in NER)
            {
                if (ner.Category == "Location")
                {
                    question.QuestionAnalytics.location = question.QuestionAnalytics.location == ""
                        ? ner.Text
                        : question.QuestionAnalytics.location + "," + ner.Text;

                }
                else if (ner.Category == "DateTime")
                {
                    question.QuestionAnalytics.time = question.QuestionAnalytics.time == ""
                        ? ner.Text
                        : question.QuestionAnalytics.time + "," + ner.Text;
                }
                else if (ner.Category == "Person")
                {
                    question.QuestionAnalytics.name = question.QuestionAnalytics.name == ""
                        ? ner.Text
                        : question.QuestionAnalytics.name + "," + ner.Text;
                }
            }

            var DS = SentimentAnalysis(question.QuestionText);
            foreach (var ds in DS.Sentences)
            {
                if (ds.Sentiment == TextSentiment.Positive)
                {
                    question.QuestionAnalytics.sentiment += "P"; 
                }
                else if (ds.Sentiment == TextSentiment.Neutral)
                {
                    question.QuestionAnalytics.sentiment += "X";
                }
                else if (ds.Sentiment == TextSentiment.Negative)
                {
                    question.QuestionAnalytics.sentiment += "N";
                }
                else
                {
                    question.QuestionAnalytics.sentiment += "Q";
                }
            }

            _dbContext.QuestionUsers.Add(question);
            _dbContext.SaveChanges();

            return question;
        }

        private UserQuestion GetOldAnalytics(Guid quesionId)
        {
            return _dbContext
                .QuestionUsers
                .Include(x => x.QuestionAnalytics)
                .First(x => x.Id == quesionId);
        }

        private CategorizedEntityCollection EntityRecognition(string Text)
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

        private DocumentSentiment SentimentAnalysis ( string Text)
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
