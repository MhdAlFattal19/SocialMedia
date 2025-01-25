namespace SocialMedia.Domain.Requests
{
    public class ReactionPostRequest : BaseRequest
    {
        public int PostId { get; set; }
        public int ReactionType { get; set; }
    }
}