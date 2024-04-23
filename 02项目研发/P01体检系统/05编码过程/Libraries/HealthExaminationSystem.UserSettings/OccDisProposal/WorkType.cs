using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraEditors.Controls;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    [Obsolete("暂停使用", true)]
    public partial class WorkType : UserBaseForm
    {
        public WorkType()
        {
            InitializeComponent();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndustrySetting_Load(object sender, EventArgs e)
        {
            //获取工种值（枚举）
            var list = WorkTypeHelper.GetList();
            //绑定工种单选控件值
            foreach (var item in list)
            {
                RadioGroupItem radio = new RadioGroupItem();
                radio.Description = item.Display;
                radio.Value = item.Id;
                rgGZ.Properties.Items.Add(radio);
            }
            //绑定状态下拉框值
            lueLB.Properties.DataSource = list;
        }

        private void sbQuery_Click(object sender, EventArgs e)
        {

        }
    }
}