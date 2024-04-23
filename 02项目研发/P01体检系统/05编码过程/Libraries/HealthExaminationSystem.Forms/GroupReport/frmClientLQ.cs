using Abp.Application.Services.Dto;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.CustomerReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.GroupReport
{
    public partial class frmClientLQ : UserBaseForm
    {
        private readonly ClientRegAppService clientRegAppService;
        private readonly ICustomerReportAppService customerReportAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly ICustomerAppService _customerAppService;
        public frmClientLQ()
        {

            clientRegAppService = new ClientRegAppService();
            customerReportAppService = new CustomerReportAppService();
            _commonAppService = new CommonAppService();
            _customerAppService = new CustomerAppService();
            InitializeComponent();
        }

        private void frmClientLQ_Load(object sender, EventArgs e)
        {
            //单位
            conClientName.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void txtCab_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CabitSelect cabitSelect = new CabitSelect();
            if (cabitSelect.ShowDialog() == DialogResult.OK)
            {
                txtCab.EditValue = cabitSelect.conName;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchClientRegLQDto dto = new SearchClientRegLQDto();
            if (conClientName.EditValue != null)
            {
                dto.Id = (Guid)conClientName.EditValue;
            }
            if (txtCab.EditValue != null)
            {
                dto.CusCabitBM = txtCab.EditValue.ToString();
            }
            var result = customerReportAppService.getClientCabinetlist(dto);
            gridControl1.DataSource = result;

        }

        private void butLQ_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridLQ.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                List<Guid> idlist = new List<Guid>();
                List<string> connamelis = new List<string>();
                foreach (var index in selectIndexes)
                {
                    var id = (Guid)gridLQ.GetRowCellValue(index, connId);
                    var rowdata = gridControl1.Views[0].GetRow(index) as ClientCabitCRDto;
                    idlist.Add(id);
                    connamelis.Add(rowdata.CabitName);

                }
                frmLqQR lqQr = new frmLqQR();
                lqQr.title = "团报批量领取";
                lqQr.LQlsit = idlist;
                lqQr.conNamelis = connamelis;
                if (lqQr.ShowDialog() == DialogResult.OK)
                {
                    simpleButton1.PerformClick();
                }
            }
        }

        private void gridView3_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "concz")
            {
                //var selectIndexes = e.RowHandle;

                var index = e.RowHandle;
                var id = (Guid)gridLQ.GetRowCellValue(index, connId);
                var rowdata = gridControl1.Views[0].GetRow(index) as ClientCabitCRDto;
                var ckname = (string)gridLQ.GetRowCellValue(index, concz);
                if (ckname == "领取")
                {
                    frmLqQR lqQr = new frmLqQR();
                    lqQr.title = "团报领取";
                    List<Guid> list = new List<Guid>();
                    list.Add(id);
                    lqQr.LQlsit = list;
                    List<string> connamelis = new List<string>();
                    connamelis.Add(rowdata.CabitName);
                    lqQr.conNamelis = connamelis;
                    if (lqQr.ShowDialog() == DialogResult.OK)
                    {
                        rowdata.GetState = 2;
                        rowdata.SendTime = lqQr.dto.SendTime;
                        rowdata.Receiver = lqQr.dto.Receiver;
                    }
                    gridLQ.RefreshData();
                    gridControl1.Refresh();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            printList();
        }

        private void printList()
        {
            var clientID = gridLQ.GetFocusedRowCellValue(connId) as Guid?;
            if (clientID != null)
            {
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                var cuslit = _customerAppService.getClientCusHS(entityDto);
                if (cuslit.Count > 0)
                {
                    reprtMain reprtMain = new reprtMain();
                    reprtMain.Detail = cuslit;

                    reprtMain.Master = new List<reprtMaster>();
                    reprtMaster master = new reprtMaster();
                    master.ClientName = gridLQ.GetFocusedRowCellValue(gridColumn7).ToString();
                    master.AllCount = cuslit.Count();
                    master.AllManCount = cuslit.Where(o => o.Sex == (int)Sex.Man).Count();
                    master.AllWomenCount = cuslit.Where(o => o.Sex == (int)Sex.Woman).Count();
                    master.AllWomenMarrCount = cuslit.Where(o=>o.MarriageStatus== (int)MarrySate.Married).Count();
                    master.AllWomenNoMarrCount = cuslit.Where(o => o.MarriageStatus == (int)MarrySate.Unmarried).Count();
                    master.AllWomenUnMarrCount = cuslit.Where(o => o.MarriageStatus == (int)MarrySate.Unstated).Count();
                    master.CheckCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not).Count();
                    master.CheckManCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Man).Count();
                    master.CheckWomenCount= cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Woman).Count();
                    master.CheckWomenMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Married).Count();
                    master.CheckWomenNoMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Unmarried).Count();
                    master.CheckWomenUnMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Unstated).Count();
                    var nocheckCuslist = cuslit.Where(o => o.CheckSate == (int)PhysicalEState.Not).Select(o => o.Name).ToList();
                    master.NoCusList = string.Join(" ", nocheckCuslist);


                    var gridppUrl = GridppHelper.GetTemplate("报告发放核对表.grf");
                    var report = new GridppReport();
                    report.LoadFromURL(gridppUrl);
                    var reportJsonString = JsonConvert.SerializeObject(reprtMain);
                    report.LoadDataFromXML(reportJsonString);
                    report.Print(false);
                }
            }
        }
        private class reprtMain
        {
            public List<reprtMaster> Master { get; set; }

            /// <summary>
            /// 明细网格
            /// </summary>
            public List<ReportHSCusDto> Detail { get; set; }
        }
        private class reprtMaster
        {
            /// <summary>
            /// 单位名称
            /// </summary>
           
            public virtual string ClientName { get; set; }
            /// <summary>
            /// 总人数
            /// </summary>

            public virtual int? AllCount { get; set; }
            /// <summary>
            /// 未检人数
            /// </summary>

            public virtual int? NoCount { get; set; }

            /// <summary>
            /// 总男性人数
            /// </summary>
            public virtual int? AllManCount { get; set; }

            /// <summary>
            /// 总女性人数
            /// </summary>
            public virtual int? AllWomenCount { get; set; }


            /// <summary>
            /// 总女性已婚人数
            /// </summary>
            public virtual int? AllWomenMarrCount { get; set; }

            /// <summary>
            /// 总女性未婚人数
            /// </summary>
            public virtual int? AllWomenNoMarrCount { get; set; }

            /// <summary>
            /// 总女性未知人数
            /// </summary>
            public virtual int? AllWomenUnMarrCount { get; set; }

            /// <summary>
            /// 已检人数
            /// </summary>

            public virtual int? CheckCount { get; set; }

            /// <summary>
            /// 已检男性人数
            /// </summary>
            public virtual int? CheckManCount { get; set; }

            /// <summary>
            /// 已检女性人数
            /// </summary>
            public virtual int? CheckWomenCount { get; set; }


            /// <summary>
            /// 已检女性已婚人数
            /// </summary>
            public virtual int? CheckWomenMarrCount { get; set; }

            /// <summary>
            /// 已检女性未婚人数
            /// </summary>
            public virtual int? CheckWomenNoMarrCount { get; set; }

            /// <summary>
            /// 已检女性未知人数
            /// </summary>
            public virtual int? CheckWomenUnMarrCount { get; set; }



            /// <summary>
            /// 备注1
            /// </summary>

            public virtual string remark1 { get; set; }
            /// <summary>
            /// 备注1
            /// </summary>

            public virtual string remark2 { get; set; }

            /// <summary>
            /// 备注3
            /// </summary>

            public virtual string remark3 { get; set; }



            /// <summary>
            /// 人员列表
            /// </summary>

            public virtual string NoCusList { get; set; }
        }
        }
}
