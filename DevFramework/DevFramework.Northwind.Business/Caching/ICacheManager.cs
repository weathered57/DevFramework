using System;
using System.Collections.Generic;
using System.Text;

namespace DevFramework.Northwind.Business.Caching
{
   public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data, int cacheTime);
        bool isAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}
