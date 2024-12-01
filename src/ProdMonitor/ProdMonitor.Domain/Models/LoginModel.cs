namespace ProdMonitor.Domain.Models
{
    public class LoginModel(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
