﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace EmotionRecognition_FunTime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KnowledgebaseController : ControllerBase
    { 
        [HttpGet]
        [Route("Get")]
        public string get()
        {
            return "hello";
        }

        [HttpPost]
        [Route("MakeRequest")]
        public async Task<HttpResponseMessage > MakeRequest(IFormCollection data)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);


            // Request headers


            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfc28ddcb40a4ff2a95cd393ef517736");//key
            //var queryingURL = "https://ft-qna.azurewebsites.net";
            var uri = "https://ft-qna.cognitiveservices.azure.com/qnamaker/v4.0/knowledgebases/create?" + queryString;

            HttpResponseMessage response;

            // Request body
            string body = JsonSerializer.Serialize(data);
            //string body = JsonConvert.SerializeObject(data);
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }

            return response;
        }
    }
}
