using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul;
using Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccHarmFul
{
    public partial class OccHarmFulList : UserBaseForm
    {
        private readonly IOccHarmFulAppService _OccHarmFulAppService;
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly IClientInfoesAppService _IClientInfoesAppService;
        public List<int> indexls = new List<int>();
      
        public OccHarmFulList()
        {
            InitializeComponent();
            _OccHarmFulAppService = new OccHarmFulAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _IClientInfoesAppService = new ClientInfoesAppService();
        }

        private void OccHarmFulList_Load(object sender, EventArgs e)
        {

            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 2020, Name = "2020" });
            lists.Add(new EnumModel { Id = 2019, Name = "2019" });
            lists.Add(new EnumModel { Id = 2018, Name = "2018" });
            lists.Add(new EnumModel { Id = 2017, Name = "2017" });
            comboBoxEdit1.Properties.DataSource = lists;

            //一级分类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.HazardHactors.ToString();
            var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxEdit2.Properties.DataSource = dl;

            ClientInfoesListInput s = new ClientInfoesListInput();
            var result = _IClientInfoesAppService.Query(s);
            comboBoxEdit3.Properties.DataSource = result;

            OutOccFactoryDto show = new OutOccFactoryDto();
            var data = _OccHarmFulAppService.GetOutOccFactories(show);          
            gridControl1.DataSource = data;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OutOccFactoryDto show = new OutOccFactoryDto();
            if (!string.IsNullOrWhiteSpace(comboBoxEdit3.Text.Trim()))
                show.ClientName = comboBoxEdit3.Text.Trim();
            if (dateEditStartTime.EditValue != null)
                show.StartCheckDate = dateEditStartTime.DateTime;

            if (dateEditEndTime.EditValue != null)
                show.EndCheckDate = dateEditEndTime.DateTime;

            if (show.StartCheckDate > show.EndCheckDate)
            {
                dxErrorProvider.SetError(dateEditStartTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(dateEditEndTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }
            if (comboBoxEdit1.EditValue != null)
                show.YearTime = comboBoxEdit1.Text.Trim();

            if (comboBoxEdit2.EditValue != null)
            {
                show.ParentId = (Guid)comboBoxEdit2.EditValue;
            }

            var data = _OccHarmFulAppService.GetOutOccFactories(show);
            gridControl1.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<OutOccFactoryDto>;
            if (result != null && result.Count > 0)
            {
                var frm = new OccHarmFulStatic(result);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "有害因素统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var ds = gridControl1.DataSource;
            gridControl1.DataSource = ds;
            gridControl1.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
    }
}
