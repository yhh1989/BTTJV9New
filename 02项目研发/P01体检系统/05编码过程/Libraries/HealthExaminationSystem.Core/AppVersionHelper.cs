using System;
using System.Diagnostics;
using System.IO;
using Abp.Reflection.Extensions;

namespace Sw.Hospital.HealthExaminationSystem.Core
{
    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// Gets current version of the application.
        /// It's also shown in the web page.
        /// </summary>
        public static string Version =>
            FileVersionInfo.GetVersionInfo(typeof(AppVersionHelper).GetAssembly().Location).FileVersion;
        //public const string Version = "3.2.0.0";

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate =>
            new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime;
    }
}