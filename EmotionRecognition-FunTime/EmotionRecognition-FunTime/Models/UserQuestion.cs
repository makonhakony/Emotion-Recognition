namespace EmotionRecognition_FunTime.Models
{
    public class UserQuestion
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public TextAnalyticModel QuestionAnalytics { get; set; }
        public Guid? FollowedUpQuestion { get; set; }
        public Guid UserId { get; set; }
        public bool IsFollowing { get; set; }
        public bool HasUser { get; set; }

        public UserQuestion(Guid userId)
        {
            QuestionText = string.Empty;
            Id = new Guid();
            FollowedUpQuestion = Guid.Empty;
            UserId = userId;
            QuestionAnalytics = new TextAnalyticModel();
            IsFollowing = false;
            HasUser = false;
        }

        public UserQuestion(
            Guid questionId, 
            Guid userId,
            TextAnalyticModel oldAnalytics
            )
        {
            Id = new Guid();
            FollowedUpQuestion = questionId;
            UserId = userId;
            QuestionText = string.Empty;
            QuestionAnalytics = oldAnalytics;
            IsFollowing = true;
            HasUser = true;
        }
    }
}
