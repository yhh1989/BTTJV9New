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
    public partial class XtraUserControl检后 : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControl检后()
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
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "个人报告"));

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "报告领取"));

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "随访设置"));

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "随访记录"));

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "危急值一览"));

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "危急值报告"));

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "投诉记录"));

        }
    }
}
