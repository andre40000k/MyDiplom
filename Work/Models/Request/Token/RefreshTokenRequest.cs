namespace LoginComponent.Models.Request.Token
{
    public class RefreshTokenRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
