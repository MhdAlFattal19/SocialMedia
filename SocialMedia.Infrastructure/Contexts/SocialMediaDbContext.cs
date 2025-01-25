using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Contexts
{
    public class SocialMediaDbContext : DbContext
    {
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserReaction> UserReactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* To Do Add Custom Configurations for entity */

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Reactions)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment Configurations
            modelBuilder.Entity<UserComment>()
                .HasMany(c => c.Reactions)
                .WithOne(r => r.Comment)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}