using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.ErrorInfo;
using HealthExaminationSystem.Win.Navigation;
using HealthExaminationSystem.Win.Properties;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Charge;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Company;
using Sw.Hospital.HealthExaminationSystem.CompanyReport;
using Sw.Hospital.HealthExaminationSystem.Compose;
using Sw.Hospital.HealthExaminationSystem.Crisis;
using Sw.Hospital.HealthExaminationSystem.CusReg;
using Sw.Hospital.HealthExaminationSystem.CusReviews;
using Sw.Hospital.HealthExaminationSystem.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Foreground;
using Sw.Hospital.HealthExaminationSystem.GroupReport;
using Sw.Hospital.HealthExaminationSystem.GuideListCollection;
using Sw.Hospital.HealthExaminationSystem.Helpers;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Market;
using Sw.Hospital.HealthExaminationSystem.NotificationBookReport;
using Sw.Hospital.HealthExaminationSystem.OAApproval;
using Sw.Hospital.HealthExaminationSystem.OccCusInfoOut;
using Sw.Hospital.HealthExaminationSystem.OutInspects;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.DailyReport;
using Sw.Hospital.HealthExaminationSystem.Permission;
using Sw.Hospital.HealthExaminationSystem.QuestionnaireMaintain;
using Sw.Hospital.HealthExaminationSystem.Scheduling;
using Sw.Hospital.HealthExaminationSystem.SchedulingSecondEdition;
using Sw.Hospital.HealthExaminationSystem.Statistics.AddStatistics;
using Sw.Hospital.HealthExaminationSystem.Statistics.Charge;
using Sw.Hospital.HealthExaminationSystem.Statistics.CusRegStatis;
using Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.Statistics.CustomerNumber;
using Sw.Hospital.HealthExaminationSystem.Statistics.Department;
using Sw.Hospital.HealthExaminationSystem.Statistics.DiseaseStatistics;
using Sw.Hospital.HealthExaminationSystem.Statistics.GeneralDoctor;
using Sw.Hospital.HealthExaminationSystem.Statistics.InspectionProject;
using Sw.Hospital.HealthExaminationSystem.Statistics.OccStatis;
using Sw.Hospital.HealthExaminationSystem.Statistics.ProjectStatistics;
using Sw.Hospital.HealthExaminationSystem.Statistics.Roster;
using Sw.Hospital.HealthExaminationSystem.Test;
using Sw.Hospital.HealthExaminationSystem.UserSettings.BarSetting;
using Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.UserSettings.CardSend;
using Sw.Hospital.HealthExaminationSystem.UserSettings.ClearData;
using Sw.Hospital.HealthExaminationSystem.UserSettings.CodeManager;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Critical;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Department;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Dictionary;
using Sw.Hospital.HealthExaminationSystem.UserSettings.InterfaceManagement;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Invoice;
using Sw.Hospital.HealthExaminationSystem.UserSettings.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.UserSettings.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.UserSettings.LogSet;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Market;
using Sw.Hospital.HealthExaminationSystem.UserSettings.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OAApprovalSet;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccAbnormalResult;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionSuspected;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionTarget;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionYear;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisRequisition;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccHarmFul;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccReview;
using Sw.Hospital.HealthExaminationSystem.UserSettings.PaymentMethod;
using Sw.Hospital.HealthExaminationSystem.UserSettings.PersonnelCategory;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Review;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest;
using Sw.Hospital.HealthExaminationSystem.UserSettings.SumHide;
using Sw.Hospital.HealthExaminationSystem.UserSettings.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Users;
using Sw.Hospital.HealthExaminationSystem.UserSettings.WorkType;

namespace HealthExaminationSystem.Win
{
    public partial class RibbonStartup : RibbonForm
    {
        private readonly Dictionary<BarItem, UserBaseForm> _userBaseForms;

        private readonly IPersonnelIndividuationConfigAppService _individuationConfigAppService;

        private string isLogin;
        public RibbonStartup()
        {
            InitializeComponent();

            ribbon.Minimized = true;

            _userBaseForms = new Dictionary<BarItem, UserBaseForm>();

            _individuationConfigAppService = new PersonnelIndividuationConfigAppService();
            //注册
            //  FormHelper.SetAuthorization(this, barButtonItemToolsSystemAuthorization, barStaticItemAuthorization);

            if (Variables.ISZYB != "1")
            {
              
                ribbonPage9.Visible = false;
                barButtonItemFrmInspectionTotalList.Visibility = BarItemVisibility.Never;
                barButtonItem99.Visibility = BarItemVisibility.Never;
                Occupationaldiseaseconsultation.Visibility = BarItemVisibility.Never;
            }
          
            // 设置全局异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            NavigationEvents.NavigationButtonClick += NavigationEvents_NavigationButtonClick;
        }

