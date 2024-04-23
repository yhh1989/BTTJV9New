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
    public partial class XtraUserControl前台管理 : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControl前台管理()
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
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "体检登记"));

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "批量登记"));

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "采样工作站"));

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "交表"));

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "体检档案"));

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "日志管理"));

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "更新缓存"));

        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            NavigationEvents.OnNavigationButtonClick(sender, new NavigationEventArgs(1, "重置状态"));


        }
    }
}
