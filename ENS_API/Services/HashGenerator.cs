namespace ENS_API.Services
{
    public class HashGenerator
    {
        public string GenerateUUID()
        {
            return Guid.NewGuid().ToString();
        }
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public bool Verify(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}
