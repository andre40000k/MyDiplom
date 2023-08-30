using LoginComponent.Models.Request;
using LoginComponent.Models.Responses.Auth;
using LoginComponent.Models.Responses.Token;

namespace LoginComponent.Interface.IServices
{
    public interface IUserService
    {
        Task<SignUpResponse> SignUpAsync(SingUpRequest singUpRequest);
        Task<TokenResponse> LoginAsinc(LoginRequest loginRequest);
        Task<LogoutResponse> LogoutAsync(Guid userId);
        Task<UserResponse> GetInfoAsync(Guid userId);

    }
}
