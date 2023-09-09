using FluentValidation;
using LoginComponent.Helpers;
using LoginComponent.Interface.IRepositories.Auth;
using LoginComponent.Interface.IServices.Auth;
using LoginComponent.Models;
using LoginComponent.Models.Request.Auth;
using LoginComponent.Models.Responses.Auth;
using LoginComponent.Models.Responses.Token;

namespace LoginComponent.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenRepositories _tokenRepositories;
        private readonly ITokenService _tokenService;
        private readonly IAuthRepositories _userRepositories;
        private IValidator<SingUpRequest> _validator;

        public AuthService(ITokenService tokenService,
            IAuthRepositories userRepositories,
            ITokenRepositories tokenRepositories,
            IValidator<SingUpRequest> validator)
        {
            _tokenService = tokenService;
            _userRepositories = userRepositories;
            _tokenRepositories = tokenRepositories;
            _validator = validator;
        }

        public async Task<UserResponse> GetInfoAsync(Guid UserId)
        {
            var user = await _userRepositories.GetDataAsync(UserId);

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

        public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepositories.GetDataAsync(loginRequest.Email);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Email not found",
                    ErrorCode = "401"
                };
            }

            var passWordHash = PasswordHelper.HashUsingPbkdf2(
                loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

            if (user.Password != passWordHash)
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
            var refreshToken = await _tokenRepositories.GetTokenAsync(UserId);

            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }

            var saveResponse = await _tokenRepositories.RemoveRefreshTokenAsync(refreshToken);

            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }

            return new LogoutResponse
            {
                Success = false,
                Error = "Unable to logout user",
                ErrorCode = "401"
            };

        }
        public async Task<SignUpResponse> SignUpAsync(SingUpRequest singUpRequest)
        {
            var existingUser = await _userRepositories.GetDataAsync(singUpRequest.Email);

            if (existingUser != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Error = "User already exists with the same email",
                    ErrorCode = "409"
                };
            }

            var result = await _validator.ValidateAsync(singUpRequest);

            if (!result.IsValid)
            {
                var error = result.Errors.FirstOrDefault();
                return new SignUpResponse
                {
                    Success = false,
                    Error = error.ErrorMessage,
                    ErrorCode = error.ErrorCode
                };
            }

            //if (singUpRequest.Password != singUpRequest.ConfirmPassword)
            //{
            //    return new SignUpResponse
            //    {
            //        Success = false,
            //        Error = "Password and confirm password do not match",
            //        ErrorCode = "400"
            //    };
            //}

            //if (singUpRequest.Password.Length <= 7) 
            //{
            //    return new SignUpResponse
            //    {
            //        Success = false,
            //        Error = "Password is weak",
            //        ErrorCode = "400"
            //    };
            //}

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
                IsEmailConfirmed = true
            };

            var saveResponse = await _userRepositories.SaveDataAsync(user);

            if (saveResponse >= 0)
            {
                return new SignUpResponse { Success = true, Email = user.Email };
            }

            return new SignUpResponse
            {
                Success = false,
                Error = "Unable to save the user",
                ErrorCode = "500"
            };
        }
    }
}