        /// <summary>
        /// 导航按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationEvents_NavigationButtonClick(object sender, NavigationEventArgs e)
        {
            if (e.Type == 0 || e.Name == "排期查询")
            {
                // 通过 Type 或 Name 确定要调用的窗体
                // 调用对应的窗体，请使用对应按钮事件
                // 例：
                barButtonItem9.PerformClick();
            }
            if (e.Type == 0 || e.Name == "单位管理")
            {
                barButtonItemFrmTjlClientInfoes.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团体预约")
            {
                barButtonItem8.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团体预约")
            {
                barButtonItem8.PerformClick();
            }
            if (e.Type == 0 || e.Name == "体检登记")
            {
                barButtonItemCustomerReg.PerformClick();
            }
            if (e.Type == 0 || e.Name == "个人报告")
            {
                barButtonItemPrintPreview.PerformClick();
            }
            if (e.Type == 0 || e.Name == "审批设置")
            {
                barButtonItem66.PerformClick();
            }
            if (e.Type == 0 || e.Name == "折扣审批")
            {
                barButtonItem65.PerformClick();
            }
            if (e.Type == 0 || e.Name == "个人收费")
            {
                barButtonItemPersonCharge.PerformClick();
            }
            if (e.Type == 0 || e.Name == "退费")
            {
                barButtonItemInvalidInvoice.PerformClick();
            }
            if (e.Type == 0 || e.Name == "发卡")
            {
                barButtonItem41.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团体收费")
            {
                barButtonItemClientCharge.PerformClick();
            }
            if (e.Type == 0 || e.Name == "日结")
            {
                barButtonItemDailyReport.PerformClick();
            }
            if (e.Type == 0 || e.Name == "报告领取")
            {
                barButtonItem36.PerformClick();
            }
            if (e.Type == 0 || e.Name == "随访设置")
            {
                barButtonItem21.PerformClick();
            }
            if (e.Type == 0 || e.Name == "随访记录")
            {
                barButtonItem82.PerformClick();
            }

            if (e.Type == 0 || e.Name == "危急值一览")
            {
                barButtonItem78.PerformClick();
            }
            if (e.Type == 0 || e.Name == "危急值报告")
            {
                barButtonItem79.PerformClick();
            }
            if (e.Type == 0 || e.Name == "投诉记录")
            {
                barButtonItem投诉管理.PerformClick();
            }
            if (e.Type == 0 || e.Name == "批量登记")
            {
                barButtonItemBatchCustomerReg.PerformClick();
            }
            if (e.Type == 0 || e.Name == "交表")
            {
                barButtonItemCrossTable.PerformClick();
            }
            if (e.Type == 0 || e.Name == "体检档案")
            {
                barButtonItem11.PerformClick();
            }
            if (e.Type == 0 || e.Name == "日志管理")
            {
                barButtonItem40.PerformClick();
            }
            if (e.Type == 0 || e.Name == "更新缓存")
            {
                barButtonItem52.PerformClick();
            }
            if (e.Type == 0 || e.Name == "重置状态")
            {
                barButtonItemResetCustomerCheck.PerformClick();
            }
            if (e.Type == 0 || e.Name == "职业健康问诊")
            {
                Occupationaldiseaseconsultation.PerformClick();
            }
            if (e.Type == 0 || e.Name == "医生站")
            {
                barButtonItemDoctorDesk.PerformClick();
            }
            if (e.Type == 0 || e.Name == "职业主检")
            {
                barButtonItemFrmInspectionTotalList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团报")
            {
                barButtonItemGroupReportList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "医生工作量")
            {
                barButtonItem14.PerformClick();
            }
            if (e.Type == 0 || e.Name == "危机值报告")
            {
                barButtonItem79.PerformClick();
            }
            if (e.Type == 0 || e.Name == "批量总检")
            {
                barButtonItemFrmInspectionTotalList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "通知单")
            {
                OccDisRequisition.PerformClick();
            }
            if (e.Type == 0 || e.Name == "主检工作量")
            {
                barButtonItem15.PerformClick();
            }
            if (e.Type == 0 || e.Name == "主检工作量")
            {
                barButtonItem15.PerformClick();
            }
            if (e.Type == 0 || e.Name == "初审")
            {
                barButtonItem86.PerformClick();
            }
            if (e.Type == 0 || e.Name == "主检")
            {
                barButtonItemFrmInspectionTotalNew.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团体报告")
            {
                barButtonItemGroupReportList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "团体报告")
            {
                barButtonItemGroupReportList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "阳性汇总")
            {
                barButtonItemGroupReportList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "预约排期")
            {
                barButtonItemGroupReportList.PerformClick();
            }
            if (e.Type == 0 || e.Name == "体检数据")
            {
                barButtonItem67.PerformClick();
            }
            if (e.Type == 0 || e.Name == "体检数据")
            {
                barButtonItem67.PerformClick();
            }
            if (e.Type == 0 || e.Name == "科室分成")
            {
                barButtonItemFinancialStatement.PerformClick();
            }

            //XtraMessageBox.Show(this, "尚未实现功能！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected UserViewDto CurrentUser => Variables.User;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var alertInfo = new AlertInfo("错误", e.Exception.Message)
            {
                Tag = e.Exception
            };
            alertControl.Show(this, alertInfo);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var alertInfo = new AlertInfo("错误", ((Exception)e.ExceptionObject).Message)
            {
                Tag = e.ExceptionObject
            };
            alertControl.Show(this, alertInfo);
        }

        private void SkinRibbonGalleryBarItem_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            Settings.Default.SkinStyle = e.Item.Tag.ToString();
            Settings.Default.Save();
            GC.Collect();
        }

        private void BarButtonItemStandardFont_ItemClick(object sender, ItemClickEventArgs e)
        {
            Settings.Default.EmSize = 10;
            Settings.Default.Save();
            GC.Collect();
            AppearanceObject.DefaultFont = new Font(AppearanceObject.DefaultFont.Name, 10);
            AppearanceObject.DefaultMenuFont = new Font(AppearanceObject.DefaultMenuFont.Name, 10);
        }

        private void BarButtonItemBigFont_ItemClick(object sender, ItemClickEventArgs e)
        {
            Settings.Default.EmSize = 12;
            Settings.Default.Save();
            GC.Collect();
            AppearanceObject.DefaultFont = new Font(AppearanceObject.DefaultFont.Name, 12);
            AppearanceObject.DefaultMenuFont = new Font(AppearanceObject.DefaultMenuFont.Name, 12);
        }

        private void BarButtonItemJumboFont_ItemClick(object sender, ItemClickEventArgs e)
        {
            Settings.Default.EmSize = 15;
            Settings.Default.Save();
            GC.Collect();
            AppearanceObject.DefaultFont = new Font(AppearanceObject.DefaultFont.Name, 15);
            AppearanceObject.DefaultMenuFont = new Font(AppearanceObject.DefaultMenuFont.Name, 15);
        }

        private void BarButtonItemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void BarButtonItemItemGroupList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ItemGroupList>(e);
        }

        private void BarButtonItemDepartmentList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DepartmentManager>(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            //注册
            FormHelper.SetAuthorization(this, barButtonItemToolsSystemAuthorization, barStaticItemAuthorization);

            if (Variables.ISZYB != "1")
            {
                ribbonPage9.Visible = false;
            }
            base.OnLoad(e);

            if (permissionManager.Enabled && Variables.PermissionEnabled)
            {
                //var formModuleAppService = new FormModuleAppService();
                var formModules = DefinedCacheHelper.GetFormModules();
                foreach (DictionaryEntry property in permissionManager.HashtableProperties)
                {
                    var provide = (ProvidedProperties)property.Value;
                    if (provide.Enabled)
                    {
                        if (!string.IsNullOrWhiteSpace(provide.Id))
                        {
                            try
                            {
                                //var result = formModuleAppService.GetByName(new NameDto { Name = provide.Id });
                                var result = formModules.Find(r => r.Name == provide.Id);
                                IPermission permission = null;
                                if (result == null)
                                {
                                    continue;
                                }
                                if (result.FormRoles == null)
                                { continue; }
                                foreach (var role in result.FormRoles)
                                    if (permission == null)
                                    {
                                        permission = new PrincipalPermission(CurrentUser.Id.ToString(), role.Name);
                                    }
                                    else
                                    {
                                        permission =
                                            permission.Union(new PrincipalPermission(CurrentUser.Id.ToString(),
                                                role.Name));
                                    }

                                if (permission != null)
                                {
                                    try
                                    {
                                        permission.Demand();
                                    }
                                    catch (SecurityException)
                                    {
                                        if (property.Key is Control control)
                                        {
                                            control.Enabled = false;
                                        }
                                        else if (property.Key is BarItem barItem)
                                        {
                                            barItem.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (property.Key is Control control)
                                    {
                                        control.Enabled = false;
                                    }
                                    else if (property.Key is BarItem barItem)
                                    {
                                        barItem.Enabled = false;
                                    }
                                }
                            }
                            catch (UserFriendlyException exception)
                            {
                                Console.WriteLine(exception);
                            }
                        }
                    }
                }
            }
        }

        private void BarButtonItemWorkTypeList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<WorkTypeList>(e);
        }

        private void BarButtonItemUserList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<UserList>(e);
        }

        private void BarButtonItemItemList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ItemList>(e);
        }

        private void BarButtonItemItemSuitList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ItemSuitList>(e);
        }

        private void BarButtonItemBasicDictionaryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<BasicDictionaryList>(e);
        }

