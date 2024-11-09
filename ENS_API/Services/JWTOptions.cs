namespace ENS_API.Services
{
    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}