using SocialMedia.Domain.Requests;
using SocialMedia.Domain.Responses;

namespace SocialMedia.Domain.Contracts
{
    public interface ISocialMediaService
    {
        Task<CommentPostResponse> CommentPost(CommentPostRequest request);
        Task<CreatePostResponse> CreatePost(CreatePostRequest request);
        Task<GetPostsResponse> GetPosts(GetPostsRequest request);
        Task<ReactionPostResponse> ReactionPost(ReactionPostRequest request);
    }
}
