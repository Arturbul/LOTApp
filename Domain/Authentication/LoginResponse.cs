namespace Core.Authentication
{
    public class LoginResponse
    {
        public string JwtToken { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
