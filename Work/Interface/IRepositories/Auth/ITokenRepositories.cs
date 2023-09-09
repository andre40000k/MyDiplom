using LoginComponent.Models;

namespace LoginComponent.Interface.IRepositories.Auth
{
    public interface ITokenRepositories
    {
        Task<User?> GetUserAsync(Guid id);
        Task<int> AddUserTokenAsync(User user, RefreshToken refreshToken);
        void RemoveUserToken(User user);
        Task<int> RemoveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetTokenAsync(Guid id);
    }
}