        private void RibbonStartup_Load(object sender, EventArgs e)
        {
            try
            {
                DelPic();
                Application.DoEvents();

                barStaticItemVersion.Caption = $@"版本：{Application.ProductVersion}";
                var user = CurrentUser.Name;
                var apiUrl = ConfigurationManager.AppSettings.Get(Variables.ApiUrl);
                var uriBuilder = new UriBuilder(apiUrl);
                var ip = NetHelper.GetLocalIpByRemoteHost(uriBuilder.Host);
                barStaticItemCompany.Caption = Variables.Company;
                barStaticItemUser.Caption = user;
                barStaticItemAddress.Caption = ip;
                Application.DoEvents();
                barButtonItem起始页.PerformClick();
                //日志
                ICommonAppService _commonAppService = new CommonAppService();
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = CurrentUser.EmployeeNum;
                createOpLogDto.LogName = CurrentUser.Name;
                createOpLogDto.LogText = "登录系统";
                createOpLogDto.LogDetail = $@"版本：{Application.ProductVersion}";
                createOpLogDto.LogType = (int)LogsTypes.Other;
                _commonAppService.SaveOpLog(createOpLogDto);
                //barSubItem3.ImageIndex = 0;
                barStaticItem1.Caption= Variables.RegName+"-" + barStaticItem1.Caption;
            }
            catch (Exception ex)
            {
                throw;
            }
            // 临时试用版设置
            //var date = DateTime.Now;
            //var tempDate = new DateTime(2019, 3, 1);
            //if (date > tempDate)
            //{
            //    Text = $@"{Text}【试用版】";
            //    if (date > tempDate.AddDays(10))
            //    {
            //        XtraMessageBox.Show(this, "软件试用即将到期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        if (date > tempDate.AddDays(20))
            //        {
            //            timer.Start();
            //        }
            //    }
            //}
        }
        public async Task DelPic()
        {
            await Task.Run(() => clearPic());
        }
        private void clearPic()
        {
            string image = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\";
            if (Directory.Exists(image))
            {
                DirectoryInfo df = new DirectoryInfo(image);
                df.Delete(true);
            }
        }
        private void BarButtonItemDoctorDesk_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DoctorDesk>(e);
        }

