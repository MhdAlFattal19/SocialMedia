using SocialMedia.Domain.DTOs;

namespace SocialMedia.Domain.Responses
{
    public class BaseServiceResponse
    {
        public dynamic Data { get; set; }
        public List<MessageDTO> MessageDTOs { get; set; }
        public int StatusCode { get; set; }
        public int Status { get; set; }
    }
}