using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Linq;

namespace DevFramework.Northwind.Business.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;
        public void Add(string key, object data, int cacheTime=60)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Cache.Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool isAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline|RegexOptions.Compiled|RegexOptions.IgnoreCase);
            var keyToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();
        }
    }
}
