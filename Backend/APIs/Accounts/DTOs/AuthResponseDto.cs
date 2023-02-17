namespace Accounts.DTOs
{
    public class AuthResponseDto
    {
        public Boolean IsAuthSuccessful { get; set; }
        public String ErrorMessage { get; set; }
        public String Token { get; set; }
        public String Provider { get; set; }
    }
}
