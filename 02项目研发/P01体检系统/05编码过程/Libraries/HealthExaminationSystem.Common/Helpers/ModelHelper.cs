using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ModelHelper
    {
        public static T CustomMapTo<T>(T src, T tag = null)
            where T : class, new()
        {
            if (src == null) return null;
            if (tag == null) tag = new T();
            Type type = typeof(T);
            foreach (var pi in type.GetProperties())
            {
                if (pi.CanWrite)
                    pi.SetValue(tag, pi.GetValue(src, null), null);
            }
            return tag;
        }

        public static T2 CustomMapTo2<T1, T2>(T1 src, T2 tag = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (src == null) return null;
            if (tag == null) tag = new T2();
            Type type1 = typeof(T1);
            Type type2 = typeof(T2);
            var pis = type1.GetProperties();
            if (pis.Length >= type2.GetProperties().Length)
            {
                foreach (var pi1 in pis)
                {
                    var pi2 = type2.GetProperty(pi1.Name);
                    if (pi2 != null) pi2.SetValue(tag, pi1.GetValue(src, null), null);
                }
            }
            else
            {
                pis = type2.GetProperties();
                foreach (var pi2 in pis)
                {
                    var pi1 = type1.GetProperty(pi2.Name);
                    if (pi1 != null) pi2.SetValue(tag, pi1.GetValue(src, null), null);
                }
            }
            return tag;
        }

        /// <summary>
        /// 通过序列化进行深度克隆，需要标注序列化特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DeepCloneBySerialization<T>(T t)
        {
            using (var sourceStream = new System.IO.MemoryStream())
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(sourceStream, t);
                using (var targetStream = new System.IO.MemoryStream())
                {
                    sourceStream.CopyTo(targetStream);
                    return (T)bf.Deserialize(targetStream);
                }
            }
        }

        /// <summary>
        /// 通过Json进行深度克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DeepCloneByJson<T>(T t)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(t));
        }

    }
}
