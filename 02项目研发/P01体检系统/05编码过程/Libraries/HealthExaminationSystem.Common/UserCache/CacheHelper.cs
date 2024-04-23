using System;
using System.Runtime.Caching;
using static System.Runtime.Caching.MemoryCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.UserCache
{
    /// <summary>
    /// 缓存帮助
    /// </summary>
    public static class CacheHelper
    {
        private static readonly object Locker = new object();
        
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存标识</param>
        /// <param name="cachePopulate">缓存数据区域</param>
        /// <returns></returns>
        public static T GetCacheItem<T>(string key, Func<T> cachePopulate)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(@"Invalid cache key", nameof(key));
            }

            if (cachePopulate == null)
            {
                throw new ArgumentNullException(nameof(cachePopulate));
            }

            if (Default[key] == null)
            {
                lock (Locker)
                {
                    if (Default[key] == null)
                    {
                        var cacheItem = new CacheItem(key, cachePopulate.Invoke());
                        var cacheItemPolicy = new CacheItemPolicy();
                        Default.Set(cacheItem, cacheItemPolicy);
                    }
                }
            }

            return (T)Default[key];
        }

        /// <summary>
        /// 更新缓存项
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存标识</param>
        /// <param name="cachePopulate">缓存数据区域</param>
        /// <returns></returns>
        public static T UpdateCacheItem<T>(string key, Func<T> cachePopulate)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid cache key", nameof(key));
            }

            if (cachePopulate == null)
            {
                throw new ArgumentNullException(nameof(cachePopulate));
            }

            lock (Locker)
            {
                var cacheItem = new CacheItem(key, cachePopulate.Invoke());
                var cacheItemPolicy = new CacheItemPolicy();
                Default.Set(cacheItem, cacheItemPolicy);
            }

            return (T)Default[key];
        }
    }
}