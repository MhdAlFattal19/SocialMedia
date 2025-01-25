using SocialMedia.Domain.Models;

namespace SocialMedia.Domain.IRepositories
{
    public interface ISocialMediaRepository
    {
        Task CommentPost(UserComment comment);
        Task CreatePost(Post post);
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetUserPosts(string userId);
        Task ReactionPost(UserReaction reaction);
    }
}
