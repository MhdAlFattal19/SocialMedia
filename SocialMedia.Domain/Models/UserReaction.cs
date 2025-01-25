using Microsoft.AspNetCore.Identity;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Models.CustomModels;

namespace SocialMedia.Domain.Models
{
    public class UserReaction : GenericModel
    {
        public UserReactionEnum ReactionType { get; set; }
        public string UserId { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }

        public int? CommentId { get; set; }
        public UserComment Comment { get; set; }
    }
}