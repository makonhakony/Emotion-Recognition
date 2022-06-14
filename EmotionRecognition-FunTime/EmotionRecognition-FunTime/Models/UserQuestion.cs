namespace EmotionRecognition_FunTime.Models
{
    public class UserQuestion
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public TextAnalyticModel QuestionAnalytics { get; set; }
        public Guid? FollowedUpQuestion { get; set; }
        public Guid UserId { get; set; }

        public UserQuestion()
        {
            QuestionText = string.Empty;
            Id = Guid.Empty;
            FollowedUpQuestion = Guid.Empty;
            UserId = Guid.Empty;
            QuestionAnalytics = new TextAnalyticModel();
        }

        public UserQuestion(Guid questionId, Guid userId)
        {
            Id = new Guid();
            UserId = userId;
            FollowedUpQuestion = questionId;
            QuestionText = string.Empty;
            QuestionAnalytics = new TextAnalyticModel();
        }
    }
}
