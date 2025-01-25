namespace SocialMedia.Domain.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ReactionDTO> Reactions { get; set; } = new List<ReactionDTO>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
