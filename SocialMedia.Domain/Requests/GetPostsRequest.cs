namespace SocialMedia.Domain.Requests
{
    public class GetPostsRequest : BaseRequest
    {
        public string? UserId { get; set; }
    }
}