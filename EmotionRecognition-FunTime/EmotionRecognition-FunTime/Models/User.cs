namespace EmotionRecognition_FunTime.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Name = null;
        }

        public User (string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
