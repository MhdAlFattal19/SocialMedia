using Microsoft.AspNetCore.Identity;
using SocialMedia.Domain.Models.CustomModels;

namespace SocialMedia.Domain.Models
{
    public class UserComment : GenericModel
    {
        public string Comment { get; set; }
        public string UserId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public ICollection<UserReaction> Reactions { get; set; } = new List<UserReaction>();

    }
}