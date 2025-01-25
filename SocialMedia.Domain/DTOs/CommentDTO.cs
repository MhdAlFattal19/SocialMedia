namespace SocialMedia.Domain.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public List<ReactionDTO> Reactions { get; set; } = new List<ReactionDTO>();
    }
}
