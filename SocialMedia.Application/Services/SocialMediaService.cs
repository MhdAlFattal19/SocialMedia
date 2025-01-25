using AutoMapper;
using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.IRepositories;
using SocialMedia.Domain.Models;
using SocialMedia.Domain.Requests;
using SocialMedia.Domain.Responses;
using System.Net;

namespace SocialMedia.Application.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ISocialMediaUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheManager;

        public SocialMediaService(ISocialMediaUnitOfWork unitOfWork, IMapper mapper,
            ICacheManager cacheManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// this funcation for create post by user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreatePostResponse> CreatePost(CreatePostRequest request)
        {
            // to do add validation request

            var post = new Post
            {
                PostType = (PostTypeEnum)request.PostType,
                Content = request.Content,
                UserId = request.RequestInfoService.UserId.ToString(),
            };

            await _unitOfWork.SocialMediaRepository.CreatePost(post);

            await _unitOfWork.SaveAsync();

            RemoveCash("posts_cache");

            return new CreatePostResponse
            {
                Status = (int)APIResponseEnum.Success,
                StatusCode = (int)HttpStatusCode.OK,
                MessageDTOs = new List<MessageDTO>
                {
                      new MessageDTO
                      {
                         Message = "Create Post Successfully"
                      }
                }
            };
        }

        /// <summary>
        /// this funcation for get posts for specific user or for un specific user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetPostsResponse> GetPosts(GetPostsRequest request)
        {
            // to do add validation request

            const string cashKey = "posts_cache";

            var result = _cacheManager.GetCacheItem<List<PostDTO>>(cashKey);
            if (result is null || result.Count == 0)
            {
                var posts = !string.IsNullOrEmpty(request.UserId) ?
                await _unitOfWork.SocialMediaRepository.GetUserPosts(request.UserId) :
                await _unitOfWork.SocialMediaRepository.GetPosts();

                result = _mapper.Map<List<PostDTO>>(posts);

                _cacheManager.SetCacheItem(cashKey, result);
            }

            return new GetPostsResponse
            {
                Data = result,
                Status = (int)APIResponseEnum.Success,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        /// <summary>
        /// this funcation for reaction post by user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReactionPostResponse> ReactionPost(ReactionPostRequest request)
        {
            // to do add validation request and for unlike post or user already liked

            var reaction = new UserReaction
            {
                PostId = request.PostId,
                ReactionType = (UserReactionEnum)request.ReactionType,
                UserId = request.RequestInfoService.UserId.ToString(),
            };

            await _unitOfWork.SocialMediaRepository.ReactionPost(reaction);

            await _unitOfWork.SaveAsync();

            RemoveCash("posts_cache");

            return new ReactionPostResponse
            {
                Status = (int)APIResponseEnum.Success,
                StatusCode = (int)HttpStatusCode.OK,
                MessageDTOs = new List<MessageDTO>
                {
                      new MessageDTO
                      {
                         Message = "Reaction Post Successfully"
                      }
                }
            };
        }

        /// <summary>
        /// this funcation for reaction post by user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CommentPostResponse> CommentPost(CommentPostRequest request)
        {
            // to do add validation request

            var comment = new UserComment
            {
                PostId = request.PostId,
                Comment = request.Comment,
                UserId = request.RequestInfoService.UserId.ToString(),
            };

            await _unitOfWork.SocialMediaRepository.CommentPost(comment);

            await _unitOfWork.SaveAsync();

            RemoveCash("posts_cache");

            return new CommentPostResponse
            {
                Status = (int)APIResponseEnum.Success,
                StatusCode = (int)HttpStatusCode.OK,
                MessageDTOs = new List<MessageDTO>
                {
                      new MessageDTO
                      {
                         Message = "Comment Post Successfully"
                      }
                }
            };
        }

        private void RemoveCash(string key)
        {
            _cacheManager.RemoveItem(key);
        }
    }
}