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
    /// 职业健康导航
    /// </summary>
    public partial class XtraUserControl职业健康 : XtraUserControl
    {
        /// <summary>
        /// 初始化 职业健康导航
        /// </summary>
        public XtraUserControl职业健康()
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "职业健康问诊"));

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "医生站"));

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "职业主检"));


        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "团报"));

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "医生工作量"));

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "危机值报告"));

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "批量总检"));

        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "通知单"));

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "主检工作量"));

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "体检档案"));

        }
    }
}
