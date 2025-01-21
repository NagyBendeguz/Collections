namespace Collections.Entities.Dtos.User
{
    public class LoginResultDto
    {
        public string Token { get; set; } = "";

        public DateTime Expiration { get; set; }
    }
}
