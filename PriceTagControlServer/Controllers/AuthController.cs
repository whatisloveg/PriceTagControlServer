using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceTagControlServer.ApplictaionContext.Models;
using PriceTagControlServer.ApplictaionContext.Repositories.Interfeces;

namespace PriceTagControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Registration(string email, string phoneNumber, string password)
        {
            var isUsesExist = await _userRepository.GetByEmail(email);
            if (isUsesExist != null) 
            {
                return BadRequest("Is user exist");
            }

            User user = new User {
                Id = Guid.NewGuid(),
                Email = email, PhoneNumber = phoneNumber,
                Password = password
            };

            Guid newUserId = await _userRepository.Create(user);
            return newUserId;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Login(string email , string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user.Password == password) 
            {
                return Ok(user);
            }
            return BadRequest("Auth Error");
        }
    }
}
