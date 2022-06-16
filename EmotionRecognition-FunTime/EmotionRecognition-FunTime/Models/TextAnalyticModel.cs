using Azure.AI.TextAnalytics;

namespace EmotionRecognition_FunTime.Models
{
    public class TextAnalyticModel
    {
        public Guid Id { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string sentiment { get; set; }
        public string time { get; set; }

        public TextAnalyticModel()
        {
            Id = Guid.NewGuid();
            location = "";
            name = "";
            sentiment = "";
            time = "";
        }
    }
}
