using Microsoft.AspNetCore.Mvc;
using Azure;
using System;
using System.Globalization;
using Azure.AI.TextAnalytics;

namespace EmotionRecognition_FunTime.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private readonly ILogger<TextAnalysisController> _logger;

        public TextAnalysisController(ILogger<TextAnalysisController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return "test";
        }
    }
}
