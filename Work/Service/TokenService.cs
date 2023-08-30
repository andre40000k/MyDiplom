using LoginComponent.Helpers;
using LoginComponent.Interface.IServices;
using LoginComponent.LoginDataBase;
using LoginComponent.Models;
using LoginComponent.Models.Request;
using LoginComponent.Models.Responses.Token;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.Service
{
    public class TokenService : ITokenService
    {
        private readonly LoginContext _loginContext;

        public TokenService(LoginContext loginContext)
        {
            _loginContext = loginContext;
        }

        public async Task<Tuple<string, string>> GenerateTokenAsync(Guid userId)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(userId);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

            var userRecord = await _loginContext.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(user => user.Id == userId);

            if(userRecord == null)
            {
                return null;
            }

            var salt = PasswordHelper.GetSecureSalt();

            var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, salt);

            if(userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);
            }

            userRecord.RefreshTokens?.Add(new RefreshToken
            {
                ExpiryTime= DateTime.Now.AddDays(10),
                Ts = DateTime.Now,
                UserId = userId,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt),
            });

            await _loginContext.SaveChangesAsync();

            var token = new Tuple<string, string>(accessToken, refreshToken);
            return token;
        }

        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userRecord = await _loginContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(y => y.Id == user.Id);

            if (userRecord == null)
                return false;

            if(userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                var currentRefreshToken = userRecord.RefreshTokens.First();
                _loginContext.RefreshTokens.Remove(currentRefreshToken);
            }

            return false;
        }

        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _loginContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == refreshTokenRequest.UserId);
            var response = new ValidateRefreshTokenResponse();

            if(refreshToken == null)
            {
                response.Success= false;
                response.Error = "Invalid session or user is already logged out";
                response.ErrorCode = "401";
                return response;
            }

            var refreshTokenToValidateHash = PasswordHelper.HashUsingPbkdf2(
                refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));

            if (refreshToken.TokenHash != refreshTokenToValidateHash)
            {
                response.Success = false;
                response.Error = "Invalid refresh token";
                response.ErrorCode = "401";
                return response;
            }

            if (refreshToken.ExpiryTime < DateTime.Now)
            {
                response.Success = false;
                response.Error = "Refresh token has expired";
                response.ErrorCode = "401";
                return response;
            }

            response.Success = true;
            response.UserId = refreshToken.UserId;
            return response;    

        }
    }
}
