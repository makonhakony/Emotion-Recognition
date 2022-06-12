using Microsoft.EntityFrameworkCore;

namespace EmotionRecognition_FunTime.Models
{
    public class FunTimeDbContext : DbContext
    {
        public FunTimeDbContext(DbContextOptions<FunTimeDbContext> options): base(options)
        {

        }
        
        public DbSet<UserQuestion> QuestionUsers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
