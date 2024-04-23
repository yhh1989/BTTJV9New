using System;

namespace HealthExaminationSystem.Win.Navigation
{
    /// <summary>
    /// 导航事件参数
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化 导航事件参数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public NavigationEventArgs(int type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}