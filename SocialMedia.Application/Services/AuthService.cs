using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Helpers;
using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Requests;
using SocialMedia.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Properties
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IResponseHelper _responseHelper;
        private readonly JwtTokenConfig _jwtTokenConfig;
        #endregion

        #region Methods

        public AuthService(UserManager<IdentityUser> userManager, IMapper mapper,
            IResponseHelper responseHelper, JwtTokenConfig jwtTokenConfig)
        {
            _userManager = userManager;
            _mapper = mapper;
            _responseHelper = responseHelper;
            _jwtTokenConfig = jwtTokenConfig;
        }

        public async Task<RegisterUserResponse> Register(RegisterUserRequest request)
        {
            // to do add validation for validate request 

            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email  // you can put usersname 
            };

            var createUserResult = await _userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
            {
                return _responseHelper
                    .GenerateErrorResponse<RegisterUserResponse>
                    (string.Join(",", createUserResult.Errors.Select(a => a.Description)), (int)HttpStatusCode.BadRequest);
            }

            var createPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);
            if (!createPasswordResult.Succeeded)
            {
                return _responseHelper
                    .GenerateErrorResponse<RegisterUserResponse>
                    (string.Join(",", createPasswordResult.Errors.Select(a => a.Description)), (int)HttpStatusCode.BadRequest);
            }

            return new RegisterUserResponse
            {
                Status = (int)APIResponseEnum.Success,
                MessageDTOs = new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Message = "Register Successflly"
                    }
                }
            };

        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            // to do validate login request

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return _responseHelper
                    .GenerateErrorResponse<LoginResponse>
                    (string.Join(",", "User is not found"), (int)HttpStatusCode.BadRequest);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
            {
                return _responseHelper
                    .GenerateErrorResponse<LoginResponse>
                    (string.Join(",", "User or Password is not match"), (int)HttpStatusCode.BadRequest);
            }

            return new LoginResponse
            {
                Data = new LoginDTO
                {
                    AccessToken = GenerateToken(user.Id),
                },
                MessageDTOs = new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Message = "Login successfully"
                    }
                },
                Status = (int)APIResponseEnum.Success,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        #endregion
        #region Private Methods
        private string GenerateToken(string userId)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.AccessTokenExpiration);
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("user_Id", userId, ClaimValueTypes.String));



            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: _jwtTokenConfig.Issuer,
                        audience: _jwtTokenConfig.Audience,
                        subject: claimsIdentity,
                        notBefore: issuedAt,
                        expires: expires,
                        signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        #endregion
    }
}
