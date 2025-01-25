using SocialMedia.Domain.Requests;
using SocialMedia.Domain.Responses;

namespace SocialMedia.Domain.Contracts
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegisterUserResponse> Register(RegisterUserRequest request);
    }
}