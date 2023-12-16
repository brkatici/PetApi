using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulPetApi.Authentication;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("[controller]/AddUser")]
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        [HttpGet]
        [Route("[controller]/GetSpecificUser{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {

            var user = _context.Users.FirstOrDefault(u=>u.UserId == userId);
            if (user == null)
            {
                return NotFound(); // Belirtilen userId'ye sahip user bulunamadı
            }
            return Ok(user);

        }

        [HttpGet]
        [Route("[controller]/GetAllUsers")]
        public List<User> GetAllUsers()
        {
            List<User> users = _context.Users.ToList();       
            return users;
        }
    }
}
