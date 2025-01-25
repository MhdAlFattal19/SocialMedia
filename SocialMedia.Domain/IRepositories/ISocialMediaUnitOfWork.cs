namespace SocialMedia.Domain.IRepositories
{
    public interface ISocialMediaUnitOfWork
    {
        ISocialMediaRepository SocialMediaRepository { get; }
        Task SaveAsync();

    }
}
