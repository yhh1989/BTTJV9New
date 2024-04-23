using System;

namespace HealthExaminationSystem.Win.Navigation
{
    /// <summary>
    /// 导航事件
    /// </summary>
    public static class NavigationEvents
    {
        /// <summary>
        /// 导航按钮点击事件
        /// </summary>
        public static event EventHandler<NavigationEventArgs> NavigationButtonClick;

        /// <summary>
        /// 触发导航按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnNavigationButtonClick(object sender, NavigationEventArgs e)
        {
            NavigationButtonClick?.Invoke(sender, e);
        }
    }
}