using LoginComponent.DataBase;
using LoginComponent.Models;
using Microsoft.EntityFrameworkCore;
using LoginComponent.Interface.IRepositories.Auth;

namespace LoginComponent.Repositories.Auth
{
    public class TokenRepositories : ITokenRepositories
    {
        private readonly AplicationContext _loginContext;

        public TokenRepositories(AplicationContext loginContext)
        {
            _loginContext = loginContext;
        }

        public async Task<int> AddUserTokenAsync(User user, RefreshToken refreshToken)
        {
            user.RefreshTokens.Add(refreshToken);

            return await _loginContext.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetTokenAsync(Guid id)
        {
            return await _loginContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await _loginContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<int> RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            _loginContext.RefreshTokens.Remove(refreshToken);

            return await _loginContext.SaveChangesAsync();
        }

        public void RemoveUserToken(User user)
        {
            var currentRefreshToken = user.RefreshTokens.First();
            _loginContext.RefreshTokens.Remove(currentRefreshToken);
        }
    }
}
