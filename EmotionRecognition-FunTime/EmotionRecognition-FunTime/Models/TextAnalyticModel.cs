using Azure.AI.TextAnalytics;

namespace EmotionRecognition_FunTime.Models
{
    public class TextAnalyticModel
    {
        public List<string> location { get; set; } = new List<string>();
        public List<string> name { get; set; } = new List<string>();
        public TextSentiment? reason { get; set; }
        public List<string> time { get; set; } = new List<string>();
        public CategorizedEntityCollection? NER { get; set; }
        public DocumentSentiment? DS { get; set; }

        public TextAnalyticModel()
        {
            location = new List<string>();
            name = new List<string>();
            reason = null;
            time = new List<string>();
            NER = null;
            DS = null;
        }
    }
}
