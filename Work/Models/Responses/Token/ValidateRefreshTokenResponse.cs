namespace LoginComponent.Models.Responses.Token
{
    public class ValidateRefreshTokenResponse : BaseResponse
    {
        public Guid UserId { get; set; }
    }
}
