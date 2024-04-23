using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CusReporReceivet : UserBaseForm
    {
        private readonly ICustomerReportAppService customerReportAppService;
        public CusReporReceivet()
        {
            customerReportAppService = new CustomerReportAppService();
            InitializeComponent();
        }

        private void CusReporReceivet_Load(object sender, EventArgs e)
        {
            //存入状态
            txttate.Properties.DataSource = CabinetHelper.GetLQModels();
            //单位
            conClientName.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            SeachCusCabDto cabDto = new SeachCusCabDto();
            if (txtCusBM.EditValue!=null && txtCusBM.EditValue.ToString() !="")
            {
                cabDto.CustomerBM = txtCusBM.EditValue.ToString();
            }
            if (txtName.EditValue != null && txtName.EditValue.ToString() != "")
            {
                cabDto.CustomerName = txtName.EditValue.ToString();
            }
            if (txtCab.EditValue != null && txtCab.EditValue.ToString() != "")
            {
                cabDto.CabitName = txtCab.EditValue.ToString();
            }
            if (txtMoblie.EditValue != null && txtMoblie.EditValue.ToString() != "")
            {
                cabDto.Mobile = txtMoblie.EditValue.ToString();
            }
            if (txttate.EditValue!=null)
            {
                cabDto.GetState =int.Parse(txttate.EditValue.ToString());
            }
            if (dtLoginStar.EditValue != null && dtLoginEnd.EditValue != null)
            {
                cabDto.StarLoginTime = dtLoginStar.DateTime;
                cabDto.EndLoginTime = dtLoginEnd.DateTime;
            }
            if (dtLQStar.EditValue!= null && dtLQEnd.EditValue!= null)
            {
                cabDto.StarSendTime = dtLQStar.DateTime;
                cabDto.EndSendTime = dtLQEnd.DateTime;
            }
            if (conClientName.EditValue != null)
            {
                cabDto.ClientRegId = (Guid)conClientName.EditValue;
            }
            var result = customerReportAppService.getTjlCabinetlist(cabDto);
            gridControl1.DataSource = result;

        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "concz")
            {
                //var selectIndexes = e.RowHandle;

                var index = e.RowHandle;
                var id = (Guid)gridView1.GetRowCellValue(index, connId);
                var rowdata = gridControl1.Views[0].GetRow(index) as TjlCusCabitCRDto;
                var ckname = (string)gridView1.GetRowCellValue(index, concz);
                if (ckname == "领取")
                {
                    frmLqQR lqQr = new frmLqQR();
                    lqQr.title = "个报领取";
                    List<Guid> list = new List<Guid>();
                    list.Add(id);
                    lqQr.LQlsit = list;
                    List<string> connamelis = new List<string>();
                    connamelis.Add(rowdata.CabitName);
                    lqQr.conNamelis = connamelis;
                    if (lqQr.ShowDialog() == DialogResult.OK && lqQr.dto!=null) 
                    {
                        rowdata.GetState = 2;
                        rowdata.SendTime = lqQr.dto.SendTime;
                        rowdata.Receiver = lqQr.dto.Receiver;
                    }
                    gridView1.RefreshData();
                    gridControl1.Refresh();
                }
            }
        }

        private void butLQ_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                List<Guid> idlist = new List<Guid>();
                List<string> connamelis = new List<string>();
                foreach (var index in selectIndexes)
                {                    
                    var id = (Guid)gridView1.GetRowCellValue(index, connId);
                    var rowdata = gridControl1.Views[0].GetRow(index) as TjlCusCabitCRDto;
                    idlist.Add(id);
                    connamelis.Add(rowdata.CabitName);

                }
                frmLqQR lqQr = new frmLqQR();
                lqQr.title = "个报批量领取";
                lqQr.LQlsit = idlist;
                lqQr.conNamelis = connamelis;
                if (lqQr.ShowDialog() == DialogResult.OK)
                {
                    butSearch.PerformClick();
                }
            }
                
           
        }

        private void txtCab_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CabitSelect cabitSelect = new CabitSelect();
            if (cabitSelect.ShowDialog() == DialogResult.OK)
            {
                txtCab.EditValue = cabitSelect.conName;
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
          
        }
    }
}
