using SocialMedia.Domain.DTOs;

namespace SocialMedia.Domain.Contracts
{
    public interface ICacheManager
    {
        void SetCacheItem<T>(string key, T value);
        T GetCacheItem<T>(string key);
        string GetLocalizedMessage(string key, string languageCode);
        string GetLocalizedMessages(string keys, string languageCode, string separator);
        string GetLocalizedMessages(List<MessageDTO> messageDTOs, string languageCode, string separator);
        void RemoveItem(string key);
    }
}
