﻿using LoginComponent.Interface.IServices.Auth;
using LoginComponent.Models.Request.Auth;
using LoginComponent.Models.Request.Token;
using LoginComponent.Models.Responses.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginComponent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing login details",
                    ErrorCode = "L01"
                });
            }

            var loginResponse = await _userService.LoginAsync(loginRequest);

            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.Error
                });
            }

            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("refresh_token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken) || refreshTokenRequest.UserId == null)
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing refresh token details",
                    ErrorCode = "R01"
                });
            }

            var validateRefreshTokenResponse = await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);

            if (!validateRefreshTokenResponse.Success)
            {
                return BadRequest(validateRefreshTokenResponse);
            }

            var tokenResponse = await _tokenService.GenerateTokenAsync(validateRefreshTokenResponse.UserId);

            return Ok(new TokenResponse { AccessToken = tokenResponse.Item1, RefreshToken = tokenResponse.Item2 });
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SingUpRequest signUpRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new TokenResponse
                    {
                        Error = $"{string.Join(",", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            var signupResponse = await _userService.SignUpAsync(signUpRequest);

            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }

            return Ok(signupResponse.Email);
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var logout = await _userService.LogoutAsync(UserID);

            if (!logout.Success)
            {
                return UnprocessableEntity(logout);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> Info()
        {
            var userResponse = await _userService.GetInfoAsync(UserID);

            if (!userResponse.Success)
            {
                return UnprocessableEntity(userResponse);
            }

            return Ok(userResponse);

        }
    }
}
