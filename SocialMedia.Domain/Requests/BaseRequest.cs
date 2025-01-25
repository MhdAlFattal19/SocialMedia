using SocialMedia.Domain.Contracts;

namespace SocialMedia.Domain.Requests
{
    public class BaseRequest
    {
        public IRequestInfoService? RequestInfoService { get; set; }
    }
}
