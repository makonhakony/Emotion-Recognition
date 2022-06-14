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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WebApiDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
