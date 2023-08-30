using LoginComponent.Models;
using LoginComponent.Models.Request;
using LoginComponent.Models.Responses.Token;

namespace LoginComponent.Interface.IServices
{
    public interface ITokenService
    {
        Task<Tuple<string, string>> GenerateTokenAsync(Guid userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(User user);
    }
}
