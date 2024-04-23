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
    public partial class XtraUserControl主任管理 : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControl主任管理()
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "折扣审批"));

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "预约排期"));

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "体检数据"));

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "投诉记录"));

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "危急值报告"));

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "科室分成"));

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "科研管理"));

        }
    }
}