        private void BarButtonItemBarCodeList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<BarCodeList>(e);
        }

        private void BarButtonItemSuggestList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<SuggestList>(e);
        }

        private void BarButtonItemDiagnosisTop_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DiagnosisTop>(e);
        }

        private void BarButtonItemCustomerReportHandover_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CustomerReportHandover>(e);
        }

        private void BarButtonItemCustomerReportReceive_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CustomerReportSend>(e);
        }

        private void BarButtonItemCustomerReportQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CustomerReportList>(e);
        }

        private void BarButtonItemFrmInspectionTotalList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //字典控制显示职业总检页面
            var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 116)?.Remarks;
            if (IsYC != null && IsYC == "Y")
            {
                ItemClickCommon<FrmInspectionTotal>(e);
            }
            else
            { ItemClickCommon<FrmInspectionTotalList>(e); }
        }

        private void BarButtonItemPayMethod_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PayMethod>(e);
        }

        private void BarButtonItemScheduleOfGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ScheduleOfGrid>(e);
        }

        private void BarButtonItemRosterSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<RosterSetting>(e);
        }

        private void BarButtonItemFrmInvoiceManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmInvoiceManagement>(e);
        }

        private void BarButtonItemFrmClientRegList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmClientRegList>(e);
        }

        private void BarButtonItemFrmTjlClientInfoes_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<FrmTjlClientInfoes>(e);
            ItemClickCommon<frmClientManage>(e);
        }

        private void BarButtonItemCustomerReg_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CustomerReg>(e);
        }

        private void BarButtonItemPersonCharge_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PersonCharge>(e);
        }

        private void BarButtonItemClientCharge_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ClientCharge>(e);
        }

        private void BarButtonItemCrossTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CrossTable>(e);
        }

        private void BarButtonItemTestTable1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<TestTable1>(e);
        }

        private void BarButtonItemDailyReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DailyReport>(e);
        }

        private void AlertControl_AlertClick(object sender, AlertClickEventArgs e)
        {
            var exception = e.Info.Tag as Exception;
            using (var frm = new Error(exception))
            {
                frm.ShowDialog();
            }
        }

        private void BarButtonItemException_ItemClick(object sender, ItemClickEventArgs e)
        {
            var appException = new ApplicationException("111111111111111111");
            throw new Exception("22222222222222222", appException);
        }

        private void BarButtonItemPictureTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PictureTest>(e);
        }

        private void BarButtonItemChecklistReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ChecklistReport>(e);
        }

        private void BarButtonItemPaymentMethod_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<Sw.Hospital.HealthExaminationSystem.Charge.PaymentMethod>(e);
        }

        private void BarButtonItemRoleManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<RoleManager>(e);
        }

        private void BarButtonItemPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PrintPreview>(e);
        }

        private void BarButtonItemRestart_ItemClick(object sender, ItemClickEventArgs e)
        {
            Program.Mutex.Close();
            Application.Restart();
        }

        private void BarButtonItemGroupReportList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<GroupReporList>(e);
            ItemClickCommon<frmClientReport>(e);

        }

        private void BarButtonItemAppointmentStatisticsByYears_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<AppointmentStatisticsByYears>(e);
        }

        private void BarButtonItemFrmClientRegCustomerList_ItemClick(object sender, ItemClickEventArgs e)
        {
            // FrmClientRegCustomerList
            var clientRegCustomerList = new FrmClientRegCustomerList
            {
                ClientRegId = new Guid("A50116CD-C76E-4944-AB9C-04915AD949A2")
            };

            clientRegCustomerList.Show();
        }

        private void BarButtonItemFrmZJYSGZL_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmZJYSGZL>(e);
        }

        private void BarButtonItemFrmJCXMTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmJCXMTJ>(e);
        }

        private void BarButtonItemFrmTCTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmTCTJ>(e);
        }

        private void BarButtonItemComposeTool_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ComposeTool>(e);
        }

        private void BarCheckItemMergePicture_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<MergePicture>(e);
        }

        private void BarButtonItemInvalidInvoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<InvalidInvoice>(e);
        }

        private void BarButtonItemCodeConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CodeConfig>(e);
        }

        private void BarButtonItemFrmKSGZL_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmKSGZLTJ>(e);
        }

        private void BarButtonItemUpdateCache_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (splashScreenManager == null)
                //{
                //    splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager();
                //}
                splashScreenManager.ShowWaitForm();
                DefinedCacheHelper.Refresh(Loading);
                //增加更新建议
                DefinedCacheHelper.UpdateSummarizeAdvices();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void Loading(string description)
        {
            if (splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.SetWaitFormDescription(description);
            }
        }

        private void BarButtonItemDictionarySetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DictionarySetting>(e);
        }

        private void barButtonItemFrmKSHBGZLTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmKSHBGZLTJ>(e);
        }

        private void barButtonItemFrmHZJCXMTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmHZJCXMTJ>(e);
        }

        private void ItemClickCommon<T>(ItemClickEventArgs e) where T : UserBaseForm, new()
        {
            e.Item.Enabled = false;
            Application.DoEvents();
            try
            {
                if (_userBaseForms.Keys.Contains(e.Item))
                {
                    _userBaseForms[e.Item].Activate();
                }
                else
                {
                    T frm;
                    if (typeof(T) == typeof(ContractManager) && e.Item.Tag != null)
                    {
                        frm = new ContractManager(true) { MdiParent = this } as T ?? new T { MdiParent = this };
                    }
                    else
                    {
                        frm = new T { MdiParent = this };
                    }

                    if (e.Item.Tag != null)
                    {
                        frm.Text = $@"{frm.Text}{e.Item.Tag}";
                    }

                    frm.Closed += (fs, fe) => { _userBaseForms.Remove(e.Item); };
                    frm.Show();
                    _userBaseForms.Add(e.Item, frm);
                }
            }
            finally
            {
                e.Item.Enabled = true;
                GC.Collect();
            }
        }

        private void barButtonItemGuideListCollectionSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<GuideListCollectionSetting>(e);
        }

        private void RibbonStartup_Shown(object sender, EventArgs e)
        {
            var user = _individuationConfigAppService.GetUserById(new EntityDto<long> { Id = CurrentUser.Id });
            if (user.IndividuationConfig != null)
            {
                if (user.IndividuationConfig.IsActive)
                {
                    if (user.IndividuationConfig.StartupMenuBars != null)
                    {
                        foreach (var bar in user.IndividuationConfig.StartupMenuBars)
                        {
                            var item = ribbon.Items.FirstOrDefault(r => r.Name == bar.BarButtonItemName);
                            item?.PerformClick();
                        }
                    }
                }
            }
            Activate();

            // barButtonItemUpdateCache.PerformClick();
            try
            {
                splashScreenManager.ShowWaitForm();
                DefinedCacheHelper.Refresh(Loading);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
            isLogin = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 121)?.Remarks;
            if (!string.IsNullOrEmpty(isLogin) && isLogin == "Y")
            {
                CreateLoginAsync();
            }
        }

      

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014)
            {
                return;
            }

            base.WndProc(ref m);
        }

        private void barButtonItemUpdateUserPwd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new UserPassUpdate())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Program.Mutex.Close();
                    Application.Restart();
                }
            }
        }

        private void barButtonItemBatchCustomerReg_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<BatchCustomerReg>(e);
        }

        private void barButtonItemFrmTTJZD_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmTTJZD>(e);
        }

        private void barButtonItemScheduleEditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<ScheduleEditor>(e);
        }

        private void barButtonItemFrmCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PersonnelCategoryList>(e);
        }

        private void barButtonItemScheduleQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ScheduleQuery>(e);
        }

        private void barButtonItemFrmTJRSTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmTJRSTJ>(e);
        }

        private void BarButtonItemInterfaceList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<InterfaceList>(e);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<HazardFactorsSetting>(e);
        }

        private void barButtonItemFrmKSRYTJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmKSRYTJ>(e);
        }

        private void barButtonItemFinancialStatement_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FinancialStatement>(e);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<TargetDisease>(e);
        }

        private void barButtonItemFrmClientStatistic_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmClientStatistic>(e);
        }

        private void barButtonItemPersonnelIndividuationConfigManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PersonnelIndividuationConfigManager>(e);
        }

        private void barButtonItemFrmInspectionTotalList1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmInspectionTotalList>(e);
        }

        private void barButtonItemDoctorDeskNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<DoctorDeskNew>(e);
        }

        private void barButtonItemResetCustomerCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<UpdateCustomerChecked>(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            XtraMessageBox.Show(this, "软件试用即将到期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmClientRegList>(e);
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ScheduleOfGrid>(e);
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ScheduleQuery>(e);
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<RosterSetting>(e);
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmKSGZLTJ>(e);
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmZJYSGZL>(e);
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<RosterSetting>(e);
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ItemProcExpressList>(e);
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ItemProcExpressList>(e);
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ReviewList>(e);
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<Disease>(e);
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<WorkTypeList>(e);
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<JobCategory>(e);
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmNotificationBookReport>(e);
        }

        private void bt_ItemClick(object sender, ItemClickEventArgs e)
        {

            ItemClickCommon<DictionariesList>(e);
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DailyList>(e);
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmChargeDetails>(e);
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<HazardFactorsSetting>(e);
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<TargetDisease>(e);
        }
        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccBasicDictionaryList>(e);
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmNotificationBookReport>(e);
        }

        private void 职业类别_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DictionariesList>(e);
        }

        private void barbntsumHide_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmSumHideList>(e);
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<AddStatiList>(e);
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<handle>(e);
        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CabinetSelection>(e);
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CusReporReceivet>(e);
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmClientLQ>(e);

        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (CurrentUser.Name == "admin")
            {
                ItemClickCommon<frmCheck>(e);
            }
            else
            {
                ItemClickCommon<frmClearData>(e);
            }

        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmLogs>(e);
        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmLogs>(e);
        }

        private void panelControl2_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl3_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl7_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl4_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl5_DragEnter(object sender, DragEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl6_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl8_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl9_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl10_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl11_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl12_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl13_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl14_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl15_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl16_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl17_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl18_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl19_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl20_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl21_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void panelControl2_Click(object sender, EventArgs e)
        {
            barButtonItem8.PerformClick();

        }


        private void panelControl7_Click(object sender, EventArgs e)
        {

        }

        private void panelControl11_Click(object sender, EventArgs e)
        {

        }

        private void panelControl13_Click(object sender, EventArgs e)
        {

        }

        private void panelControl3_Click(object sender, EventArgs e)
        {
            barButtonItemFrmTjlClientInfoes.PerformClick();
        }


        private void panelControl4_Click(object sender, EventArgs e)
        {
            barButtonItem9.PerformClick();
        }

        private void panelControl5_Click(object sender, EventArgs e)
        {
            barButtonItemBatchCustomerReg.PerformClick();
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccdiseaseMain>(e);
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccHazardFactorList>(e);
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccTargetDiseaseList>(e);
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccDisRequisitionList>(e);
        }

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccdieaseConsultation>(e);
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccConclusionStatistics>(e);
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccHarmFulList>(e);
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccAbnormalResultList>(e);
        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccReviewList>(e);
        }

        private void barButtonItem51_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccDayStaticList>(e);
        }

        private void barButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccConclusionYear>(e);
        }
        private void barButtonItem41_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccConclusionSuspected>(e);
        }

        private void barButtonItem45_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccConclusionTarget>(e);
        }

        private void barButtonItemToolsSystemAuthorization_ItemClick(object sender, ItemClickEventArgs e)
        {
            //注册
            //FormHelper.SetAuthorization(this, barButtonItemToolsSystemAuthorization, barStaticItemAuthorization);


            frmRegisEdit frmRegisEdit = new frmRegisEdit();
            frmRegisEdit.ShowDialog();
        }

        private void barButtonItem31_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<MemberShipCardList>(e);
        }

        private void barButtonItem41_ItemClick_2(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CardSendList>(e);
        }

        private void barButtonItem42_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CardSeach>(e);
        }

        private void barButtonItem46_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Program.Mutex.Close();
            Application.Restart();
        }

        private void barButtonItem47_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            using (var frm = new UserPassUpdate())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Program.Mutex.Close();
                    Application.Restart();
                }
            }
        }

        private void barButtonItem51_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem52_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                splashScreenManager.ShowWaitForm();
                DefinedCacheHelper.Refresh(Loading);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                splashScreenManager.ShowWaitForm();
                DefinedCacheHelper.Refresh(Loading);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (splashScreenManager.IsSplashFormVisible)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<IndividualityStatic>(e);
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DoctorDeparmentStatic>(e);
        }

        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<BallCheckStatic>(e);
        }

        private void barPrice_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<PriceSynchronize>(e);
        }

        private void barButtonItem59_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<DiseaseStatic>(e);
        }

        private void barButtonItem61_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmInspectionTotalNew>(e);
        }

        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 33)?.Remarks;
            if (IsYC != null && IsYC == "Y")
            {
                this.ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Hide;
                this.ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
                this.ribbon.Visible = false;
                this.ribbonStatusBar.Visible = false;
                ItemClickCommon<FrmInspectionTotalNew86>(e);
            }
            else
            {
                ItemClickCommon<FrmInspectionTotalNew>(e);
            }
        }

        private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmMakeCollect>(e);
        }

        private void barButtonItem64_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmRegList>(e);
        }

        private void barButtonItem65_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ZKApproval>(e);
        }

        private void barButtonItem66_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmOAZKSet>(e);
        }

        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ReSultStstis>(e);
        }

        private void RibbonStartup_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult TS = MessageBox.Show("退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (TS == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void barButtonItem62_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<GroupToCheck>(e);
        }

        private void barButtonItem67_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ReSultStstis>(e);
        }

        private void barButtonItem68_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmGroupBilling>(e);
        }

        private void barButtonItem69_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmMoneySummary>(e);
        }

        private void barButtonItem70_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmOccInfoOut>(e);
        }

        private void barButtonItem71_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ImportDataList>(e);

        }

        private void barButtonItem75_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OutCheckList>(e);
        }

        private void barButtonItem76_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<ImportResult>(e);
            ItemClickCommon<ImportDataList>(e);
        }

        private void barButtonItem77_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmGRPayment>(e);
        }

        private void barButtonItem78_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CriticalList>(e);
        }

        private void barButtonItem79_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CrissisCalSH>(e);
        }

        private void barButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmCcCountryOut>(e);

        }

        private void barButtonItem82_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmCusVisitManage>(e);
        }

        private void barButtonItemPerSum_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmBeforInspection>(e);
        }
        /// <summary>
        /// 起始页按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem起始页_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = MdiChildren.FirstOrDefault(r => r.GetType() == typeof(HomePage));
            if (frm == null)
            {
                frm = new HomePage();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void barButtonItem83_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmSumConflictList>(e);

        }

        private void barButtonItem84_ItemClick(object sender, ItemClickEventArgs e)
        {
            // QuestionnaireManager
            ItemClickCommon<QuestionnaireManager>(e);
        }

        private void barButtonItem86_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmBeforSum>(e);
            
           // ItemClickCommon<frmBeforInspection>(e);
        }

        private void barButtonItem投诉管理_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<Sw.Hospital.HealthExaminationSystem.Market.ComplaintManager>(e);
        }

        private void barButtonItem87_ItemClick(object sender, ItemClickEventArgs e)
        {

            ItemClickCommon<frmJKZCX>(e);
        }

        private void barButtonItem88_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FormBusCus>(e);

        }

        private void barButtonItem89_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmBusClient>(e);
        }

        private void barButtonItem90_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmforceSum>(e);
        }

        private void barButtonIteme抽血工作站_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<BloodWorkstation>(e);
        }

        /// <summary>
        /// 合同管理菜单按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemContractManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ContractManager>(e);
        }

        private void barButtonItemContractManagerAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ContractManager>(e);
        }

        private void barButtonItemContractCategoryManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<ContractCategoryManager>(e);
        }

        private void barButtonItem91_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmWebCharge>(e);
            
        }

        private void barButtonItem92_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmLoginDateChage>(e);
        }

        private void barButtonItem93_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<QuickCustomerReg>(e);
            
        }

        private void barButtonItem95_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<CheckGroup>(e);
        }

        private void barButtonItem96_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmBloodQue>(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(isLogin) && isLogin == "Y")
            {
                timer1.Stop();

                queLoginAsync();
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;

            }
        }
        #region 限制一个用户同一时间只能登录一个
        /// <summary>
        /// 异步保存登录记录
        /// </summary>
        /// <returns></returns>
        public async Task CreateLoginAsync()
        {
            var _Userservice = new UserAppService();
            LoginlistDto loginlistDto = new LoginlistDto();
            loginlistDto.UserId = CurrentUser.Id;

            var hasonlin = _Userservice.QueOnlinLogin(loginlistDto);
            bool isDel = false;
            if (hasonlin == true)
            {
                DialogResult dr = XtraMessageBox.Show("系统检测到此帐号已在其它电脑登录，点“确认”后进入系统，已登录电脑将自动失效退出。点“取消”则不退出", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                { isDel = true; }
            }

            LoginlistDto outq = new LoginlistDto();
            loginlistDto.hasDel = 1;
            await Task.Run(() =>
            outq = _Userservice.SaveLogin(loginlistDto)
            );
            Variables.LoginKey = outq.Id;
            //if (outq.hasDel==1)
            //{
            //    XtraMessageBox.Show("系统检测到此帐号已在其它电脑登录，点“确认”后进入系统，已登录电脑将自动失效退出。");
            //}
        }
        /// <summary>
        /// 异步注销登录 
        /// </summary>
        /// <returns></returns>
        public async Task deletLoginAsync()
        {
            var _Userservice = new UserAppService();
            LoginlistDto loginlistDto = new LoginlistDto();
            loginlistDto.UserId = CurrentUser.Id;
            await Task.Run(() => _Userservice.DelLogin(loginlistDto));

        }
        /// <summary>
        /// 查询登录 
        /// </summary>
        /// <returns></returns>
        public async Task queLoginAsync()
        {
            if (Variables.LoginKey.HasValue)
            {
                var _Userservice = new UserAppService();
                LoginlistDto loginlistDto = new LoginlistDto();
                loginlistDto.Id = Variables.LoginKey.Value;
                var isOw = _Userservice.QueLogin(loginlistDto);
                if (isOw == false)
                {
                    timer1.Stop();
                    MessageBox.Show("该账号已在其他地方登录，您已下线");
                    this.Dispose();
                    
                }
            }

        }
        #endregion

        private void RibbonStartup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!string.IsNullOrEmpty(isLogin) && isLogin == "Y")
            {
                deletLoginAsync();
            }
        }

        private void barButtonItem97_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmOccYearsStatis>(e);
            
        }

        private void barButtonItem98_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<OccRadiationList>(e);
        }

        private void barButtonItem99_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemClickCommon<frmOccQuePic>(e);
            //ItemClickCommon<frmOccMQuePic>(e);

            //字典控制显示职业总检页面
            var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 171)?.Remarks;
            if (IsYC != null && IsYC == "Y")
            {
                ItemClickCommon<frmOccQuePic>(e);
            }
            else
            { ItemClickCommon<frmOccMQuePic>(e); }
        }

        private void barserch_ItemClick(object sender, ItemClickEventArgs e)
        {
             
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\自定义查询";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = path + "\\SelfSearch.exe";
            if (File.Exists(KHMsg.StartInfo.FileName))
            {

                KHMsg.Start();
            }

            
        }

        private void barButtonItem100_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmSumHB>(e);

        }
        private void barButtonItem101_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<frmIllState>(e);

        }

        private void barButtonItem102_ItemClick(object sender, ItemClickEventArgs e)
        {
            var GrdPath = Sw.Hospital.HealthExaminationSystem.Common.Helpers.GridppHelper.GetTemplate("芜湖五院铁路高温模板.rdlx");
            string args = GrdPath + "|" + "铁路高温模板" ;
            var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\报告查询";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = reportpath + "\\SearchSelf.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();
        }

        private void barButtonItem103_ItemClick(object sender, ItemClickEventArgs e)
        {
            var GrdPath = Sw.Hospital.HealthExaminationSystem.Common.Helpers.GridppHelper.GetTemplate("芜湖五院铁路数据模板.rdlx");
            string args = GrdPath + "|" + "铁路数据模板";
            var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\报告查询";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = reportpath + "\\SearchSelf.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();
        }

        private void barButtonItem104_ItemClick(object sender, ItemClickEventArgs e)
        {
            var GrdPath = Sw.Hospital.HealthExaminationSystem.Common.Helpers.GridppHelper.GetTemplate("芜湖五院铁路重大阳性体征模板.rdlx");
            string args = GrdPath + "|" + "铁路重大阳性模板";
            var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\报告查询";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = reportpath + "\\SearchSelf.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();
        }
        private void RibbonStartup_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = e.KeyCode == Keys.F11;
            if (flag)
            {
                bool visible = this.ribbon.Visible;
                if (visible)
                {
                    this.ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Hide;
                    this.ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
                    this.ribbon.Visible = false;
                    this.ribbonStatusBar.Visible = false;
                }
                else
                {
                    this.ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show;
                    this.ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Default;
                    this.ribbon.Visible = true;
                    this.ribbonStatusBar.Visible = true;
                }
            }
        }

        private void barButtonItem105_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FormAppointmentCalendar>(e);
        }

        private void barButtonItem106_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<XtraFormParentCompanyRegisterReportPrinter>(e);
            
        }

        private void barButtonItem107_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmInspectionTotalListAUTO>(e);
            
        }

        private void barButtonItem108_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickCommon<FrmTJRSTJ>(e);
        }
    }
}