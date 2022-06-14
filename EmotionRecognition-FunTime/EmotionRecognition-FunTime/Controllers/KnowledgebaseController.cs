using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace EmotionRecognition_FunTime.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class KnowledgebaseController : ControllerBase
    { 
        [HttpPost]
        [Route("Get")]
        public string get()
        {
            
            return "hello";
        }

        [HttpPost]
        [Route("MakeRequest")]
        public async Task<string> MakeRequest(IFormCollection data) //IFormCollection
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string result;

            // Request headers
            //string d = data.ToString();


            client.DefaultRequestHeaders.Add("Authorization", "EndpointKey e3f70fd0-caae-4363-acaa-bd6f314e030a");//key
            //var queryingURL = "https://ft-qna.azurewebsites.net";
            var uri = "https://ft-coreui.azurewebsites.net/qnamaker/knowledgebases/f846516d-e0cd-4595-b21c-2155b2c22523/generateAnswer" + queryString;

            HttpResponseMessage response;

            // Request body
            //string body = JsonSerializer.Serialize(data);

            //byte[] byteData = Encoding.UTF8.GetBytes("{\"question\" : \"I am happy\"}");

            string body = "{\"question\" : " + "\"" + data["question"] + "\"}";
            byte[] byteData = Encoding.UTF8.GetBytes(body);
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            
            return result;
        }
    }
}
