using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class BallCheckStatic : UserBaseForm
    {
        private readonly IChargeAppService _chargeAppService;
        public CommonAppService CommonAppSrv=new CommonAppService();
        public BallCheckStatic()
        {
            _chargeAppService = new ChargeAppService();
            InitializeComponent();
        }

        private void BallCheck_Load(object sender, EventArgs e)
        {
            lookClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto().ToList();
            searchLookUpEdit1.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto().ToList();
            lookClient3.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            

            gridView7.Columns[gridColumn49.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView7.Columns[gridColumn49.FieldName].DisplayFormat.Format = new CustomFormatter(FZStateHelper.FZStateType);

            gridView7.Columns[gridColumn53.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView7.Columns[gridColumn53.FieldName].DisplayFormat.Format = new CustomFormatter(JSStateHelper.FZStateType);
            
            comFZState.Properties.DataSource = FZStateHelper.GetFZStateModels();

            comJSState.Properties.DataSource = JSStateHelper.GetJSStateModels();
            txtReceivable.Enabled = true;
            txtReceivable.ReadOnly = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BallCheckDto i = new BallCheckDto();
            if (dateEdit1.EditValue != null)
            {
                i.StartDate = (DateTime)dateEdit1.EditValue;
            }
            if (dateEdit2.EditValue != null)
            {
                var  end= (DateTime)dateEdit2.EditValue;

                i.EndDate = end.AddDays(1);
            }
            if (searchLookUpEdit1.EditValue != null && searchLookUpEdit1.EditValue != "")
            {
                i.ClientRegId = (Guid)searchLookUpEdit1.EditValue;
            }
         
            var result = _chargeAppService.GetBallCheck(i);
            if (decimal.Parse( txtReceivable.EditValue.ToString())>0)
            { 
                var MaxMoney = decimal.Parse(txtReceivable.EditValue.ToString());
                result = result.Where(p=>p.SumPrice>= decimal.Parse(txtReceivable.EditValue.ToString())).ToList();
            }
            gridControl1.DataSource = result;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "团体统计1";
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

        private void button2_Click(object sender, EventArgs e)
        {
            SearchClientGroupDto input = new SearchClientGroupDto();
            if (dateEdit3.EditValue != null)
            {
                input.StarDate = DateTime.Parse(dateEdit3.DateTime.ToShortDateString());
            }
            if (dateEdit4.EditValue != null)
            {
                input.EndDate = DateTime.Parse(dateEdit4.DateTime.AddDays(1).ToShortDateString());
            }
            if (lookClient.EditValue != null && lookClient.EditValue!="")
            {
                input.ClientRegId = (Guid)lookClient.EditValue;
            }
           var ClientGroup= _chargeAppService.clientGroupStatisDtos(input);
            gridControl2.DataSource = ClientGroup;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "团体报表2";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var ds = gridControl2.DataSource;
            gridControl2.DataSource = ds;
            gridControl2.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ThreeBallCheckDto show = new ThreeBallCheckDto();
            if (Startdate.EditValue != null)
            {
                show.StartDateTime = (DateTime)Startdate.EditValue;
            }
            if (Enddate.EditValue != null)
            {
                var endTime = (DateTime)Enddate.EditValue;

                show.EndDateTime = endTime.AddDays(1);
            }
            if (lookClient3.EditValue != null && lookClient3.EditValue != "")
            {
                show.ClientRegId = (Guid)lookClient3.EditValue;
            }
            if (!string.IsNullOrWhiteSpace(comFZState.EditValue?.ToString()))
            {
                show.FZState = (int)comFZState.EditValue;
            }
            if (!string.IsNullOrWhiteSpace(comJSState.EditValue?.ToString()))
            {
                
                show.JSState = (int)comJSState.EditValue;
            }
            var result = _chargeAppService.GetThreeBallChecks(show);

            ThreeBallCheckDto threeBallCheckDto = new ThreeBallCheckDto();
            threeBallCheckDto.ClientName = "合计：" + result.GroupBy(o => o.ClientRegId).Count() +"条"; 
            threeBallCheckDto.ClientMoney = result.GroupBy(o => o.ClientRegId).Select(o => o.FirstOrDefault().ClientMoney).Sum();
            threeBallCheckDto.nvoiceMoney = result.Sum(o=>o.nvoiceMoney);
            threeBallCheckDto.AllnvoiceMoney= result.GroupBy(o => o.ClientRegId).Select(o => o.FirstOrDefault().AllnvoiceMoney).Sum();
           
            result.Add(threeBallCheckDto);
            gridControl3.DataSource = result;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "团体统计3";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;        
         
            gridControl3.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }

        private void gridView7_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string str1 = gridView7.GetRowCellValue(e.RowHandle1, "ClientName")?.ToString();
            string str2 = gridView7.GetRowCellValue(e.RowHandle2, "ClientName")?.ToString();

            if (str1 != str2)
            {

                e.Handled = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            List<ThreeBallCheckDto> result = gridControl3.DataSource as List<ThreeBallCheckDto>;
            var clientregIds = result.Select(p=> p.ClientRegId).Distinct().ToList();
            searchIDListDto searchIDListDto = new searchIDListDto();
            searchIDListDto.Ids = clientregIds;
            _chargeAppService.UPClientJSState(searchIDListDto);
            //日志
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            createOpLogDto.LogBM = "核算";
            createOpLogDto.LogName = "核算";
            var ClientNames = result.Select(p => p.ClientName).Distinct().ToList();

            createOpLogDto.LogText = "单位核算";

            createOpLogDto.LogDetail = string.Join(",", ClientNames); ;
            createOpLogDto.LogType = (int)LogsTypes.ChargId;
            CommonAppSrv.SaveOpLog(createOpLogDto);
            MessageBox.Show("更新成功");

        }
    }
}
