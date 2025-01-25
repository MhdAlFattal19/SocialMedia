namespace SocialMedia.Domain.Requests
{
    public class CommentPostRequest : BaseRequest
    {
        public string Comment { get; set; }
        public int PostId { get; set; }
    }
}