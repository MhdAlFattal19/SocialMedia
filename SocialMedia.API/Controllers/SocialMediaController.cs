using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.Requests;

namespace SocialMedia.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly IRequestInfoService _requestInfoService;
        private readonly ILogger<SocialMediaController> _logger;
        public SocialMediaController(ISocialMediaService socialMediaService, IRequestInfoService requestInfoService,
            ILogger<SocialMediaController> logger)
        {
            _socialMediaService = socialMediaService;
            _requestInfoService = requestInfoService;
            _logger = logger;
        }

        [HttpPost(nameof(CreatePost))]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(CreatePost)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            request.RequestInfoService = _requestInfoService;

            var response = await _socialMediaService.CreatePost(request);

            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(CreatePost)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet(nameof(GetPosts))]
        public async Task<IActionResult> GetPosts(GetPostsRequest request)
        {
            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(GetPosts)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            request.RequestInfoService = _requestInfoService;

            var response = await _socialMediaService.GetPosts(request);

            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(GetPosts)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(ReactionPost))]
        public async Task<IActionResult> ReactionPost(ReactionPostRequest request)
        {
            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(ReactionPost)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            request.RequestInfoService = _requestInfoService;

            var response = await _socialMediaService.ReactionPost(request);

            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(ReactionPost)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(CommentPost))]
        public async Task<IActionResult> CommentPost(CommentPostRequest request)
        {
            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(CommentPost)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            request.RequestInfoService = _requestInfoService;

            var response = await _socialMediaService.CommentPost(request);

            _logger.LogInformation($"{nameof(SocialMediaController)} - {nameof(CommentPost)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

    }
}