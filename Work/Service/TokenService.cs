using LoginComponent.Helpers;
using LoginComponent.Interface.IRepositories;
using LoginComponent.Interface.IServices;
using LoginComponent.Models;
using LoginComponent.Models.Request;
using LoginComponent.Models.Responses.Token;

namespace LoginComponent.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepositories _tokenRepositories;

        public TokenService(ITokenRepositories tokenRepositories)
        {
            _tokenRepositories = tokenRepositories;
        }

        public async Task<Tuple<string, string>> GenerateTokenAsync(Guid userId)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(userId);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

            var userRecord = await _tokenRepositories.GetUserAsync(userId);

            if (userRecord == null)
            {
                return null;
            }

            var salt = PasswordHelper.GetSecureSalt();

            var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, salt);

            if(userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);
            }

            var newRefreshToken = new RefreshToken
            {
                ExpiryTime = DateTime.Now.AddDays(10),
                Ts = DateTime.Now,
                UserId = userId,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt),
            };

            var saveToken = await _tokenRepositories.AddUserTokenAsync(userRecord, newRefreshToken);

            if (saveToken == 0)
            {
                return null;
            }

            var token = new Tuple<string, string>(accessToken, refreshToken);
            return token;
        }

        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userRecord = await _tokenRepositories.GetUserAsync(user.Id);

            if (userRecord == null)
                return false;

            if(userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                _tokenRepositories.RemoveUserToken(userRecord);
            }

            return false;
        }

        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _tokenRepositories.GetTokenAsync(refreshTokenRequest.UserId);

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
