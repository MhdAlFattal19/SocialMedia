using SocialMedia.Domain.Responses;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Models.CustomModels;

namespace SocialMedia.Domain.Contracts
{
    public interface IResponseHelper
    {
        string GetExceptionFormattedMessage(Exception ex);
        APIResponse GenerateCustomExceptionResponse(string message, APIResponseEnum status);
        APIResponse GenerateExceptionResponse(Exception ex, string languageCode, string separator = " ");
        APIResponse GenerateResponse(dynamic data, string messageKeys, string languageCode, APIResponseEnum status, string separator = " ");
        APIResponse GenerateResponse(BaseServiceResponse response, string languageCode, string separator = " ");
        T GenerateErrorResponse<T>(List<string> messages, int statusCode) where T : BaseServiceResponse, new();
        T GenerateErrorResponse<T>(string message, int statusCode, List<string> parameters = null, int status = 0) where T : BaseServiceResponse, new();
    }
}
