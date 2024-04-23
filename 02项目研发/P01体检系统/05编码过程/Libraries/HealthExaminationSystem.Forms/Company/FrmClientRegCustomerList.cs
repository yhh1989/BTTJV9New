using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.CusReg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmClientRegCustomerList : UserBaseForm
    {
        #region 申明变量
        private readonly IClientRegAppService _clientReg = new ClientRegAppService();
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
        private ICustomerAppService customerSvr;//体检预约 
        private IBarPrintAppService barPrintAppService;//体检预约 
        private readonly List<SexModel> _sexModels;
        public Guid ClientinfoId;   //单位ID
        public Guid ClientRegId;    //单位预约ID
        public Guid TeamId;  //分组ID

        public ClientTeamRegDto cteDto;  //单位团体预约Dto
        private IChargeAppService _chargeAppService { get; set; }

        //public List<CreateClientTeamInfoesDto> ListClientTeam; //单位预约分组信息

        //public List<ClientTeamRegitemViewDto> ListClientTeamItem; //单位预约分组项目信息
        #endregion 申明变量
        #region 构造函数
        public FrmClientRegCustomerList()
        {
            InitializeComponent();

            _sexModels = SexHelper.GetSexModelsForItemInfo();
            _chargeAppService = new ChargeAppService();
            //gvCustomerRegs.Columns[gridColumnSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //gvCustomerRegs.Columns[gridColumnSex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSexs);

            //gvCustomerRegs.Columns[gridColumnCheckSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //gvCustomerRegs.Columns[gridColumnCheckSate.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckState);
            gridColumnClientName.Visible = false;
        }
        /// <summary>
        /// 性别转换
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatSexs(object arg)
        {
            try
            {
                return _sexModels.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return _sexModels.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }

        /// <summary>
        /// 体检状态
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatCheckState(object arg)
        {
            try
            {
                return EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(ExaminationState), arg.ToString())));
            }
            catch
            {
                return "未知";
            }
        }
        #endregion 构造函数

        #region 系统事件

        #region 系统加载
        private void FrmClientRegCustomerList_Load(object sender, EventArgs e)
        {
            #region 申明变量
            customerSvr = new CustomerAppService();
            barPrintAppService = new BarPrintAppService();
            #endregion
            BindClientInfo();
        }
        #endregion 系统加载
        #region 添加
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能进行收费设置！");
                return;
            }
            using (var cusDetail = new CusDetail { curCustomRegInfo = new QueryCustomerRegDto { ClientRegId = ClientRegId,PersonnelCategoryId=cteDto.ClientRegDto.PersonnelCategoryId } })
            {
                if (cusDetail.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion 添加

        #region 分页
        /// <summary>
        /// 分页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNav_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Reload();
        }
        #endregion

        #region 修改编辑
        /// <summary>
        /// 修改编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能进行收费设置！");
                return;
            }
            EditCustomerReg();
        }


        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCustomerRegs_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var CustomerBM = gvCustomerRegs.GetRowCellValue(this.gvCustomerRegs.FocusedRowHandle, "CustomerBM").ToString();
                //var row = gvCustomerRegs.GetRow(rowList[0]) as CustomerRegsViewDto;
                //var CustomerBM = row.CustomerBM;
                using (var cusDetail = new CusDetail() { curCustomerBM = CustomerBM ,})
                {
                    if (cusDetail.ShowDialog() == DialogResult.OK)
                        Reload();
                }
                //EditCustomerReg();
            }
        }

        /// <summary>
        /// 编辑人员预约信息
        /// </summary>
        public void EditCustomerReg()
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            //var CustomerBM = gvCustomerRegs.GetRowCellValue(this.gvCustomerRegs.FocusedRowHandle, "CustomerBM").ToString();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要修改的数据！");
                return;
            }
            if (rowList.Count() > 1)
            {
                ShowMessageBoxInformation("请选择一条数据进行编辑！");
                return;
            }
            var row = gvCustomerRegs.GetRow(rowList[0]) as CustomerRegsViewDto;
            var CustomerBM = row.CustomerBM;
            using (var cusDetail = new CusDetail() { curCustomerBM = CustomerBM })
            {
                if (cusDetail.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能进行收费设置！");
                return;
            }
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                XtraMessageBox.Show("请选择要删除的数据！");
                return;
            }
            List<EntityDto<Guid>> listGuid = new List<EntityDto<Guid>>();
            foreach (var item in rowList)
            {
                var row = gvCustomerRegs.GetRow(item) as CustomerRegsViewDto;
                //if (row.CheckSate != (int)ExaminationState.Alr)
                //{
                //    ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", row.Customer.Name));
                //    //XtraMessageBox.Show(string.Format("{0}已开始体检，无法删除！",row.Customer.Name));
                //    return;
                //}bug3212张玉玲要求判断登记状态，已登记不能删除
                if (row.RegisterState == (int)RegisterState.Yes)
                {
                    ShowMessageBoxInformation(string.Format("{0}已登记，无法删除！", row.Customer.Name));
                    return;
                }
                if (row.CheckSate != (int)ExaminationState.Alr)
                {
                    ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", row.Customer.Name));
                    return;
                }
                EntityDto<Guid> gId = new EntityDto<Guid> { Id = row.Id };
                listGuid.Add(gId);
            }
            //操作数据库
            AutoLoading(() =>
            {
                _clientReg.DelCustomerReg(listGuid);
            });
            Reload();
            ShowMessageSucceed("删除成功！");
            //XtraMessageBox.Show("删除成功！");
        }
        #endregion

        #region 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 导入导出

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemExport_Click(object sender, EventArgs e)
        {

            var gridppUrl = GridppHelper.GetTemplate("人员预约信息模板.xls");
            if (GridppHelper.VitrualFileExist(gridppUrl))
            {
                GridppHelper.Download(gridppUrl, "人员预约信息模板");
            }
            else
            {

                var strList = new List<string>() {
                "姓名 *",
                "性别 *",
                "年龄 *",
                "婚姻状况",
                "分组编码",
                "体检号",
                "移动电话",
                "身份证号",
                "出生日期",
                "车间",
                "工种",               
                "接害工龄",
                "总工龄",
                "危害因素（多个，用“|”隔开）",
                "体检类别",
                "工号",
                "部门",
                "职务",
                "医保卡号",
                "预检时间",
                "流水号",
                "民族",
                "准备孕或育",
                "备注",
                "预约备注",
                "介绍人",
                "固定电话",
                "检查类型",
                "接害工龄单位",
                "总工龄单位",
                "客户类别",
                "通讯地址",
                "邮政编码",
                "电子邮件",
                "腾讯QQ",
                 "证件类型",
                 "接害开始时间",
                  "其他工种",                 
                   "用工单位名称",
                   "用工单位机构代码"

            };
                JArray mb_jarray = new JArray();


                var sexList = SexHelper.GetSexForPerson().Select(o => o.Display).ToList();//性别

                var marrySateList = MarrySateHelper.GetMarrySateModelsForItemInfo().Select(o => o.Display).ToList();//婚否

                var breedStateList = BreedStateHelper.GetBreedStateModels().Select(o => o.Display).ToList();//孕育

                //体检类别
                var tjlb = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString()).Select(o => o.Text)?.ToList();
                //检查类型
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
                var jclxlist = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();

                ////车间
                //chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                //var cjlis = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();

                ////工种
                //chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
                //var gzlis = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();



                //性别 
                if (sexList.Count > 0)
                {
                    JObject XBObject = new JObject();
                    XBObject.Add("vlaue", JsonConvert.SerializeObject(sexList));
                    XBObject.Add("cel", "1");
                    mb_jarray.Add(XBObject);
                }
                //婚姻状况;
                if (marrySateList.Count > 0)
                {
                    JObject ZTObject = new JObject();
                    ZTObject.Add("vlaue", JsonConvert.SerializeObject(marrySateList));
                    ZTObject.Add("cel", "3");
                    mb_jarray.Add(ZTObject);
                }
                //孕否
                if (breedStateList.Count > 0)
                {
                    JObject YFObject = new JObject();
                    YFObject.Add("vlaue", JsonConvert.SerializeObject(breedStateList));
                    YFObject.Add("cel", "21");
                    mb_jarray.Add(YFObject);
                }
                //体检类别
                if (tjlb.Count > 0)
                {
                    JObject TJLBObject = new JObject();
                    TJLBObject.Add("vlaue", JsonConvert.SerializeObject(tjlb));
                    TJLBObject.Add("cel", "14");
                    mb_jarray.Add(TJLBObject);
                }

                //检查类型
                if (jclxlist.Count > 0)
                {
                    JObject JCLXObject = new JObject();
                    JCLXObject.Add("vlaue", JsonConvert.SerializeObject(jclxlist));
                    JCLXObject.Add("cel", "25");
                    mb_jarray.Add(JCLXObject);
                }

                //年月
                JObject NYObject = new JObject();
                NYObject.Add("vlaue", JsonConvert.SerializeObject(new string[] { "年", "月" }));
                NYObject.Add("cel", "26");
                mb_jarray.Add(NYObject);

                //年月
                JObject NYObject1 = new JObject();
                NYObject1.Add("vlaue", JsonConvert.SerializeObject(new string[] { "年", "月" }));
                NYObject1.Add("cel", "27");
                mb_jarray.Add(NYObject1);
                //时间格式字段
                List<int> cellIndexs = new List<int>();
                cellIndexs.Add(6);
                cellIndexs.Add(12);

                GridControlHelper.DownloadTemplate(strList, "人员预约信息模板", mb_jarray, cellIndexs, "yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能进行收费设置！");
                return;
            }
            using (var frmImport = new FrmImportExcel() { ClientRegId = ClientRegId })
            {
                frmImport.cteDto = cteDto;
                //frmImport.ListClientTeam = ListClientTeam;
                //frmImport.ListClientTeamItem = ListClientTeamItem;
                if (frmImport.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion

        #region 打印

        /// <summary>
        /// 打印导引单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印导引单的数据！");
                return;
            }
            foreach (var item in rowList)
            {
                //CusNameInput cus = new CusNameInput();
                //cus.Id = row.Id;
                //PrintGuidance printGuidance = new PrintGuidance();
                //printGuidance.cusNameInput = cus;
                //printGuidance.Print();
                if (gvCustomerRegs.GetRow(item) is CustomerRegsViewDto row)
                {
                  PrintGuidanceNew.Print(row.Id);
                    //CusNameInput cus = new CusNameInput();
                    //cus.Id = row.Id;
                    //PrintGuidance printGuidance = new PrintGuidance();
                    //printGuidance.cusNameInput = cus;
                    //printGuidance.Print();
             
                  
                }
            }
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBarPrint_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印条码的数据！");
                //XtraMessageBox.Show("请选择要打印条码的数据！");
                return;
            }
            List<CusNameInput> cuslis = new List<CusNameInput>();
            foreach (var item in rowList)
            {
                var row = gvCustomerRegs.GetRow(item) as CustomerRegsViewDto;              
                CusNameInput cus = new CusNameInput();
                cus.Id = row.Id;
                cus.Theme = "1";
                cus.CusRegBM = row.CustomerBM;
                cuslis.Add(cus);
                #region MyRegion
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "江苏鑫亿四院")
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = cus.Id;
                        var slo = customerSvr.getupdate(chargeBM);
                        if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                        {
                            MessageBox.Show("还未生成申请单不能打印条码，请批量登记或登记后再打印！");
                            return;
                        }
                    }
                }
                #endregion
            }
            FrmBarPrint frmBarPrint = new FrmBarPrint();
            frmBarPrint.BarPrintAll(cuslis);
        }


        /// <summary>
        /// 打印导引单\条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印的数据！");
                //XtraMessageBox.Show("请选择要打印的数据！");
                return;
            }
            List<CusNameInput> cuslist = new List<CusNameInput>();
            foreach (var item in rowList)
            {
                var row = gvCustomerRegs.GetRow(item) as CustomerRegsViewDto;
           
                CusNameInput cus = new CusNameInput();
                cus.Id = row.Id;
                cus.Theme = "1";
                cus.CusRegBM = row.CustomerBM;
                cuslist.Add(cus);
                PrintGuidanceNew.Print(row.Id);
                #region MyRegion
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "江苏鑫亿四院")
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = cus.Id;
                        var slo = customerSvr.getupdate(chargeBM);
                        if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                        {
                            MessageBox.Show("还未生成申请单不能打印条码，请批量登记或登记后再打印！");
                            return;
                        }
                    }
                }
                #endregion
            }
            FrmBarPrint frmBarPrint = new FrmBarPrint();
            frmBarPrint.BarPrintAll(cuslist);
        }
        #endregion

        #endregion 系统事件

        #region 初始化窗体

        /// <summary>
        /// 
        /// </summary>
        public void BindClientInfo()
        {
            Reload();
        }
        #endregion


        /// <summary>
        /// 人员预约信息绑定
        /// 绑定Grid数据
        /// 分页绑定
        /// </summary>
        public void Reload()
        {
            int man = 0;
            int woman = 0;
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gdvCustomerReg.DataSource = null;
            try
            {
                //PageInputDto<CustomerRegsInputDto> page = new PageInputDto<CustomerRegsInputDto>();
                //page.TotalPages = TotalPages;
                //page.CurentPage = CurrentPage;

                CustomerRegsInputDto dto = new CustomerRegsInputDto();
                string strSearch = txtSearch.Text;
                if (ClientinfoId != Guid.Empty && ClientinfoId != null)
                    dto.ClientinfoId = ClientinfoId;
                if (ClientRegId != Guid.Empty && ClientinfoId != null)
                    dto.ClientRegId = ClientRegId;
                if (!string.IsNullOrWhiteSpace(strSearch))
                    dto.TextValue = strSearch;
                if (rdoCheckState.EditValue != null && (int)rdoCheckState.EditValue != (int)ExaminationState.Whole)
                    dto.CheckState = (int?)rdoCheckState.EditValue;
                if (radioGroupRegState.EditValue != null && (int)radioGroupRegState.EditValue != (int)ExaminationState.Whole)
                    dto.RegState = (int?)radioGroupRegState.EditValue;
                var customerReg = _clientReg.GetCustomerReg(dto).OrderBy(p=>p.ClientTeamInfo.Id).ThenBy(p=>p.CustomerBM).ToList();

                //page.Input = dto;
                //var output = _clientReg.PageCustomerRegFulls(page);

                //TotalPages = output.TotalPages;
                //CurrentPage = output.CurrentPage;
                //InitialNavigator(dataNav);
                gdvCustomerReg.DataSource = customerReg;
                foreach (var tm in customerReg)
                {
                    if (tm.Customer.Sex == 1)
                    {
                        man += 1;
                    }
                    else
                    {
                        woman += 1;
                    }
                }
                LbCustomer.Text = "共" + customerReg.Count + "人";
                LbCustomerN.Text = "男" + man + "人";
                LbCustomerV.Text = "女" + woman + "人";
            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        private bool getFZState()
        {
            bool isFZState = false;
            if (ClientRegId != Guid.Empty)
            {
                var FZSt = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = ClientRegId });
                if (FZSt == 1)
                {
                    return true;
                }
            }
            return isFZState;

        }

        private void butreplase_Click(object sender, EventArgs e)
        {
            CustomerRegsInputDto dto = new CustomerRegsInputDto();
            string strSearch = txtSearch.Text;
            if (ClientinfoId != Guid.Empty && ClientinfoId != null)
                dto.ClientinfoId = ClientinfoId;
            if (ClientRegId != Guid.Empty && ClientinfoId != null)
                dto.ClientRegId = ClientRegId;
            if (!string.IsNullOrWhiteSpace(strSearch))
                dto.TextValue = strSearch;
            if (rdoCheckState.EditValue != null && (int)rdoCheckState.EditValue != (int)ExaminationState.Whole)
                dto.CheckState = (int?)rdoCheckState.EditValue;
            if (radioGroupRegState.EditValue != null && (int)radioGroupRegState.EditValue != (int)ExaminationState.Whole)
                dto.RegState = (int?)radioGroupRegState.EditValue;
            var customerReg = _clientReg.GetCustomerReg(dto);

            var renamelist = customerReg.GroupBy(p =>new { p.Customer.Name }).Where(p=>p.Count()>1).ToList();
            if (renamelist.Count > 0)
            {
                var cuslist = renamelist.Select(p => p.Key.Name).ToList();
                var nowlist = customerReg.Where(p => cuslist.Contains(p.Customer.Name)).OrderBy(p=>p.Customer.Name).ToList();
                gdvCustomerReg.DataSource = nowlist;
                gdvCustomerReg.Refresh();
                gdvCustomerReg.RefreshDataSource();
            }
            else
            {
                gdvCustomerReg.DataSource = null;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "单位人员";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gdvCustomerReg.ExportToXls(saveFileDialog.FileName);
        }
    }
}
