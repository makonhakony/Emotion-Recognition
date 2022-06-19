using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Http;
using EmotionRecognition_FunTime.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EmotionRecognition_FunTime.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class KnowledgebaseController : ControllerBase
    {
        [HttpPost]
        [Route("MakeRequest")]
        public async Task<QnaModel> MakeRequest(IFormCollection data) //IFormCollection
        {
            HttpClient client = new HttpClient();
            string uri;
            string body;
            string result;
            bool isNeutral = true;
            string text = data["Text"].ToString();

            if (text.Length == 1)
            {
                if (text != "x")
                {
                    text += "s";
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text.Substring(i) != "x")
                {
                    isNeutral = false;
                }
            }

            if (isNeutral)
            {
                client.DefaultRequestHeaders.Add("Authorization", "EndpointKey e3f70fd0-caae-4363-acaa-bd6f314e030a");

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                uri = "https://ft-coreui.azurewebsites.net/qnamaker/knowledgebases/f846516d-e0cd-4595-b21c-2155b2c22523/generateAnswer" + queryString;
                body = "{\"question\" : " + "\"" + data["question"] + "\"}";

            }
            else //Complex Structure
            {
                client.DefaultRequestHeaders.Add("Authorization", "EndpointKey e3f70fd0-caae-4363-acaa-bd6f314e030a");

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                uri = "https://ft-coreui.azurewebsites.net/qnamaker/knowledgebases/3ef2c6e1-5e29-46db-bae6-9bcef2127dd1/generateAnswer" + queryString;
                body = "{\"question\" : " + "\"" + text + "\"}";

            }

            HttpResponseMessage response;

            byte[] byteData = Encoding.UTF8.GetBytes(body);
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }

            JObject convert  = JObject.Parse(result);
            string val = isNeutral
                ? convert["answers"][0]["answer"].ToString()
                : String.Format(convert["answers"][0]["answer"].ToString()
                    , SplitLastChar(data["Name"].ToString())
                    , SplitLastChar(data["Location"].ToString())
                    , SplitLastChar(data["Time"].ToString())
                    );
            QnaModel returnVal = new QnaModel(val);
            return returnVal;
        }

        public string SplitLastChar(string input)
        {
            string result;
            result = input.Split(",").ToList().LastOrDefault();

            return result;
        }
    }
}