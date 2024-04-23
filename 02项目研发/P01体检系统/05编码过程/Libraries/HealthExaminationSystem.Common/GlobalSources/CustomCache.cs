using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Common.GlobalSources
{
    /// <summary>
    /// 自定义缓存
    /// </summary>
    public static class CustomCache
    {
        /// <summary>
        /// 缓存数据字典
        /// </summary>
        private static readonly Dictionary<string, CustomCacheModel> _CustomCacheDictionary = new Dictionary<string, CustomCacheModel>();

        static CustomCache()
        {
            //WaitCallback wcb = new WaitCallback(obj => {
            //    while (true)
            //    {
            //        string[] timeoutKeys = _CustomCacheDictionary.Where(m => m.Value.Timeout > DateTime.Now).Select(m => m.Key).ToArray();
            //        foreach (var key in timeoutKeys)
            //        {
            //            _CustomCacheDictionary.Remove(key);
            //        }
            //        Thread.Sleep(60000); // 1分钟
            //    }
            //});
            //ThreadPool.QueueUserWorkItem(wcb);
        }
        
        /// <summary>
        /// 添加缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="second">x秒后缓存数据超时</param>
        public static void Add(string key, object val, int second = 60)
        {
            _CustomCacheDictionary.Add(key, new CustomCacheModel { Timeout = DateTime.Now.AddSeconds(second), Data = val });
        }
        /// <summary>
        /// 判断缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            if (_CustomCacheDictionary.ContainsKey(key))
            {
                if(DateTime.Now < _CustomCacheDictionary[key].Timeout)
                {
                    return true;
                }
                else
                {
                    Remove(key);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 移除指定缓存数据
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _CustomCacheDictionary.Remove(key);
        }
        /// <summary>
        /// 清空缓存数据
        /// </summary>
        public static void Clear()
        {
            _CustomCacheDictionary.Clear();
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T)_CustomCacheDictionary[key].Data;
        }

        private static readonly object CustomCacheGetFuncLock = new object();
        /// <summary>
        /// 获取缓存数据，泛型委托的方式，延迟缓存数据加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key, Func<T> func, int second = 60)
        {
            T t = default(T);
            if (Exists(key))
            {
                t = CustomCache.Get<T>(key);
            }
            else
            {
                lock (CustomCacheGetFuncLock)
                {
                    if (Exists(key))
                    {
                        t = CustomCache.Get<T>(key);
                    }
                    else
                    {
                        t = func.Invoke();
                        CustomCache.Add(key, t, second);
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 缓存模型
        /// </summary>
        private class CustomCacheModel
        {
            /// <summary>
            /// 超时时间
            /// </summary>
            public DateTime Timeout { get; set; }
            /// <summary>
            /// 缓存数据
            /// </summary>
            public object Data { get; set; }
        }
    }
}
