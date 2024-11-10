using ENS_API.Data;
using Microsoft.EntityFrameworkCore;

namespace ENS_API.Services
{
    public class AuthService
    {
        private readonly HashGenerator _hashGenerator;
        private readonly JWTProvider _jwtProvider;
        private readonly ENSDbContext _dbContext;
        public AuthService(HashGenerator hashGenerator, JWTProvider jWTProvider, ENSDbContext dbContext)
        {
            _hashGenerator = hashGenerator;
            _jwtProvider = jWTProvider;
            _dbContext = dbContext;
        }
        public string Register(string email, string password,string phone_number)
        {
            if((_dbContext.Users.SingleOrDefault(u => u.Email == email)!=null) || (_dbContext.Users.SingleOrDefault(u => u.PhoneNumber == phone_number) != null))
            {
                return "Email or phone number already in use of someone else...";
            }
            var newuser = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _hashGenerator.Generate(password),
                PhoneNumber = phone_number,
                Contacts = new List<Contact>(),
            };
            _dbContext.Users.Add(newuser);
            _dbContext.SaveChanges();
            return Login(email, password);
        }
        public string Login(string email, string password)
        {
            var token = CreateJWT(email, password);
            return token;
        }
        public string CreateJWT(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            var result = _hashGenerator.Verify(password, user.PasswordHash);
            if (result == false)
            {
                throw new Exception("Login credetials are false!");
            }
            var token = _jwtProvider.GenerateToken(user);
            return token;
        }
    }
}
