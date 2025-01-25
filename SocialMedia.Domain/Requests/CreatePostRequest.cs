namespace SocialMedia.Domain.Requests
{
    public class CreatePostRequest : BaseRequest
    {
        public string Content { get; set; }
        public int PostType { get; set; } 
    }
}
