
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccCusInfoOut;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut;
using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.OccCusInfoOut
{
    public partial class frmOccInfoOut : UserBaseForm
    {
        private IOccCusInfoOutAppService occDayStaticAppService =new OccCusInfoOutAppService();
   
        public frmOccInfoOut()
        {
            InitializeComponent();
        }

        private void frmOccInfoOut_Load(object sender, EventArgs e)
        {
            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("职业健康档案", true, "职业健康档案", gridControl1, gridControl2, gridControl3
                , gridControl4
                , gridControl5
                , gridControl6);
        }
        #region  NPOI大数据量多个sheet导出




        #endregion

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InOccSearchDto inOccSearchDto = new InOccSearchDto();
            if (!string.IsNullOrWhiteSpace(dateEditStar.EditValue?.ToString()))
            {
                inOccSearchDto.Stardt = DateTime.Parse(dateEditStar.DateTime.ToShortDateString());
            }
            if (!string.IsNullOrWhiteSpace(dateEditEnd.EditValue?.ToString()))
            {
                inOccSearchDto.Enddt = DateTime.Parse(dateEditEnd.DateTime.AddDays(1).ToShortDateString());
            }           
            if (!string.IsNullOrWhiteSpace(sleDW.EditValue?.ToString()))
            {
                inOccSearchDto.ClientRegId = (Guid)sleDW.EditValue;
            }

            var cuslist = occDayStaticAppService.getOccCusInfoDto(inOccSearchDto);
            gridControl1.DataSource = cuslist.OutOccCusInfos;
            gridControl2.DataSource = cuslist.OutOccClientInfos;
            gridControl3.DataSource = cuslist.OutOccRiskS;
            gridControl4.DataSource = cuslist.OutOccRiskSTimes;
            gridControl5.DataSource = cuslist.OutOccCusItems;
            gridControl6.DataSource = cuslist.OutOccCusSums;
        }
    }
}
