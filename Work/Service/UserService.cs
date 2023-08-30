using LoginComponent.Helpers;
using LoginComponent.Interface.IServices;
using LoginComponent.LoginDataBase;
using LoginComponent.Models;
using LoginComponent.Models.Request;
using LoginComponent.Models.Responses.Auth;
using LoginComponent.Models.Responses.Token;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.Service
{
    public class UserService : IUserService
    {
        private readonly LoginContext _loginContext;
        private readonly ITokenService _tokenService;

        public UserService(LoginContext loginContext, ITokenService tokenService)
        {
            _loginContext = loginContext;
            _tokenService = tokenService;
        }

        public async Task<UserResponse> GetInfoAsync(Guid UserId)
        {
            var user = await _loginContext.Users.FindAsync(UserId);

            if (user == null)
            {
                return new UserResponse
                {
                    Success = false,
                    Error = "No user found",
                    ErrorCode = "401"
                };
            }

            return new UserResponse
            {
                Success = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreationDate = user.Ts
            };
        }

        public async Task<TokenResponse> LoginAsinc(LoginRequest loginRequest)
        {
            var user = _loginContext.Users.SingleOrDefault(
                user => user.IsEmailConfirmed &&
                user.Email == loginRequest.Email);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error= "Email not found",
                    ErrorCode= "401"
                };
            }

            var passWordHash = PasswordHelper.HashUsingPbkdf2(
                loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

            if(user.Password != passWordHash)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Invalid password",
                    ErrorCode = "401"
                };
            }

            var token = await Task.Run(() => _tokenService.GenerateTokenAsync(user.Id));

            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2,
                UserId = user.Id,
                FirstName = user.FirstName
            };
        }

        public async Task<LogoutResponse> LogoutAsync(Guid UserId)
        {
            var refreshToken = await _loginContext.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == UserId);

            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }

            _loginContext.RefreshTokens.Remove(refreshToken);

            var saveResponse = await _loginContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }

            return new LogoutResponse { 
                Success = false, 
                Error = "Unable to logout user", 
                ErrorCode = "401" 
            };

        }
        public async Task<SignUpResponse> SignUpAsync(SingUpRequest singUpRequest)
        {
            var existingUser = await _loginContext.Users.SingleOrDefaultAsync(user => user.Email == singUpRequest.Email);

            if (existingUser != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Error = "User already exists with the same email",
                    ErrorCode = "S02"
                };
            }

            if (singUpRequest.Password != singUpRequest.ConfirmPassword)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Error = "Password and confirm password do not match",
                    ErrorCode = "S03"
                };
            }

            if (singUpRequest.Password.Length <= 7) // This can be more complicated than only length, you can check on alphanumeric and or special characters
            {
                return new SignUpResponse
                {
                    Success = false,
                    Error = "Password is weak",
                    ErrorCode = "S04"
                };
            }

            var salt = PasswordHelper.GetSecureSalt();
            var passwordHash = PasswordHelper.HashUsingPbkdf2(singUpRequest.Password, salt);

            var user = new User
            {
                Email = singUpRequest.Email,
                Password = passwordHash,
                PasswordSalt = Convert.ToBase64String(salt),
                FirstName = singUpRequest.FirstName,
                LastName = singUpRequest.LastName,
                Ts = singUpRequest.Ts,
                IsEmailConfirmed = true // You can save is false and send confirmation email to the user, then once the user confirms the email you can make it true
            };

            await _loginContext.Users.AddAsync(user);

            var saveResponse = await _loginContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SignUpResponse { Success = true, Email = user.Email };
            }

            return new SignUpResponse
            {
                Success = false,
                Error = "Unable to save the user",
                ErrorCode = "S05"
            };
        }
    }
}
