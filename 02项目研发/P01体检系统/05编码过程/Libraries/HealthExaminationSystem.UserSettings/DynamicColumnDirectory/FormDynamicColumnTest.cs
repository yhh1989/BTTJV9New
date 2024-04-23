using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列测试
    /// </summary>
    public partial class FormDynamicColumnTest : UserBaseForm
    {
        /// <summary>
        /// 表格视图标识
        /// </summary>
        private const string CurrentGridViewId = "cd91281e6e534e6297be399fb1ecfe59";

        /// <summary>
        /// 初始化“动态列测试”
        /// </summary>
        public FormDynamicColumnTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDynamicColumnTest_Load(object sender, EventArgs e)
        {
            DynamicColumnConfigurationHelper.LoadGridViewDynamicColumnConfiguration(CurrentGridViewId, gridView1);
        }

        /// <summary>
        /// 动态配置列按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonsimpleButtonDTPZL动态配置列_Click(object sender, EventArgs e)
        {
            using (var frm = new FormDynamicColumnConfiguration())
            {
                frm.CurrentGridViewId = CurrentGridViewId;
                frm.CurrentGridView = gridView1;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    DynamicColumnConfigurationHelper.LoadGridViewDynamicColumnConfiguration(CurrentGridViewId, gridView1);
                }
            }
        }
    }
}
