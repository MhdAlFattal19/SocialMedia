using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Contexts
{
    public class SocialMediaIdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public SocialMediaIdentityDbContext(DbContextOptions<SocialMediaIdentityDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}