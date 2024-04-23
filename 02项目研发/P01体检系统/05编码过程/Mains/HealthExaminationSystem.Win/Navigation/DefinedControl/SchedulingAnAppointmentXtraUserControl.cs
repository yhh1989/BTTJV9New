using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HealthExaminationSystem.Win.Navigation.DefinedControl
{
    /// <summary>
    /// 预约配置控件
    /// </summary>
    public partial class SchedulingAnAppointmentXtraUserControl : XtraUserControl
    {
        /// <summary>
        /// 初始化 预约配置控件
        /// </summary>
        public SchedulingAnAppointmentXtraUserControl()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// 排期查询 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton排期查询_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "排期查询"));

          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "单位管理"));

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "团体预约"));

        }
    }
}
