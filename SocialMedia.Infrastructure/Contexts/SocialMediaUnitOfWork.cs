using SocialMedia.Domain.IRepositories;
using SocialMedia.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Contexts
{
    public class SocialMediaUnitOfWork : ISocialMediaUnitOfWork
    {
        private readonly SocialMediaDbContext _context;

        public SocialMediaUnitOfWork(SocialMediaDbContext context)
        {
            _context = context;
        }

        public ISocialMediaRepository SocialMediaRepository
        {
            get
            {
                return new SocialMediaRepository(_context);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
