namespace EmotionRecognition_FunTime.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public User (string name)
        {
            Id = new Guid();
            Name = name;
        }
    }
}
