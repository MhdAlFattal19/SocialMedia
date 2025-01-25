using Microsoft.AspNetCore.Identity;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Models.CustomModels;

namespace SocialMedia.Domain.Models
{
    public class Post : GenericModel
    {
        public string Content { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string UserId { get; set; }

        public ICollection<UserComment> Comments { get; set; } = new List<UserComment>();
        public ICollection<UserReaction> Reactions { get; set; } = new List<UserReaction>();
    }
}