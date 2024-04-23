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
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using System.Diagnostics;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using DevExpress.XtraCharts;
using System.Globalization;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.CusReg;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CustomerNumber
{
    public partial class FrmTJRSTJ : UserBaseForm
    {
        private ICustomerAppService customerApp;
        private IClientInfoesAppService _clientInfoesAppService; //单位接口
        private List<CustomerRegDto> occConclusls;
        //private List<QueryAllForNumberDto> result = null;
        private List<QueryAllForNumberDto> result = null;

        public FrmTJRSTJ()
        {
            InitializeComponent();
            customerApp = new CustomerAppService();
            _clientInfoesAppService = new ClientInfoesAppService();
        }

        private void FrmTJRSTJ_Load(object sender, EventArgs e)
        {       
            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 0, Name = "全部" });
            list.Add(new EnumModel { Id = 1, Name = "团检" });
            list.Add(new EnumModel { Id = 2, Name = "个检" });
            txt_type.Properties.DataSource = list;

            //gridViewNumberList.Columns[gridColumnSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //gridViewNumberList.Columns[gridColumnSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            //var companyList = _clientInfoesAppService.Query(new Application.ClientInfoes.Dto.ClientInfoesListInput());
            //searchLookUpEditCompany.Properties.DataSource = companyList;

            var clientRegs = DefinedCacheHelper.GetClientRegNameComDto();
            searchLookUpEditCompany.Properties.DataSource = clientRegs;
            searchLookUpEditCompany.Properties.DisplayMember = "ClientName";
            searchLookUpEditCompany.Properties.ValueMember = "Id";

            var sexList = SexHelper.GetSexForPerson();
            lookUpEditRenyuanLeibie.Properties.DataSource = customerApp.GetAllPersonTypes();
            lookUpEditSex.Properties.DataSource = sexList;
            dateEditStartDate.DateTime = DateTime.Now;
            dateEditEndDate.DateTime = DateTime.Now;
            // simpleButton_Tongji_Click(simpleButton_Tongji, new EventArgs());
            gridViewNumberList.Columns[conReviewSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewNumberList.Columns[conReviewSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(ReviewSateTypeHelper.ReviewFormatter);

            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();
        }

        private void simpleButton_Tongji_Click(object sender, EventArgs e)
        {
            treeListNumber.ClearNodes();
            gridControlList.DataSource = null;
            QueryAllForNumberInput inputDto = new QueryAllForNumberInput();
            //inputDto.CheckState = (int)PhysicalEState.Not;
            inputDto.RegisterState = (int)RegisterState.Yes;

            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            if (lookUpEditSex.EditValue != null)
                inputDto.Sex = (int)lookUpEditSex.EditValue;

            //if (dateEditStartDate.DateTime != null)
            if (dateEditStartDate.EditValue != null)
                inputDto.DateStart = dateEditStartDate.DateTime;
            //if (dateEditEndDate.DateTime != null)
            if (dateEditEndDate.EditValue != null)
                inputDto.DateEnd = dateEditEndDate.DateTime;
            //体检日期
            if (!string.IsNullOrWhiteSpace(comDateType?.EditValue.ToString()) && comDateType?.EditValue.ToString()=="体检日期")
            {
                inputDto.DateType = 1;
            }
            if (searchLookUpEditCompany.EditValue != null && !searchLookUpEditCompany.EditValue.Equals(string.Empty))
                inputDto.CompanyId = (Guid)searchLookUpEditCompany.EditValue;

            if (lookUpEditRenyuanLeibie.EditValue != null)
                inputDto.PersonalTypeId = (Guid)lookUpEditRenyuanLeibie.EditValue;

            if (!string.IsNullOrWhiteSpace(textEditJieshaoren.Text))
                inputDto.Introducer = textEditJieshaoren.Text;

            try
            {
                var allDate = customerApp.QueryAllForNumber(inputDto);
                var company = allDate.Where(a => a.ClientRegId != null && a.ClientRegId != Guid.Empty);

                var types = company.Where(a => a.PersonnelCategory != null);
                var typesId = types.Select(a => a.PersonnelCategory.Id).Distinct();
                string typesStr = string.Empty;
                foreach (var typeId in typesId)
                {
                    var typeCount = types.Where(a => a.PersonnelCategory.Id == typeId);
                    typesStr += typeCount.FirstOrDefault().PersonnelCategory.Name + ":" + typeCount.Count() + "人,";
                }
                typesStr = typesStr.ToString().Trim(',');

                var companyNode = treeListNumber.AppendNode(new object[] {
                    "团检"+"(共"+company.Count()+"人"+(typesStr==string.Empty?string.Empty:","+typesStr)+")"
                    }, -1);
                companyNode.Tag = Guid.Empty;
                var companyDis = company.OrderByDescending(o => o.BookingDate).Select(c => c.ClientRegId).Distinct();
                foreach (var companyId in companyDis)
                {
                    var companySum = company.Where(c => c.ClientRegId == companyId);

                    var companyTypes = companySum.Where(c => c.PersonnelCategory != null);
                    var companyTypesId = companyTypes.Select(a => a.PersonnelCategory.Id).Distinct();
                    string companyTypesStr = string.Empty;
                    foreach (var companyTypeId in companyTypesId)
                    {
                        var companyTypeCount = companyTypes.Where(a => a.PersonnelCategory.Id == companyTypeId);
                        companyTypesStr += companyTypeCount.FirstOrDefault().PersonnelCategory.Name + ":" + companyTypeCount.Count() + "人,";
                    }
                    companyTypesStr = companyTypesStr.ToString().Trim(',');

                    var sonNode = treeListNumber.AppendNode(new object[] {
                    companySum.FirstOrDefault()?.ClientReg?.ClientInfo?.ClientName+"(共"+companySum.Count()+"人"+(companyTypesStr==string.Empty?string.Empty:","+companyTypesStr)+")"
                    }, companyNode.Id);
                    sonNode.Tag = companyId;
                }
                companyNode.ExpandAll();

                var personal = allDate.Where(a => a.ClientRegId == null || a.ClientRegId == Guid.Empty);

                var personalTypes = personal.Where(p => p.PersonnelCategory != null);
                var personalTypesId = personalTypes.Select(a => a.PersonnelCategory.Id).Distinct();
                string personalTypesStr = string.Empty;
                foreach (var personalTypeId in personalTypesId)
                {
                    var companyTypeCount = personalTypes.Where(a => a.PersonnelCategory.Id == personalTypeId);
                    personalTypesStr += companyTypeCount.FirstOrDefault().PersonnelCategory.Name + ":" + companyTypeCount.Count() + "人,";
                }
                personalTypesStr = personalTypesStr.ToString().Trim(',');

                var personalNode = treeListNumber.AppendNode(new object[] {
                    "个人"+"(共"+personal.Count()+"人"+(personalTypesStr==string.Empty?string.Empty:","+personalTypesStr)+")"
                    }, -1);
                personalNode.Tag = null;

                personalNode.ExpandAll();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
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

        private void lookUpEditTijianLeibie_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditRenyuanLeibie.EditValue = null;
        }

        private void searchLookUpEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                searchLookUpEditCompany.EditValue = null;
        }

        private void dateEditStartDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                dateEditStartDate.EditValue = null;
        }

        private void dateEditEndDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                dateEditEndDate.EditValue = null;
        }

        private void lookUpEditSex_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditSex.EditValue = null;
        }

        private void treeListNumber_Click(object sender, EventArgs e)
        {
            QueryAllForNumberInput inputDto = new QueryAllForNumberInput();
            inputDto.RegisterState = (int)RegisterState.Yes;
            inputDto.SelectType = 1;
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            try
            {
                if (lookUpEditSex.EditValue != null)
                    inputDto.Sex = (int)lookUpEditSex.EditValue;

                if (dateEditStartDate.EditValue != null)
                    inputDto.DateStart = dateEditStartDate.DateTime;
                if (dateEditEndDate.EditValue != null)
                    inputDto.DateEnd = dateEditEndDate.DateTime;

                if (!string.IsNullOrWhiteSpace(textEditJieshaoren.Text))
                    inputDto.Introducer = textEditJieshaoren.Text;

                if (searchLookUpEditCompany.EditValue != null && !searchLookUpEditCompany.EditValue.Equals(string.Empty))
                {
                    inputDto.CompanyId = (Guid)searchLookUpEditCompany.EditValue;

                }


                if (lookUpEditRenyuanLeibie.EditValue != null)
                    inputDto.PersonalTypeId = (Guid)lookUpEditRenyuanLeibie.EditValue;

                var clientRegId = treeListNumber.FocusedNode.Tag;
                if (clientRegId != null)
                {
                    //if ((Guid)clientRegId == Guid.Empty)
                    //    return;
                    inputDto.ClientRegId = (Guid)clientRegId;
                }

                //体检日期
                if (!string.IsNullOrWhiteSpace(comDateType?.EditValue.ToString()) && comDateType?.EditValue.ToString() == "体检日期")
                {
                    inputDto.DateType = 1;
                }

                var allDate = customerApp.QueryAllForPerson(inputDto);
                gridControlList.DataSource = allDate;
                //选完树改变公司选择下拉框的值
                //var companyChange = allDate.FirstOrDefault();
                //if (companyChange != null)
                //{
                //    var companyInfo = companyChange.ClientReg;
                //    if (companyInfo != null)
                //    {
                //        searchLookUpEditCompany.EditValue = companyInfo.ClientInfo.Id;
                //    }
                //    else
                //        searchLookUpEditCompany.EditValue = null;
                //}
                //else
                //    searchLookUpEditCompany.EditValue = null;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
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

        private void simpleButtonDaochu_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            QueryAllForNumberInput inputDto = new QueryAllForNumberInput();
            //inputDto.CheckState = (int)PhysicalEState.Not;
            inputDto.RegisterState = (int)RegisterState.Yes;
            inputDto.SelectType = 1;
            if (lookUpEditSex.EditValue != null)
                inputDto.Sex = (int)lookUpEditSex.EditValue;

            if (dateEditStartDate.EditValue != null)
                inputDto.DateStart = dateEditStartDate.DateTime;
            if (dateEditEndDate.EditValue != null)
                inputDto.DateEnd = dateEditEndDate.DateTime;

            if (searchLookUpEditCompany.EditValue != null && !searchLookUpEditCompany.EditValue.Equals(string.Empty))
            {
                inputDto.CompanyId = (Guid)searchLookUpEditCompany.EditValue;
            }

            if (!string.IsNullOrWhiteSpace(textEditJieshaoren.Text))
                inputDto.Introducer = textEditJieshaoren.Text;

            if (lookUpEditRenyuanLeibie.EditValue != null)
                inputDto.PersonalTypeId = (Guid)lookUpEditRenyuanLeibie.EditValue;

            var clientRegId = treeListNumber.FocusedNode.Tag;
            if (clientRegId != null)
            {
                inputDto.ClientRegId = (Guid)clientRegId;
            }
            try
            {
                var allDate = customerApp.QueryAllForPerson(inputDto).OrderByDescending(a => a.ClientRegId).ToList();
                if (allDate.Count <= 0)
                    return;

                DataTable table = new DataTable();
                table.Columns.Add("体检日期");
                table.Columns.Add("档案号");
                table.Columns.Add("体检号");
                table.Columns.Add("介绍人");
                table.Columns.Add("姓名");
                table.Columns.Add("性别");
                table.Columns.Add("年龄");
                table.Columns.Add("预约备注");
                table.Columns.Add("单位");
                table.Columns.Add("部门");
                table.Columns.Add("分组名称");
                table.Columns.Add("加项");
                table.Columns.Add("减项");
                table.Columns.Add("人员类别");
                table.Columns.Add("收费状态");
                table.Columns.Add("个人已收");
                table.Columns.Add("应收金额");
                foreach (var item in allDate)
                {
                    var row = table.NewRow();
                    row["体检日期"] = item.BookingDate.Value.ToString("yyyy-MM-dd");
                    row["档案号"] = item.Customer.ArchivesNum;
                    row["体检号"] = item.CustomerBM;
                    row["介绍人"] = item.Jieshaoren;
                    row["姓名"] = item.Customer.Name;
                    row["性别"] = SexHelper.CustomSexFormatter(item.Customer.Sex);
                    row["年龄"] = item.Customer.Age;
                    row["预约备注"] = item.Remarks;
                    row["单位"] = item.ClientReg == null ? "" : item.ClientReg.ClientInfo.ClientName;
                    row["部门"] = item.Customer.Department;
                    row["分组名称"] = item.ClientTeamInfo == null ? "" : item.ClientTeamInfo.TeamName;
                    row["加项"] = item.AddItem;
                    row["减项"] = item.MinusItem;
                    row["人员类别"] = item.PersonnelCategory == null ? "" : item.PersonnelCategory.Name;
                    row["收费状态"] = item.PayType;
                    row["个人已收"] = item.McusPayMoney == null ? 0 : item.McusPayMoney.PersonalPayMoney;
                    row["应收金额"] = (item.McusPayMoney == null ? 0 : item.McusPayMoney.PersonalMoney) + (item.McusPayMoney == null ? 0 : item.McusPayMoney.ClientMoney);

                    table.Rows.Add(row);
                }
                var countRow = table.NewRow();
                countRow[12] = "个人已收总额：" + (allDate.Where(a => a.McusPayMoney != null).Sum(a => a.McusPayMoney.PersonalPayMoney));
                countRow[13] = "应收总额：" + (allDate.Where(a => a.McusPayMoney != null).Sum(a => a.McusPayMoney.ClientMoney) + allDate.Where(a => a.McusPayMoney != null).Sum(a => a.McusPayMoney.PersonalMoney));

                table.Rows.Add(countRow);

                //ExcelHelper.ExportToExcel("花名册", gridControl1);
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "体检人数统计";

                if (allDate.FirstOrDefault().ClientReg != null)
                    saveFileDialog.FileName = allDate.FirstOrDefault().ClientReg.ClientInfo.ClientName;

                saveFileDialog.DefaultExt = "xls";
                saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
                if (saveFileDialog.ShowDialog() != DialogResult.OK
                ) //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                    return;
                var FileName = saveFileDialog.FileName;
                #region MyRegion
                //总人数
                var zrs = allDate.ToList();
                //未登记人数
                var wdj = allDate.Where(p => p.RegisterState == 1).ToList();
                //已登记为体检人数
                var ydjwtj= allDate.Where(p => p.RegisterState == 2 &&
                  p.CheckSate==1 &&  p.SummSate !=3 && p.SummSate != 4).ToList();
                //体检中人数
                var tjz = allDate.Where(p => p.RegisterState == 2 
                && p.CheckSate == 2 && p.SummSate != 3 && p.SummSate != 4).ToList();
                //体检完成未总检
                var tjwc = allDate.Where(p => p.RegisterState == 2
                && p.CheckSate == 3 && p.SummSate != 3 && p.SummSate != 4).ToList();
                //总检人数
                var shrs = allDate.Where(p =>p.RegisterState==2 &&
                (  p.SummSate == 3 || p.SummSate == 4)).ToList();
                DataTable dtrs = new DataTable();
                dtrs.Columns.Add("统计项目");
                dtrs.Columns.Add("结果");
                DataRow rsdr = dtrs.NewRow();
                rsdr["统计项目"] = "总人数：("+ zrs.Count + ")";
                rsdr["结果"] = string.Join(",", zrs.Select(p=>p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(rsdr);
                DataRow wdjdr = dtrs.NewRow();
                wdjdr["统计项目"] = "未登记人数：(" + wdj.Count + ")";
                wdjdr["结果"] = string.Join(",", wdj.Select(p => p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(wdjdr);

                DataRow ydjwtjdt = dtrs.NewRow();
                ydjwtjdt["统计项目"] = "已登记未体检人数：(" + ydjwtj.Count + ")";
                ydjwtjdt["结果"] = string.Join(",", ydjwtj.Select(p => p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(ydjwtjdt);

                DataRow tjzdt = dtrs.NewRow();
                tjzdt["统计项目"] = "体检中人数：(" + tjz.Count + ")";
                tjzdt["结果"] = string.Join(",", tjz.Select(p => p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(tjzdt);

                DataRow tjwcdt = dtrs.NewRow();
                tjwcdt["统计项目"] = "体检完成未总检：(" + tjwc.Count + ")";
                tjwcdt["结果"] = string.Join(",", tjwc.Select(p => p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(tjwcdt);

                DataRow shrsdt = dtrs.NewRow();
                shrsdt["统计项目"] = "已总检人数：(" + shrs.Count + ")";
                shrsdt["结果"] = string.Join(",", shrs.Select(p => p.Customer.Name)).Distinct().ToList();
                dtrs.Rows.Add(shrsdt);


                #endregion
                ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
                DataSet ds = new DataSet();
                ds.Tables.Add(table);
                ds.Tables.Add(dtrs);
                ex.DataTableToExcel(table, "体检人数统计", true);
                ex.Dispose();
                if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    DialogResult.Yes)
                    Process.Start(FileName); //打开指定路径下的文件
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
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

        public FrmTJRSTJ(List<CustomerRegDto> input)
        {
            
            occConclusls = input;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QueryAllForNumberInput inputDto = new QueryAllForNumberInput();
            int StartDay = 0;
            int EndDay = 0;
            int StartMonth = 0;
            int EndMonth = 0;
            if (dateEdit1.EditValue != null)
            {
                inputDto.DateStart = (DateTime)dateEdit1.EditValue;
                EndMonth = Convert.ToInt32(dateEdit1.Text.ToString().Substring(5, 2));
                EndDay = Convert.ToInt32(dateEdit1.Text.ToString().Substring(8, 2));
                
            }
            if (dateEdit2.EditValue != null)
            {
                inputDto.DateEnd = (DateTime)dateEdit2.EditValue;
                StartMonth = Convert.ToInt32(dateEdit2.Text.ToString().Substring(5, 2));
                StartDay = Convert.ToInt32(dateEdit2.Text.ToString().Substring(8, 2));
                
            }
            int StartDays = 0;
            int EndDays= 0;
            int StartMonths = 0;
            int EndMonths = 0;
            if (dateEdit3.EditValue != null)
            {
                inputDto.DateStart = (DateTime)dateEdit3.EditValue;
                EndMonths = Convert.ToInt32(dateEdit3.Text.ToString().Substring(5, 2));
                EndDays = Convert.ToInt32(dateEdit3.Text.ToString().Substring(8,2));           
            }
           
            if (dateEdit4.EditValue != null)
            {
                inputDto.DateEnd = (DateTime)dateEdit4.EditValue;
                StartMonths = Convert.ToInt32(dateEdit4.Text.ToString().Substring(5, 2));
                StartDays = Convert.ToInt32(dateEdit4.Text.ToString().Substring(8, 2));               
            }
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            var result = customerApp.QueryAllCount(inputDto).GroupBy(o => o.GroupOrPersonal).Select(o => new result
            {
                conName = o.Key,
                conCount = o.Count(),
            }).ToList();
            ToLineGragh(result);
            
            if (StartDays - EndDays <= 30 && StartMonths == EndMonths && StartMonths != 0)
             {
                var results2 = customerApp.QueryAllCount(inputDto).Where(o => o.RegisterState == 2).OrderBy(o => o.LoginDate.Value.Day).GroupBy(o => new { o.LoginDate.Value.Day })
                           .Select(o => new result2 { conNames = o.Where(s => s.GroupOrPersonal == "个人").Count(), conCount = o.Where(s => s.GroupOrPersonal == "单位").Count(), conName = o.Key.Day }).ToList();
                   ToLineGraghs2(results2);
             }
            else  if (StartDay - EndDay <= 30 && EndMonth == StartMonth && StartMonth!=0)
             {
                var results2 = customerApp.QueryAllCount(inputDto).Where(o => o.RegisterState == 2).OrderBy(o => o.LoginDate.Value.Day).GroupBy(o => new { o.LoginDate.Value.Day })
                           .Select(o => new result2 { conNames = o.Where(s => s.GroupOrPersonal == "个人").Count(), conCount = o.Where(s => s.GroupOrPersonal == "单位").Count(), conName = o.Key.Day }).ToList();
                    ToLineGraghs2(results2);
             }
             else
             {
                var results2 = customerApp.QueryAllCount(inputDto).Where(o => o.RegisterState == 2).OrderBy(o => o.LoginDate.Value.Month).GroupBy(o => new { o.LoginDate.Value.Month })
                            .Select(o => new result2 { conNames = o.Where(s => s.GroupOrPersonal == "个人").Count(), conCount = o.Where(s => s.GroupOrPersonal == "单位").Count(), conName = o.Key.Month }).ToList();
                   ToLineGraghs2(results2);
             }

            var results = customerApp.QueryAllCount(inputDto).Where(s=>s.PhysicalType==2).OrderBy(s => s.LoginDate.Value.Day).GroupBy(o => new { o.LoginDate.Value.Day }).Select(o => new results
            {
                conName = o.Key.Day,
                conCount = o.Count(),
            }).ToList();
            ToLineGraghs(results);

            var result3 = customerApp.QueryAllCount(inputDto).OrderBy(o=>o.LoginDate.Value.Day).GroupBy(o => new { o.LoginDate.Value.Day }).Select(o => new result3
            {
                conCount1 = o.Where(s => s.PhysicalType == 2).Count(),
                conCount2 =o.Where(s => s.PhysicalType != 2).Count(),
                conCount3 = o.Key.Day,
            }).ToList();
            ToLineGraghs3(result3);
            splashScreenManager.CloseWaitForm();
        }

        private void ToLineGraghs3(List<result3> data)
        {
            string xBindName = "conCount3";
            string yBindName = "conCount2";
            string seriesName = "团检总人数";
            ViewType seriesType = ViewType.Bar;
            seriesType = ViewType.Bar;
            chartControl2.Series.Clear();
            CreateSeries3(chartControl2, seriesName, seriesType, data, xBindName, yBindName, null);
            seriesName = "职业健康总人数";
            yBindName = "conCount1";
            CreateSeries3(chartControl2, seriesName, seriesType, data, xBindName, yBindName, null);
        }
        public void CreateSeries3(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.ShowInLegend = true;
            _series.Label.TextPattern = "{A}:{V}:{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }


        private void ToLineGraghs2(List<result2> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "团检";
            ViewType seriesType = ViewType.Line;
            seriesType = ViewType.Line;
            chartControl1.Series.Clear();
            CreateSeries2(chartControl1, seriesName, seriesType, data, xBindName, yBindName, null);
            seriesName = "个人";
            yBindName = "conNames";
            CreateSeries2(chartControl1, seriesName, seriesType, data, xBindName, yBindName, null);
        }
        public void CreateSeries2(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.ShowInLegend = true;
            _series.Label.TextPattern = "{A}:{V}:{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }
        private void ToLineGraghs(List<results> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "conName";
            ViewType seriesType = ViewType.Line;
            seriesType = ViewType.Line;
            chartControl3.Series.Clear();           
            CreateSeriess(chartControl3, seriesName, seriesType, data, xBindName, yBindName, null);
        }
        public void CreateSeriess(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.ShowInLegend = true;
            _series.Label.TextPattern = "{A}:{V}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }
        private void ToLineGragh(List<result> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "conName";
            ViewType seriesType = ViewType.Pie;
            seriesType = ViewType.Pie;
            ChartControl.Series.Clear();
            CreateSeries(ChartControl, seriesName, seriesType, data, xBindName, yBindName, null );            
        }
       
        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
             Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.ShowInLegend = true;
            _series.Label.TextPattern = "{A}:{V}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            dgc.DataSource = null;
            result = new List<QueryAllForNumberDto>();            
            QueryAllForNumberInput query = new QueryAllForNumberInput();
            DateTime QueryStartTime = Convert.ToDateTime(dtpStart.EditValue.ToString());
            DateTime QueryEndTime = Convert.ToDateTime(dtpEnd.EditValue.ToString());
            //if (dateEdit5.EditValue != null && dateEdit6.EditValue!=null)
            //{
            //    DateTime QueryStartTime1 = Convert.ToDateTime(dateEdit5.EditValue.ToString());
            //    DateTime QueryEndTime1 = Convert.ToDateTime(dateEdit6.EditValue.ToString());
            //}
            
            if (QueryStartTime > QueryEndTime || QueryStartTime.Year != QueryEndTime.Year || dtpStart.EditValue == null || dtpEnd.EditValue == null)
            {
                MessageBox.Show("日期格式不正确，请选择正确的时间范围");
                return;
            }

            if (txt_type.EditValue != null)
            {
                query.SelectType = (int)txt_type.EditValue;
            }
            //时间
            if (dtpStart.EditValue != null)
            {
                query.DateStart = QueryStartTime;
            }
            if (dtpEnd.EditValue != null)
            {
                query.DateEnd = QueryEndTime;
            }
            //预约
            //if (dateEdit5.EditValue != null)
            //{
            //    query.DateStarts = (DateTime)dateEdit5.EditValue;
            //}
            //if (dateEdit6.EditValue != null)
            //{
            //    query.DateEnds = (DateTime)dateEdit6.EditValue;
            //}
            if (rdo_Period.SelectedIndex == 0)
            {
                query.WeekQuery= true;
            }
            else
                query.WeekQuery = false;           
            result = customerApp.QueryAllForNumbers(query);
            if (rdo_Period.SelectedIndex == 0)
            {
                gridColumn1.Caption = "周期(周)";
                GetPeriod(QueryStartTime, QueryEndTime);
                List<QueryAllForNumberDto> QueryDataList = new List<QueryAllForNumberDto>();
                for (int i = 0; i < YearList.Count; i++)
                {
                    QueryAllForNumberDto addweekRow = new QueryAllForNumberDto();
                    addweekRow.Type = YearList[i].Type;
                    addweekRow.CurrentData = result.Where(r => Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                    > YearList[i].STtime && Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                    < YearList[i].EDtime).Sum(r => r.CurrentData);
                    addweekRow.CurrentDatas = result.Where(r => Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                    > YearList[i].STtime && Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                    < YearList[i].EDtime).Sum(r => r.CurrentDatas);
                    
                    QueryDataList.Add(addweekRow);
                }
                foreach (var s in QueryDataList)
                {
                    s.RenCount = s.CurrentData + s.CurrentDatas;
                }
                result = QueryDataList;
            }
            if (rdo_Period.SelectedIndex == 1)
            {
                gridColumn1.Caption = "周期(月)";
                result = result.Where(r => Convert.ToInt32(r.Type.Substring(0, r.Type.Length - 1)) >= QueryStartTime.Month
                                    && Convert.ToInt32(r.Type.Substring(0, r.Type.Length - 1)) <= QueryEndTime.Month).ToList();
            }
            if (rdo_Period.SelectedIndex == 2)
            {
                gridColumn1.Caption = "周期(季)";
                List<QueryAllForNumberDto> QueryDataList = new List<QueryAllForNumberDto>();

                foreach (var item in QueryList())
                {
                    QueryAllForNumberDto QueryData = new QueryAllForNumberDto();
                    QueryData.Type = item.Type;
                    QueryData.CurrentData = result.Where(r => item.Query.Contains(r.Type)).Sum(r => r.CurrentData);
                    QueryData.CurrentDatas = result.Where(r => item.Query.Contains(r.Type)).Sum(r => r.CurrentDatas);
                    QueryDataList.Add(QueryData);
                    foreach (var s in QueryDataList)
                    {
                        s.RenCount = s.CurrentData + s.CurrentDatas;
                    }

                }
                result = QueryDataList.Where(r => r.CurrentData != 0 && r.CurrentDatas != 0).ToList();
            }
            ToLineGragh(result);
            dgc.DataSource = result;
        }
        /// <summary>
        /// 把接收到的数据转换额外柱状图表所需要的数据, 绘制图表
        /// </summary>
        private void ToLineGragh(List<QueryAllForNumberDto> data)
        {
            string xBindName = "Type";
            string yBindName = "CurrentDatas";
            string seriesName = gridColumn16.Caption;
            ViewType seriesType = ViewType.Bar;
            if (cob_LeiXing.Text == "饼图")
            {
                seriesType = ViewType.Pie;
                chartControl4.Series.Clear();
                CreateSeries6(chartControl4, seriesName, seriesType, data, xBindName, yBindName, null);
                seriesName = gridColumn18.Caption;
                yBindName = "CurrentData";
                CreateSeries6(chartControl4, seriesName, seriesType, data, xBindName, yBindName, null);
            }
            else if (cob_LeiXing.Text == "折线图")
            {
                seriesType = ViewType.Line;
                chartControl4.Series.Clear();
                CreateSeries5(chartControl4, seriesName, seriesType, data, xBindName, yBindName, null);
                seriesName = gridColumn18.Caption;
                yBindName = "CurrentData";
                CreateSeries5(chartControl4, seriesName, seriesType, data, xBindName, yBindName, null);
            }
        }
        /// <summary>
        /// 绘制图标
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="seriesName"></param>
        /// <param name="seriesType"></param>
        /// <param name="dataSource"></param>
        /// <param name="xBindName"></param>
        /// <param name="yBindName"></param>
        /// <param name="createSeriesRule"></param>
        public void CreateSeries5(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);

            //_series.ArgumentScaleType = ScaleType.Qualitative;
            //_series.ArgumentDataMember = xBindName;
            //_series.ValueDataMembers[0] = yBindName;
            //_series.ValueDataMembers.AddRange(yBindName);
            //_series.ShowInLegend = true;
            //_series.Label.TextPattern = "{A}:{V}:{s}";
            //_series.DataSource = dataSource;
            //if (createSeriesRule != null)
            //    createSeriesRule(_series);
            //chat.Series.Add(_series);
        }
        public void CreateSeries6(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(yBindName);
            _series.ShowInLegend = true;
            _series.Label.TextPattern = "{A}:{V}//{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }
        /// <summary>
        /// 获取指定期间的起止日期
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        List<Statistic> YearList;
        List<Statistic> LastYearList;
        public void GetPeriod(DateTime beginDate, DateTime endDate)
        {
            YearList = new List<Statistic>();
            LastYearList = new List<Statistic>();
            int Syear = beginDate.Year;
            beginDate = beginDate.AddDays(1 - Convert.ToInt32(beginDate.DayOfWeek.ToString("d")));  //本周周一
            int weeks = GetWeekOfYear(beginDate) - 1;//本年第几周
            GetWeek(Syear - 1, weeks + 1);//去年第几周的开始时间和结束时间
                                          //本年查询周数
            for (DateTime dt = beginDate.Date; dt < Convert.ToDateTime(dtpEnd.EditValue.ToString()); dt = dt.AddDays(6))
            {
                weeks = weeks + 1;
                Statistic result = new Statistic();
                result.Type = "第" + weeks + "周";
                result.STtime = dt;
                result.EDtime = dt.AddDays(7);
                YearList.Add(result);
            }
            //往年查询周数
            int weeks2 = weeks - YearList.Count;
            for (int i = 0; i < YearList.Count; i++)
            {
                if (i != 0)
                    dtweekStart = dtweekStart.AddDays(6);
                weeks2 = weeks2 + 1;
                Statistic result2 = new Statistic();
                result2.Type = "第" + weeks2 + "周";
                result2.STtime = dtweekStart;
                result2.EDtime = dtweekStart.AddDays(7);
                LastYearList.Add(result2);
            }
        }
        /// <summary>
        /// 一年中的周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        DateTime dtweekStart;
        DateTime dtweekeEnd;
        public void GetWeek(int year, int weekNum)
        {
            var dateTime = new DateTime(year, 1, 1);
            dateTime = dateTime.AddDays(7 * weekNum);
            dtweekStart = dateTime.AddDays(-(int)dateTime.DayOfWeek + (int)DayOfWeek.Monday);
            dtweekeEnd = dateTime.AddDays((int)DayOfWeek.Saturday - (int)dateTime.DayOfWeek + 1);
        }
        /// <summary>
        /// 季度
        /// </summary>
        /// <returns></returns>
        public List<Statistic> QueryList()
        {
            List<Statistic> list = new List<Statistic>();
            list.Add(new Statistic() { Type = "第一季度", Query = new List<string>() { "1月", "2月", "3月" } });
            list.Add(new Statistic() { Type = "第二季度", Query = new List<string>() { "4月", "5月", "6月" } });
            list.Add(new Statistic() { Type = "第三季度", Query = new List<string>() { "7月", "8月", "9月" } });
            list.Add(new Statistic() { Type = "第四季度", Query = new List<string>() { "10月", "11月", "12月" } });
            return list;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            var allDate = gridControlList.DataSource as List<QueryAllForPersonDto>;
           
           var filname= ExcelHelper.GridViewToExcel(gridViewNumberList, "人数统计", "人数统计");
            //ExcelHelper.ExportToExcel("人数统计", gridControlList);


            if (allDate != null)
            {
                #region MyRegion
                //总人数
                var zrs = allDate.ToList();
                //未登记人数
                var wdj = allDate.Where(p => p.RegisterState == 1).ToList();
                //已登记为体检人数
                var ydjwtj = allDate.Where(p => p.RegisterState == 2 &&
                   p.CheckSate == 1 && p.SummSate != 3 && p.SummSate != 4).ToList();
                //体检中人数
                var tjz = allDate.Where(p => p.RegisterState == 2
                && p.CheckSate == 2 && p.SummSate != 3 && p.SummSate != 4).ToList();
                //体检完成未总检
                var tjwc = allDate.Where(p => p.RegisterState == 2
                && p.CheckSate == 3 && p.SummSate != 3 && p.SummSate != 4).ToList();
                //总检人数
                var shrs = allDate.Where(p => p.RegisterState == 2 &&
                (p.SummSate == 3 || p.SummSate == 4)).ToList();
                DataTable dtrs = new DataTable();
                dtrs.Columns.Add("统计项目");
                dtrs.Columns.Add("结果");
                DataRow rsdr = dtrs.NewRow();
                rsdr["统计项目"] = "总人数：(" + zrs.Count + ")";
                var dd= string.Join(",", zrs.Select(p => p.Customer.Name).Distinct().ToList());
                rsdr["结果"] = string.Join(",", zrs.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(rsdr);
                DataRow wdjdr = dtrs.NewRow();
                wdjdr["统计项目"] = "未登记人数：(" + wdj.Count + ")";
                wdjdr["结果"] = string.Join(",", wdj.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(wdjdr);

                DataRow ydjwtjdt = dtrs.NewRow();
                ydjwtjdt["统计项目"] = "已登记未体检人数：(" + ydjwtj.Count + ")";
                ydjwtjdt["结果"] = string.Join(",", ydjwtj.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(ydjwtjdt);

                DataRow tjzdt = dtrs.NewRow();
                tjzdt["统计项目"] = "体检中人数：(" + tjz.Count + ")";
                tjzdt["结果"] = string.Join(",", tjz.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(tjzdt);

                DataRow tjwcdt = dtrs.NewRow();
                tjwcdt["统计项目"] = "体检完成未总检：(" + tjwc.Count + ")";
                tjwcdt["结果"] = string.Join(",", tjwc.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(tjwcdt);

                DataRow shrsdt = dtrs.NewRow();
                shrsdt["统计项目"] = "已总检人数：(" + shrs.Count + ")";
                shrsdt["结果"] = string.Join(",", shrs.Select(p => p.Customer.Name).Distinct().ToList());
                dtrs.Rows.Add(shrsdt);
                var fileName = filname.Replace("人数统计", "人数统计1");

                ExcelHelper ex = new ExcelHelper(fileName);
                ex.DataTableToExcel(dtrs, "体检人数统计", true);
                ex.Dispose();
                #endregion
            }

            

        }

        private void gridControlList_DoubleClick(object sender, EventArgs e)
        {
            var Customerregdto = gridViewNumberList.GetFocusedRow() as QueryAllForPersonDto;
            if (Customerregdto == null)
                return;
            #region 调用总检界面            

            if (Customerregdto == null)
            {
                ShowMessageBoxWarning("请选中行！");
            }
            else
            {
                CustomerReg queryAll = new CustomerReg(Customerregdto.CustomerBM);
                queryAll.ShowDialog();

            }
            #endregion
        }
    }
    public class RecordData
    {
        public string name { get; set; }
        public int value { get; set; }
    }
    public class Statistic
    {
        public string Type { get; set; }
        public DateTime STtime { get; set; }
        public DateTime EDtime { get; set; }
        public List<string> Query { get; set; }
        public virtual int conName { get; set; }
        public virtual int conNames { get; set; }
        public virtual string conCount { get; set; }
    }
    public class result
    {
        public virtual string conName { get; set; }
        public virtual int conCount { get; set; }       
    }
    public class result2
    {
        public virtual int conName { get; set; }
        public virtual int conNames { get; set; }
        public virtual int conCount { get; set; }
    }
    public class results
    {
        public virtual int conName { get; set; }
        public virtual int conCount { get; set; }
    }
    public class result3
    {
        public virtual int conCount1 { get; set; }
        public virtual int conCount2 { get; set; }
        public virtual int conCount3 { get; set; }
    }
}