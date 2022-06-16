using EmotionRecognition_FunTime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmotionRecognition_FunTime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly FunTimeDbContext _dbContext;
        public UserController(
            ILogger<UserController> logger,
            FunTimeDbContext context
            )
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetId")]
        public User Getuser(string? Name)
        {
            User user =  Name != null
                ? new User(Name)
                : new User();
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        [HttpDelete]
        [Route("DeleteId")]
        public void DeleteUser(Guid Id)
        {
            User? deleteUser = _dbContext.Users.FirstOrDefault(x => x.Id == Id);
            if (deleteUser != null)
            {
                _dbContext.Users.Remove(deleteUser);
                _dbContext.SaveChanges();
            }
            
        }
    }
}
