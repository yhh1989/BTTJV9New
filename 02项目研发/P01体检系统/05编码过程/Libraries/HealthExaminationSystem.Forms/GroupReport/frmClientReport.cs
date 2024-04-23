using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
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
using static Sw.Hospital.HealthExaminationSystem.CustomerReport.PrintPreview;

namespace Sw.Hospital.HealthExaminationSystem.GroupReport
{
    public partial class frmClientReport : UserBaseForm
    {
        private readonly ClientRegAppService clientRegAppService;
        private readonly ICustomerReportAppService customerReportAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly ICustomerAppService _customerAppService;

        public frmClientReport()
        {
            clientRegAppService = new ClientRegAppService();
            customerReportAppService = new CustomerReportAppService();
            _commonAppService = new CommonAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _customerAppService = new CustomerAppService();
            InitializeComponent();
        }

        private void frmClientReport_Load(object sender, EventArgs e)
        {
           
            //隐私报告
            var list1 = new List<EnumModel>();
            list1.Add(new EnumModel { Id = 1, Name = "仅隐私报告" });
            list1.Add(new EnumModel { Id = 2, Name = "团体报告(不含隐私)" });
            list1.Add(new EnumModel { Id = 3, Name = "全部" });
            comboBoxEdit2.Properties.DataSource = list1;
            comboBoxEdit2.EditValue = 3;
            //单位
            conClientName.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            //存入状态
            concatstate.Properties.DataSource = CabinetHelper.GetCRModels();
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = "ReferenceStandard";
            var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            lookUpEdit1.Properties.DataSource = dl;
            lookUpEdit1.Properties.DisplayMember = "Text";
            lookUpEdit1.Properties.ValueMember = "Id";
            //报告模板
            var mb = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.GroupReportSet.ToString() && o.Value== 10);
            if (mb != null && mb.Remarks != "")
            {
                comboBoxEdit1.Properties.Items.Clear();
                var namels = mb.Remarks.Split('|');
                foreach (var str in namels)
                {
                    if (str != null)
                    {
                        if (Variables.ISZYB == "2")
                        {
                            if (str.Contains("职业健康"))
                            {
                                comboBoxEdit1.Properties.Items.Add(str);
                            }
                        }
                        else
                        {
                            comboBoxEdit1.Properties.Items.Add(str);
                        }
                    }
                }

            }
            else
            {
                if (Variables.ISZYB == "2")
                {
                    comboBoxEdit1.Properties.Items.Clear();
                    comboBoxEdit1.Properties.Items.Add("职业健康团报");

                }
            }
            if (comboBoxEdit1.Properties.Items.Count > 0)
            {
                comboBoxEdit1.SelectedIndex = 0;
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchClientRegLQDto dto = new SearchClientRegLQDto();
            if (conClientName.EditValue != null)
            {
                dto.Id = (Guid)conClientName.EditValue;
            }
            if (dateEditStart.EditValue != null && dateEditEnd.EditValue != null)
            {
                dto.StartCheckDate = dateEditStart.DateTime;
                dto.EndCheckDate = dateEditEnd.DateTime;
            }
            if (dtcatStar.EditValue != null && dtcatEnd.EditValue != null)
            {
                dto.StarCusCabitTime = dtcatStar.DateTime;
                dto.EndCusCabitTime = dtcatEnd.DateTime;
            }
            if (txtCab.EditValue != null)
            {
                dto.CusCabitBM = txtCab.EditValue.ToString();

            }
            if (concatstate.EditValue != null)
            {
                dto.CusCabitState = (int)concatstate.EditValue;
            }
           var result= clientRegAppService.getClientRegLQ(dto);
            gridControl1.DataSource = result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能预览报告！");
                //return;
            }
            var selectIndexes = gridViewClientReg.GetSelectedRows();
            var cuslist = new List<Guid>();
            string Star = "";
            string End = "";
            if (selectIndexes.Length != 0)
            {
                //改成支持多单位
                List<Guid> ClientRegID = new List<Guid>();
                for (int row = 0; row < selectIndexes.Length; row++)
                {
                    var nowid = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[row], ConClientRegID);
                    ClientRegID.Add(nowid);
                }
                if (checkCus.Checked == true)
                {
                    
                    //var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    frmClientCusList frmClientCusList = new frmClientCusList();
                    frmClientCusList.clientregID = ClientRegID;
                    if (frmClientCusList.ShowDialog() == DialogResult.OK)
                    {
                        cuslist = frmClientCusList.CusId;
                        if (frmClientCusList.star.HasValue)
                        {
                            Star= frmClientCusList.star.Value.ToString("yyyy-MM-dd");
                            End= frmClientCusList.end.Value.ToString("yyyy-MM-dd");
                        }
                    }
                }
                if (comboBoxEdit1.Text.Contains("健康体检"))
                {
                    int isreport = (int)comboBoxEdit2.EditValue;
                    //var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                   //EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    var printReport = new PrintClientReport();
                    bool isok = false;
                    if (checkZD.Checked == true)
                    {
                        isok = true;
                    }
                    if (comboBoxEdit1.Text.Contains(".grf"))
                    {
                        printReport.Print(true, ClientRegID, "", isok, comboBoxEdit1.Text, cuslist, isreport);
                    }
                    else
                    {
                        printReport.Print(true, ClientRegID, "", isok, "", cuslist, isreport);
                    }
                }
                else if (comboBoxEdit1.Text.Contains("职业"))
                {
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintOccClientReport();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    string Reference = "";
                    if (lookUpEdit1.Text != "")
                    {
                        Reference = lookUpEdit1.Text;
                    }
                    string MB = "";
                    if (comboBoxEdit1.Text.Contains(".grf"))
                    {
                        MB = comboBoxEdit1.Text;
                    }
                    bool isfch = true;
                    if (checkISFC.Checked == false)
                    {
                        isfch = false;
                    }
                    printReport.Print(true, input, null, cuslist, Star, End, Reference, MB, isfch);
                }
                else if (comboBoxEdit1.Text.Contains("团报分析解答")) 
                {
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintClientSumReport();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    printReport.Print( input, true, "","") ;
                }
                else if (comboBoxEdit1.Text.Contains("报告发放核对表"))
                {
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var ClientName = gridViewClientReg.GetRowCellValue(selectIndexes[0], cnClientName).ToString();
                    printList(id,  ClientName,"");
                }

            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能导出报告！");
                //return;
            }
            var cuslist = new List<Guid>();
            string Star = "";
            string End = "";
            var selectIndexes = gridViewClientReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                List<Guid> ClientRegID = new List<Guid>();
                foreach (var row in selectIndexes)
                {
                    var nowid = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[row], ConClientRegID);
                    ClientRegID.Add(nowid);
                }

                if (checkCus.Checked == true)
                    {
                       // var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                        frmClientCusList frmClientCusList = new frmClientCusList();
                        frmClientCusList.clientregID = ClientRegID;
                        if (frmClientCusList.ShowDialog() == DialogResult.OK)
                        {
                            cuslist = frmClientCusList.CusId;
                            if (frmClientCusList.star.HasValue)
                            {
                                Star = frmClientCusList.star.Value.ToString("yyyy-MM-dd");
                                End = frmClientCusList.end.Value.ToString("yyyy-MM-dd");
                            }
                        }
                    }


                    string path = "";
                if (Shell.BrowseForFolder("请选择文件夹！",out path) != DialogResult.OK)
                    return;
                //path = path.Replace(@"\\", @"\");
                string pathold = path;

                var clientname = gridViewClientReg.GetRowCellValue(selectIndexes[0], cnClientName);
                string strnewpath = path + "\\" + clientname;
                if (comboBoxEdit1.Text.Contains("健康体检"))
                {
                    bool isok = false;
                    if (checkZD.Checked == true)
                    {
                        isok = true;
                    }
                    //var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintClientReport();
                    //EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    //改成支持多单位
                  
                    if (comboBoxEdit1.Text.Contains(".grf"))
                    {
                        //printReport.Print(true, input, "", isok, comboBoxEdit1.Text);
                        printReport.Print(false, ClientRegID, strnewpath, isok, comboBoxEdit1.Text, cuslist);
                    }
                    else
                    {
                        printReport.Print(false, ClientRegID, strnewpath, isok,"", cuslist);
                    }

                   
                   
                }
                else if (comboBoxEdit1.Text.Contains("职业"))
                {
                   
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintOccClientReport();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    // printReport.Print(true, input, strnewpath,Star,En);
                    string Reference = "";
                    if (lookUpEdit1.Text != "")
                    {
                        Reference = lookUpEdit1.Text;
                    }
                    string MB = "";
                    if (comboBoxEdit1.Text.Contains(".grf"))
                    {
                        MB = comboBoxEdit1.Text;
                    }
                    bool isfch = true;
                    if (checkISFC.Checked == false)
                    {
                        isfch = false;
                    }
                    printReport.Print(false, input, strnewpath, cuslist, Star, End, Reference, MB, isfch);
                }
                else if (comboBoxEdit1.Text.Contains("团报分析解答"))
                {
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var printReport = new PrintClientSumReport();
                    EntityDto<Guid> input = new EntityDto<Guid> { Id = id };
                    printReport.Print(input, false, "", strnewpath);
                }
                else if (comboBoxEdit1.Text.Contains("报告发放核对表"))
                {
                    var id = (Guid)gridViewClientReg.GetRowCellValue(selectIndexes[0], ConClientRegID);
                    var ClientName = gridViewClientReg.GetRowCellValue(selectIndexes[0], cnClientName).ToString();
                    printList(id, ClientName, strnewpath);
                }
                MessageBox.Show("导出成功！");
            }
            else
            {
                ShowMessageBoxWarning("请先选中一条数据！");
            }
        }

        private void gridViewClientReg_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "conCZCabitState")
            {
                //var selectIndexes = e.RowHandle;

                var index = e.RowHandle;
                var id = (Guid)gridViewClientReg.GetRowCellValue(index, ConClientRegID);
                var rowdata = gridControl1.Views[0].GetRow(index) as ClientRegLQDto;
                var ckname = (string)gridViewClientReg.GetRowCellValue(index, conCZCabitState);
                if (ckname == "存入")
                {
                    CabitSelect cabitSelect = new CabitSelect();
                    if (cabitSelect.ShowDialog() == DialogResult.OK)
                    {


                        AutoLoading(() =>
                        {

                            TjlCusCabitDto dto = new TjlCusCabitDto();
                            dto.ClientRegId = id;
                            dto.CabitName = cabitSelect.conName;
                            dto.GetState = 1;
                            dto.ReportState = 2;
                            customerReportAppService.SaveTjlCabinet(dto);
                            ClientRegUpCatDto catDto = new ClientRegUpCatDto();
                            catDto.Id = id;
                            catDto.CusCabitBM = cabitSelect.conName;
                            catDto.CusCabitState = 1;
                            catDto.CusCabitTime = _commonAppService.GetDateTimeNow().Now;
                            var result = customerReportAppService.UpClientRegUpCat(catDto);
                            rowdata.CusCabitBM = result.CusCabitBM;
                            rowdata.CusCabitState = result.CusCabitState;
                            rowdata.CusCabitTime = result.CusCabitTime;                 
                        });
                        gridViewClientReg.RefreshData();
                        gridControl1.Refresh();

                    }
                }
                else if (ckname == "取消存入")
                {


                    AutoLoading(() =>
                    {
                        TjlCusCabitDto dto = new TjlCusCabitDto();
                        dto.CustomerRegId = id;
                        customerReportAppService.DelTjlCabinet(dto);
                        ClientRegUpCatDto catDto = new ClientRegUpCatDto();
                        catDto.Id = id;
                        catDto.CusCabitBM = "";
                        catDto.CusCabitState = 0;
                        catDto.CusCabitTime = null;
                        var result = customerReportAppService.UpClientRegUpCat(catDto);
                        rowdata.CusCabitBM = result.CusCabitBM;
                        rowdata.CusCabitState = result.CusCabitState;
                        rowdata.CusCabitTime = result.CusCabitTime;                 
                    });

                    gridViewClientReg.RefreshData();
                    gridControl1.Refresh();
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
        private void printList(Guid clientID,string ClientName,string path)
        {
            
       
            if (clientID != null)
            {
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                entityDto.Id = clientID;
                var cuslit = _customerAppService.getClientCusHS(entityDto);
                if (cuslit.Count > 0)
                {
                    reprtMain reprtMain = new reprtMain();
                    reprtMain.Detail = cuslit.ToList();

                    reprtMain.Master = new List<reprtMaster>();
                    reprtMaster master = new reprtMaster();
                    master.ClientName = ClientName;
                    master.AllCount = cuslit.Count();
                    master.AllManCount = cuslit.Where(o => o.Sex == (int)Sex.Man).Count();
                    master.AllWomenCount = cuslit.Where(o => o.Sex == (int)Sex.Woman).Count();
                    master.AllWomenMarrCount = cuslit.Where(o => o.MarriageStatus == (int)MarrySate.Married).Count();
                    master.AllWomenNoMarrCount = cuslit.Where(o => o.MarriageStatus == (int)MarrySate.Unmarried).Count();
                    master.AllWomenUnMarrCount = cuslit.Where(o => o.MarriageStatus == (int)MarrySate.Unstated).Count();
                    master.CheckCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not).Count();
                    master.CheckManCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Man).Count();
                    master.CheckWomenCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Woman).Count();
                    master.CheckWomenMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Married).Count();
                    master.CheckWomenNoMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Unmarried).Count();
                    master.CheckWomenUnMarrCount = cuslit.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.MarriageStatus == (int)MarrySate.Unstated).Count();
                    var nocheckCuslist = cuslit.Where(o => o.CheckSate == (int)PhysicalEState.Not).Select(o => o.Name).ToList();
                    master.NoCount = nocheckCuslist.Count();
                    master.NoCusList = string.Join(" ", nocheckCuslist);
                    reprtMain.Master.Add(master);

                    var gridppUrl = GridppHelper.GetTemplate("报告发放核对表.grf");
                    var report = new GridppReport();
                    report.LoadFromURL(gridppUrl);
                    var reportJsonString = JsonConvert.SerializeObject(reprtMain);
                    report.LoadDataFromXML(reportJsonString);
                    try
                    {
                         if (path != "")
                            {

                                report.ExportDirect(GRExportType.gretXLS, path, false, false);

                            }
                            else
                                report.PrintPreview(true);
                       
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

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
        private void simpleButton4_Click(object sender, EventArgs e)
        {  
        }
    }
}
