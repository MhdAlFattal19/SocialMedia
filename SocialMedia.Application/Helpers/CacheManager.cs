using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.DTOs;
using Newtonsoft.Json;

namespace SocialMedia.Application.Helpers
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void SetCacheItem<T>(string key, T value)
        {
            _cache.Set(key, value);
        }

        public void RemoveItem(string key)
        {
            _cache.Remove(key);
        }

        public T GetCacheItem<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public string GetLocalizedMessage(string key, string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = "en";
            }
            Dictionary<string, string> keyValues = _cache.Get<Dictionary<string, string>>("localized_messages_" + languageCode);
            string result = keyValues[key];
            return result ?? "";
        }

        public string GetLocalizedMessages(string keys, string languageCode, string separator)
        {
            var result = string.Empty;

            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = "en";
            }

            var keyValues = _cache.Get<Dictionary<string, string>>("localized_messages_" + languageCode);

            var keyList = keys.Split(",").ToList();
            var sep = string.Empty;

            foreach (var key in keyList)
            {
                result += sep + (keyValues.TryGetValue(key, out var value) ? value : key);
                sep = separator;
            }

            return $"{result}." ?? "";
        }

        public string GetLocalizedMessages(List<MessageDTO> messageDTOs, string languageCode, string separator)
        {
            var result = string.Empty;

            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = "en";
            }

            var keyValues = _cache.Get<Dictionary<string, string>>("localized_messages_" + languageCode);

            var sep = string.Empty;

            foreach (var item in messageDTOs)
            {
                result += sep + (keyValues.TryGetValue(item.Message, out var value) ? string.Format(value, item.Parameters.ToArray()) : item.Message);
                sep = separator;
            }

            return result ?? "";
        }
    }

}
