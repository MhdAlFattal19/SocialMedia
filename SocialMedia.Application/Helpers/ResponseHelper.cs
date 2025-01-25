using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Models.CustomModels;
using SocialMedia.Domain.Responses;

namespace SocialMedia.Application.Helpers
{
    public class ResponseHelper : IResponseHelper
    {
        private readonly ICacheManager _cacheManager;
        public ResponseHelper(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public string GetExceptionFormattedMessage(Exception ex)
        {
            return string.Join(" | ", ex.Source, ex.Message, ex.InnerException, ex.StackTrace);
        }
        public APIResponse GenerateExceptionResponse(Exception ex, string languageCode, string separator = " ")
        {
            return new APIResponse
            {
                Status = (int)APIResponseEnum.Exception,
                Messages = new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Message = _cacheManager.GetLocalizedMessages("ERROR", languageCode, " \n"),
                        Type = MessageTypeEnum.Technical
                    }
                },
                ErrorMessage = GetExceptionFormattedMessage(ex)
            };
        }
        public APIResponse GenerateCustomExceptionResponse(string message, APIResponseEnum status)
        {
            return new APIResponse
            {
                Messages = new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Message = _cacheManager.GetLocalizedMessages(message, "en", " "),
                        Type = MessageTypeEnum.Technical
                    }
                },
                Status = (int)status
            };
        }
        public APIResponse GenerateResponse(dynamic data, string messageKeys, string languageCode, APIResponseEnum status, string separator = " ")
        {
            return new APIResponse
            {
                Data = data,
                Status = (int)status
            };
        }
        public APIResponse GenerateResponse(BaseServiceResponse response, string languageCode, string separator = " ")
        {
            var messageDTOs = new List<MessageDTO>();
            if (response.MessageDTOs != null && response.MessageDTOs.Any())
            {
                switch (response.Status)
                {
                    case (int)APIResponseEnum.Success:
                        messageDTOs.Add(new MessageDTO
                        {
                            Message = _cacheManager.GetLocalizedMessages(response.MessageDTOs, languageCode, " \n"),
                            Type = MessageTypeEnum.Information
                        });
                        break;
                    case (int)APIResponseEnum.Failure:
                        var successMessages = response.MessageDTOs.Where(x => x.Type == MessageTypeEnum.Information).ToList();
                        var warningMessages = response.MessageDTOs.Where(x => x.Type == MessageTypeEnum.Business).ToList();
                        var errorMessages = response.MessageDTOs.Where(x => x.Type == MessageTypeEnum.Technical).ToList();
                        if (successMessages != null && successMessages.Count > 0)
                        {
                            messageDTOs.Add(new MessageDTO
                            {
                                Message = _cacheManager.GetLocalizedMessages(successMessages, languageCode, " \n"),
                                Type = MessageTypeEnum.Information
                            });
                        }
                        if (warningMessages != null && warningMessages.Count > 0)
                        {
                            messageDTOs.Add(new MessageDTO
                            {
                                Message = _cacheManager.GetLocalizedMessages(warningMessages, languageCode, " \n"),
                                Type = MessageTypeEnum.Business
                            });
                        }
                        if (errorMessages != null && errorMessages.Count > 0)
                        {
                            messageDTOs.Add(new MessageDTO
                            {
                                Message = _cacheManager.GetLocalizedMessages(errorMessages, languageCode, " \n"),
                                Type = MessageTypeEnum.Technical
                            });
                        }
                        break;
                }
            }

            return new APIResponse
            {
                Data = response.Data,
                Status = (int)response.Status,
                Messages = messageDTOs
            };
        }
        public T GenerateErrorResponse<T>(List<string> messages, int statusCode) where T : BaseServiceResponse, new()
        {
            var messageDTOs = messages.Select(m =>
                new MessageDTO
                {
                    Type = MessageTypeEnum.Business,
                    Message = m
                }
            ).ToList();
            return new T
            {
                Status = (int)APIResponseEnum.Failure,
                MessageDTOs = messageDTOs,
                StatusCode = statusCode
            };
        }
        public T GenerateErrorResponse<T>(string message, int statusCode, List<string> parameters = null, int status = (int)APIResponseEnum.Failure) where T : BaseServiceResponse, new()
        {
            var messageDTOs = new List<MessageDTO>{
                new MessageDTO
                {
                    Type = MessageTypeEnum.Business,
                    Message = message,
                    Parameters = parameters is null ? new List<string>(): parameters
                }
            };
            return new T
            {
                Status = status,
                MessageDTOs = messageDTOs,
                StatusCode = statusCode
            };
        }
    }
}