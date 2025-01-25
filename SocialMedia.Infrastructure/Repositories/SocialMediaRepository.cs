using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.IRepositories;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Contexts;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly SocialMediaDbContext _context;
        #region Properties
        #endregion

        #region Methods
        public SocialMediaRepository(SocialMediaDbContext context)
        {
            _context = context;
        }

        public async Task CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _context.Posts
                .Include(a => a.Comments)
                .ThenInclude(a => a.Reactions)
                .Include(a => a.Reactions)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Post>> GetUserPosts(string userId)
        {
            return await _context.Posts
                .Include(a => a.Comments)
                .ThenInclude(a => a.Reactions)
                .Include(a => a.Reactions)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task ReactionPost(UserReaction reaction)
        {
            await _context.UserReactions.AddAsync(reaction);
        }

        public async Task CommentPost(UserComment comment)
        {
            await _context.UserComments.AddAsync(comment);
        }
        #endregion
    }
}