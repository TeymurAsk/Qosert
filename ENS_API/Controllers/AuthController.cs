using ENS_API.Data;
using ENS_API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ENS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly HashGenerator _hashGenerator;
        private readonly ENSDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(AuthService authService, IHttpContextAccessor httpContextAccessor, HashGenerator hashGenerator, ENSDbContext context)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _hashGenerator = hashGenerator;
            _context = context;
        }
        // POST api/<AuthController>
        [HttpPost("login")]
        public void LoginUser(string email, string password)
        {
            _authService.Login(email, password, _httpContextAccessor.HttpContext);
        }
        // POST api/<AuthController>
        [HttpPost("register")]
        public void RegisterUser(string email, string password, string phone_number)
        {
            var newuser = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _hashGenerator.Generate(password),
                PhoneNumber = phone_number,
                Contacts = new List<Contact>(),
            };
            if (newuser == null)
            {
                BadRequest("Please fill form properly, the app wasn't build for manual testing");
            }
            _context.Users.Add(newuser);
            _context.SaveChanges();
            _authService.Login(email, password, _httpContextAccessor.HttpContext);
        }
    }
}
