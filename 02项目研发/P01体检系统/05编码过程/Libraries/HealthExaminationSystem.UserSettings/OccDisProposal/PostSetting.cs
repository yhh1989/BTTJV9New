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
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    [Obsolete("暂停使用", true)]
    public partial class PostSetting : UserBaseForm
    {
        public PostSetting()
        {
            InitializeComponent();
        }

        private void PostSetting_Load(object sender, EventArgs e)
        {

        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            var gwzt = tegwzt.Text.Trim();
            if (string.IsNullOrWhiteSpace(gwzt))
            {
                dxErrorProvider.SetError(tegwzt, string.Format(Variables.MandatoryTips, "岗位状态"));
                tegwzt.Focus();
                return;
            }

        }
    }
}