using Abp.Application.Services.Dto;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
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

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class frmApplivation : UserBaseForm
    {
        public Guid GuidClientRegID;
        public string gtClientName = "";
        public ClientRegDto currClientRet = new ClientRegDto();
        ICustomerAppService _customerSvr = new CustomerAppService();
        public UpClientMZDto UpClientMZDto = new UpClientMZDto();
        IClientRegAppService clientRegAppService = new ClientRegAppService();

        private IChargeAppService chargeAppServicechargeAppService = new ChargeAppService();

        public frmApplivation()
        {
            InitializeComponent();
        }

        private void frmApplivation_Load(object sender, EventArgs e)
        {
            refresh();
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            #region HIS接口
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                //if (HISName == "北仑" || HISName == "南京飓风")
                //{
                simpleButton1.Visible = false;
                dxErrorProvider.ClearErrors();
                if (string.IsNullOrWhiteSpace(txtpayMoney.Text))
                {
                    XtraMessageBox.Show("请输入金额！");
                    return;
                }
                if (decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥')) <= 0)
                {
                    XtraMessageBox.Show("收费金额不能小于0！");
                    return;
                }
                //南京飓风接口挂号      
                string mzh = "";
                if (HISName == "南京飓风" || HISName == "世轩" )
                {
                    //7|姓名|性别|年龄|证件类型|证件编码|手机电话|地址|科室编码|操作员
                    //姓名：单位名称，性别：9  年龄：0 ，身份证号：传空
                    EntityDto<Guid> entityDto = new EntityDto<Guid>();
                    entityDto.Id = GuidClientRegID;
                    UpClientMZDto = clientRegAppService.getClientMZ(entityDto);
                    mzh = UpClientMZDto.Remark;
                    if (string.IsNullOrEmpty(UpClientMZDto.Remark))
                    {
                        string instr = "7|" + gtClientName + "|9|0|单位||||1071|" + CurrentUser.EmployeeNum;
                        var outstr = _customerSvr.SaveHisInfo(new InCarNumDto { CardNum = instr, HISName = HISName });
                        if (string.IsNullOrEmpty(outstr.CardNum))
                        {
                            MessageBox.Show("HIS挂号失败！，请重新登记！");
                            //return;
                        }
                        else
                        {
                            //|门诊号|患者ID|
                            var sp = outstr.CardNum.Split('|');
                            UpClientMZDto.ClientRegBM = sp[2];
                            UpClientMZDto.Remark = sp[1];
                            mzh = UpClientMZDto.Remark;
                            clientRegAppService.UpClientMZ(UpClientMZDto);
                        }
                    }
                }
                TJSQDto input = new TJSQDto();
                input.DWMC = gtClientName;
                input.HISName = HISName;
                input.UserID = Variables.User.Id;
                input.FYZK = decimal.Parse(txtpayMoney.EditValue.ToString().Trim('¥'));
                if (!string.IsNullOrEmpty(txtFP.Text.Trim()))
                {
                    input.FPName = txtFP.Text.Trim();

                }
                input.Remark = textRemark.Text.Trim();
                input.ClientRegId = GuidClientRegID;
                 
                var appliy = _customerSvr.InsertSFCharg(input);
                if (appliy != null)
                {
                    string AppliyNum = appliy.ApplicationNum;
                    if (checkEdit1.Checked == true)
                    {
                        printList(AppliyNum, mzh);

                    }
                    else
                    {
                        MessageBox.Show("申请生成成功，申请号为：" + AppliyNum);
                    }
                    refresh();
                    // appliy.CreatTime
                }
                //}
            }
            #endregion
        }
        private void refresh()
        {

            TJSQDto input = new TJSQDto();
            input.DWMC = gtClientName;
            input.ClientRegId = GuidClientRegID;
            var appls = _customerSvr.getapplication(input);
            gridInvo.DataSource = appls;
        }
        private void printList(string AppliyNum,string mzh)
        {
            var reportJson = new ReportJson();
            reportJson.Master = new List<Master>();
            var master = new Master();
            master.CompanyName = gtClientName;
            master.PayMoney = txtpayMoney.EditValue.ToString().Trim('¥');
            master.UserName = CurrentUser.Name;
            master.AppliyNum = AppliyNum;
            master.mzh = mzh;
            master.FPName = txtFP.Text;
            master.remark = textRemark.Text;
            reportJson.Master.Add(master);
            var gridppUrl = GridppHelper.GetTemplate("申请单.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);
            report.Print(false);
        }

        /// <summary>
        /// 报表
        /// </summary>
        public class ReportJson
        {
            /// <summary>
            /// 参数
            /// </summary>
            public List<Master> Master { get; set; }

            /// <summary>
            /// 明细网格
            /// </summary>
            public List<Detail> Detail { get; set; }
        }

        /// <summary>
        /// 报表参数
        /// </summary>
        public class Master
        {
            /// <summary>
            /// 申请单号
            /// </summary>
            public string AppliyNum { get; set; }
            /// <summary>
            /// 单位名称
            /// </summary>
            public string CompanyName { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public string PayMoney { get; set; }
            /// <summary>
            /// 打印时间
            /// </summary>
            public string PrintTime { get; set; }
            /// <summary>
            /// 打印者
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 发票抬头
            /// </summary>
            public string FPName { get; set; }


            /// <summary>
            /// 门诊号
            /// </summary>
            public string mzh { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }


        }

        /// <summary>
        /// 明细网格
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 打勾
            /// </summary>
            public string Tick { get; set; }

            /// <summary>
            /// 科室名称
            /// </summary>
            public string DeparmentName { get; set; }
            /// <summary>
            /// 检验类型
            /// </summary>
            public string Colour { get; set; }
            /// <summary>
            /// 检验类型
            /// </summary>
            public string TestType { get; set; }

            /// <summary>
            /// 组合名称
            /// </summary>
            public string ItemGroupName { get; set; }

            /// <summary>
            /// 科室地址（提示信息）
            /// </summary>
            public string DepartmentAddress { get; set; }

            /// <summary>
            /// 医生签名
            /// </summary>
            public string DoctorSign { get; set; }

          

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridInvo.GetSelectedRowDtos<OutApplicationDto>();
            if (selectIndexes == null)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            if (selectIndexes.Count == 0)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {
                
                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何申请单号！");
                    return;
                }
                try
                {
                    TJSQDto tJSQDto = new TJSQDto();
                    tJSQDto.SQDH = currentDate.ApplicationNum;
                    var result = _customerSvr.DelApply(tJSQDto);
                    XtraMessageBox.Show(result.err);
                    refresh();
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridInvo.GetSelectedRowDtos<OutApplicationDto>();
            if (selectIndexes == null)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            if (selectIndexes.Count == 0)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何申请单号！");
                    return;
                }
                try
                {
                    var reportJson = new ReportJson();
                    reportJson.Master = new List<Master>();
                    var master = new Master();
                    master.CompanyName = gtClientName;
                    master.PayMoney = currentDate.ZHMoney.ToString();
                    master.UserName = CurrentUser.Name;
                    master.AppliyNum = currentDate.ApplicationNum;

                    string mzh = "";
                          EntityDto<Guid> entityDto = new EntityDto<Guid>();
                        entityDto.Id = GuidClientRegID;
                        UpClientMZDto = clientRegAppService.getClientMZ(entityDto);
                        mzh = UpClientMZDto.Remark;
                        master.mzh = mzh;
                    master.FPName = currentDate.FPName;
                    master.remark = currentDate.Remark;
                    reportJson.Master.Add(master);
                    var gridppUrl = GridppHelper.GetTemplate("申请单.grf");
                    var report = new GridppReport();
                    report.LoadFromURL(gridppUrl);
                    var reportJsonString = JsonConvert.SerializeObject(reportJson);
                    report.LoadDataFromXML(reportJsonString);
                    report.Print(false);
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridInvo.GetSelectedRowDtos<OutApplicationDto>();
            if (selectIndexes == null)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            if (selectIndexes.Count == 0)
            {
                ShowMessageBoxInformation("尚未选定任何申请单号！");
                return;
            }
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何申请单号！");
                    return;
                }
                try
                {
                    if (currentDate.REFYZK== currentDate.ZHMoney)
                    {
                        MessageBox.Show("该申请单已全部回款！");
                        return;
                    }
                    var ss = currentDate.ZHMoney;
                    if (currentDate.REFYZK.HasValue)
                    {
                        ss = ss - currentDate.REFYZK.Value;
                    }
                    frmZKJE frmZKJE = new frmZKJE(ss);
                    frmZKJE.ShowDialog();
                    if (frmZKJE.DialogResult == DialogResult.OK)
                    {
                        TJSQ hisinput = new TJSQ();
                        hisinput.SQDH = currentDate.ApplicationNum;
                        hisinput.BRSFH = CurrentUser.EmployeeNum;
                        hisinput.BRFPH = "";
                        hisinput.FYZK = frmZKJE.OutReMoney;
                        var sfsq = chargeAppServicechargeAppService.HISTTCharge(hisinput);
                        refresh();
                        MessageBox.Show("设置成功！");
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }

        }
    }
}
