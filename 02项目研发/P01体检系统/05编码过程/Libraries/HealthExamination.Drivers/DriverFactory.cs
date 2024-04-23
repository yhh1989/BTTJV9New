using System;
using System.IO;
using System.Reflection;
using AppDomain = System.AppDomain;
using Directory = System.IO.Directory;

namespace Sw.Hospital.HealthExamination.Drivers
{
    /// <summary>
    /// 驱动器工厂
    /// </summary>
    public static class DriverFactory
    {
        /// <summary>
        /// 驱动实现程序集
        /// </summary>
        private static readonly Assembly DriverRealizeAssembly;
     

        /// <summary>
        /// 驱动器工厂
        /// </summary>
        static DriverFactory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyDirectory = Path.Combine(baseDirectory, "Drivers");
            if (Directory.Exists(assemblyDirectory))
            {
                var fileNames = Directory.GetFiles(assemblyDirectory, "*.DriverRealizes.dll");
               
                if (fileNames.Length != 0)
                {
                    var fileName = fileNames[0];
                    DriverRealizeAssembly = Assembly.LoadFrom(fileName);
                }
            }
        }

        /// <summary>
        /// 获取驱动器
        /// </summary>
        /// <typeparam name="T">对应接口</typeparam>
        /// <returns>如果存在则为驱动器对象，否则为 T 的默认值</returns>
        public static T GetDriver<T>() where T : IDriver
        {
            if (DriverRealizeAssembly == null)
                return default(T);
            var types = DriverRealizeAssembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (type.GetInterface(typeof(T).Name) == null)
                    continue;
                return (T)Activator.CreateInstance(type);
            }

            return default(T);
        }
    }
}
