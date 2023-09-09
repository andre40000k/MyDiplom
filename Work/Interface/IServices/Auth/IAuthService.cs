using LoginComponent.Models.Request.Auth;
using LoginComponent.Models.Responses.Auth;
using LoginComponent.Models.Responses.Token;

namespace LoginComponent.Interface.IServices.Auth
{
    public interface IAuthService
    {
        Task<SignUpResponse> SignUpAsync(SingUpRequest singUpRequest);
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<LogoutResponse> LogoutAsync(Guid userId);
        Task<UserResponse> GetInfoAsync(Guid userId);

    }
}
