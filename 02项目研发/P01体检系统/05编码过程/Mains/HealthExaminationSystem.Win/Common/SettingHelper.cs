using System.Configuration;
using System.Reflection;

namespace HealthExaminationSystem.Win.Common
{
    /// <summary>
    /// 设置帮助器
    /// </summary>
    internal class SettingHelper
    {
        /// <summary>
        /// 升级设置
        /// </summary>
        internal static void UpgradeSetting()
        {
            var previousVersion = Properties.Settings.Default.GetPreviousVersion(nameof(Properties.Settings.Default.CurrentVersion));
            if (previousVersion != null && !previousVersion.Equals("0.0.0.0"))
            {
                var currentVersion = Properties.Settings.Default.CurrentVersion;
                if (currentVersion.Equals("0.0.0.0"))
                {
                    Properties.Settings.Default.Upgrade();
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    Properties.Settings.Default.CurrentVersion = version;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                if (Properties.Settings.Default.CurrentVersion.Equals("0.0.0.0"))
                {
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    Properties.Settings.Default.CurrentVersion = version;
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}