using Azure.AI.TextAnalytics;

namespace EmotionRecognition_FunTime.Models
{
    public class TextAnalyticModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Sentiment { get; set; }
        public string Time { get; set; }

        public TextAnalyticModel()
        {
            Id = Guid.NewGuid();
            Location = "";
            Name = "";
            Sentiment = "";
            Time = "";
        }
    }
}
