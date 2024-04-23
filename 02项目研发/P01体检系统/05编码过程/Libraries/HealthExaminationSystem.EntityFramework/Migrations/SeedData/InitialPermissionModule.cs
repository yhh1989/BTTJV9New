using System;
using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 初始化权限模型123
    /// </summary>
    public class InitialPermissionModule
    {
        private readonly MyProjectDbContext _context;

        public static List<FormModule> InitialModules { get; }

        public static List<FormModule> DeleteModules { get; }

        public InitialPermissionModule(MyProjectDbContext context)
        {
            _context = context;
        }

        static InitialPermissionModule()
        {
            InitialModules = new List<FormModule>
            {

                new FormModule
                {
                    Id = new Guid("20936C3B-5DC8-40C1-8A12-5C8DA1A7D21B"),
                    Name = "Main.Market.Company.barButtonItemFrmTjlClientInfoes",
                    Nickname = "主窗体.销售管理.单位.建档",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.单位.建档（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("3644160E-8F75-4C9D-AF46-90BACD856120"),
                    Name = "Main.Market.Team.barButtonItemComposeTool",
                    Nickname = "主窗体.销售管理.团体.组单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.团体.组单（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("15F8C623-1F04-47E1-A6C4-5D6E0BE24B53"),
                    Name = "Main.Market.Statistics.barButtonItemAppointmentStatisticsByYears",
                    Nickname = "主窗体.销售管理.统计.历年预约统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.统计.历年预约统计（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("401C25B1-D387-477E-A776-AA1B498341FA"),
                    Name = "Main.FrontDesk.Examination.barButtonItemCustomerReg",
                    Nickname = "主窗体.前台管理.体检.登记",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2018年12月5日）
                 new FormModule
                {
                    Id = new Guid("D779DA62-2D69-472C-B9AD-578968B3BE93"),
                    Name = "CustomerReg.butCharge",
                    Nickname = "主窗体.前台管理.体检.登记.收费按钮",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.赠检（已校验 2018年12月5日）
                     new FormModule
                {
                    Id = new Guid("6FA99B68-AABB-44ED-80D7-DD222BA54DDB"),
                    Name = "FrmSeleteItemGroup.btnZJPyment",
                    Nickname = "主窗体.前台管理.体检.登记.选择组合.赠检",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2018年12月5日）
                 new FormModule
                {
                    Id = new Guid("9FD8908A-9FC9-47C9-8297-02328D74495C"),
                    Name = "CustomerReg.btnSave",
                    Nickname = "主窗体.前台管理.体检.登记.保存按钮",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },
                           new FormModule
                {
                    Id = new Guid("C57AB07F-8AED-433E-A3CD-13DBC91C2829"),
                    Name = "CustomerReg.butPrinitDYD",
                    Nickname = "主窗体.前台管理.体检.登记.打印导引单",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2018年12月5日）
                                             new FormModule
                {
                    Id = new Guid("C8EE8339-6BF9-4286-8069-5CAAC80D758C"),
                    Name = "CustomerReg.butPrinitTBM",
                    Nickname = "主窗体.前台管理.体检.登记.打印条形码",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2018年12月5日）
                                                           new FormModule
                {
                    Id = new Guid("7232BE96-37C9-4ED1-8288-6D2F4B35E388"),
                    Name = "CustomerReg.butPrinitDYDTM",
                    Nickname = "主窗体.前台管理.体检.登记.打印条码/导引单",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("EE6200DD-0296-45B8-9349-5AC76B80C446"),
                    Name = "Main.FrontDesk.Examination.barButtonItemBatchCustomerReg",
                    Nickname = "主窗体.前台管理.体检.批量登记",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.团体.预约（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("C7E59AF9-3C43-49DF-8510-C293B9AFD08B"),
                    Name = "Main.FrontDesk.Examination.barButtonItemFrmClientRegList",
                    Nickname = "主窗体.前台管理.体检.团体导引单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.团体.预约（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("695B1D3D-5D19-436F-870E-862B46211AA1"),
                    Name = "Main.FrontDesk.Examination.barButtonItemCrossTable",
                    Nickname = "主窗体.前台管理.体检.交表",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.交表（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("72E060A4-F588-4104-ABD9-D7583D985711"),
                    Name = "Main.FrontDesk.Examination.barButtonItemGuideListCollectionSetting",
                    Nickname = "主窗体.前台管理.体检.导引单领取",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.导引单领取（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("F3DB97D2-865B-4791-B511-4BA3D958E081"),
                    Name = "Main.FrontDesk.Appointment.barButtonItemScheduleOfGrid",
                    Nickname = "主窗体.前台管理.预约.排期",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.预约.排期（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("7C976DBC-0EBD-4D59-A56B-9D9A1670F490"),
                    Name = "Main.FrontDesk.Appointment.barButtonItemScheduleQuery",
                    Nickname = "主窗体.前台管理.预约.排期查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.预约.排期查询（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("8215496E-900E-4B33-8060-E8DFEDC4A1A0"),
                    Name = "Main.FrontDesk.Statistics.barButtonItemRosterSetting",
                    Nickname = "主窗体.前台管理.统计.体检档案",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.统计.花名册（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("70E60812-827B-4C21-A54B-B5DE6BA6F25E"),
                    Name = "Main.Doctor.DoctorWorkstation.barButtonItemDoctorDesk",
                    Nickname = "主窗体.医生管理.医生工作站.工作站",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.医生工作站.工作站（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("23974C40-1FF9-4EF0-8A19-01875CC2B531"),
                    Name = "Main.Doctor.Check.barButtonItemFrmInspectionTotalList",
                    Nickname = "主窗体.医生管理.总检.职业总检",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核（已校验 2018年12月5日）
                  new FormModule
                {
                    Id = new Guid("F769EC22-3023-4EFA-A7D3-7DF9DDA53A23"),
                    Name = "Main.Doctor.Check.barButtonItemFrmInspectionTotaAuto",
                    Nickname = "主窗体.医生管理.总检.自动总检",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核（已校验 2018年12月5日）
                                new FormModule
                {
                    Id = new Guid("BBF76233-FC7F-40CB-BBA9-B7E214AFD2FE"),
                    Name = "Main.Doctor.Check.barButtonItemFrmInspectionTotalNew",
                    Nickname = "主窗体.医生管理.总检.新总检",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核（已校验 2018年12月5日）
                
                //new FormModule
                //{
                //    Id = new Guid("{3EF19EFF-4623-4446-8CA5-F2F80ABF2DFB}"),
                //    Name = "Main.Doctor.Check.barButtonItemFrmInspectionTotalList1",
                //    Nickname = "主窗体.医生管理.总检.审核1",
                //    TypeName = Variables.MenuType,
                //    CreationTime = DateTime.Now,
                //    CreatorUserId = 0
                //},// 主窗体.医生管理.总检.审核1（已校验 2019年1月21日）
                //DoctorDesk.cglueJianChaYiSheng
                 new FormModule
                {
                    Id = new Guid("{FFBFC0EC-C764-47B5-BFF9-365EC4C76E13}"),
                    Name = "FrmInspectionTotal.simpleButtonShenHe",
                    Nickname = "主窗体.医生管理.总检审核.审核按钮",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.工作站.检查医生（已校验 2019年1月21日）
                   new FormModule
                {
                    Id = new Guid("{286691B1-AD50-4C91-B98E-0B96852E24E8}"),
                    Name = "FrmInspectionTotal.simpleButtonUpdateSum",
                    Nickname = "主窗体.医生管理.总检审核.更新汇总",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.工作站.检查医生（已校验 2019年1月21日）
                 
                   new FormModule
                {
                    Id = new Guid("{AB1BE93A-B203-40E6-8D28-50503F4E1D28}"),
                    Name = "FrmInspectionTotal.butPriintReport",
                    Nickname = "主窗体.医生管理.总检审核.打印报告",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.按钮
                      new FormModule
                {
                    Id = new Guid("{F830A62F-1A59-40AE-9A70-1CE6F679D00A}"),
                    Name = "FrmInspectionTotal.butReport",
                    Nickname = "主窗体.医生管理.总检审核.报告预览",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.按钮
                          new FormModule
                {
                    Id = new Guid("{B4273F62-1B38-4A53-BAB7-3B1E7963605D}"),
                    Name = "FrmInspectionTotal.butReCeive",
                    Nickname = "主窗体.医生管理.总检审核.复查",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.按钮
                                     new FormModule
                {
                    Id = new Guid("{F649D382-0B12-451B-91EB-F95B5F03BCED}"),
                    Name = "FrmInspectionTotal.butClearSum",
                    Nickname = "主窗体.医生管理.总检审核.清除总检",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.按钮
                                                         new FormModule
                {
                    Id = new Guid("{FA66C3F9-092B-4B47-9FB3-C6D13B31854A}"),
                    Name = "FrmInspectionTotal.butSaveSum",
                    Nickname = "主窗体.医生管理.总检审核.保存初审",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.按钮
                              new FormModule
                {
                    Id = new Guid("{BDF8F6FE-4F91-4B33-BC62-1A3CD19ECD9D}"),
                    Name = "FrmInspectionTotal.butMakeSum",
                    Nickname = "主窗体.医生管理.总检审核.汇总初审",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                              },// 主窗体.医生管理.总检.按钮
                 new FormModule
                {
                    Id = new Guid("{3F1A87F9-02CD-4E24-8DE4-A67681F3B35A}"),
                    Name = "DoctorDesk.cglueJianChaYiSheng",
                    Nickname = "主窗体.医生管理.医生工作站.工作站.检查医生",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.工作站.检查医生（已校验 2019年1月21日）
                     new FormModule
                {
                    Id = new Guid("{1A94C730-3B6D-4CC9-9979-C73D533F6132}"),
                    Name = "DoctorDesk.cglueShenHeYiSheng",
                    Nickname = "主窗体.医生管理.医生工作站.工作站.审核医生",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.工作站.审核医生（已校验 2019年1月21日）
                new FormModule
                {
                    Id = new Guid("7143B1A3-9709-4C82-BDC3-090BDC1BD9C9"),
                    Name = "Main.FinancialAffairs.Charge.barButtonItemPersonCharge",
                    Nickname = "主窗体.财务管理.收费.个人收费",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.收费.个人收费（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("3B09F70B-37A9-4F60-AFD9-433139D3EA39"),
                    Name = "Main.FinancialAffairs.Charge.barButtonItemClientCharge",
                    Nickname = "主窗体.财务管理.收费.团体结算",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.收费.团体结算（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("1595028F-7809-49DB-A88A-D02AAD6183A7"),
                    Name = "Main.FinancialAffairs.Invoice.barButtonItemFrmInvoiceManagement",
                    Nickname = "主窗体.财务管理.发票.发票管理",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.发票.发票管理（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("7EA138A4-ED13-465D-90B4-88FA7CE9BCCA"),
                    Name = "Main.FinancialAffairs.Invoice.barButtonItemInvalidInvoice",
                    Nickname = "主窗体.财务管理.发票.退费",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.发票.退费（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("AE527B1D-82F4-4DEE-8E2E-827E301C69D6"),
                    Name = "Main.FinancialAffairs.Print.barButtonItemChecklistReport",
                    Nickname = "主窗体.财务管理.打印.体检清单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.打印.体检清单（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("06DA5D15-9AFC-4C0A-A573-C3C9A304A4BC"),
                    Name = "Main.FinancialAffairs.Statistics.barButtonItemDailyReport",
                    Nickname = "主窗体.财务管理.统计.日报表",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.统计.日报表（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("D30BCF2B-A365-462B-A224-A396F58E8B36"),
                    Name = "Main.FinancialAffairs.Statistics.barButtonItemFrmTTJZD",
                    Nickname = "主窗体.财务管理.统计.团体结账单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.统计.团体结账单（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("6FCFBC00-7CCD-4140-852B-F940D705BBFB"),
                    Name = "Main.Report.Report.barButtonItemGroupReportList",
                    Nickname = "主窗体.报告管理.报告.团体报告",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.团体报告（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("66F0A283-B6BD-4D06-ADEF-6FC42B1C141C"),
                    Name = "Main.Report.Report.barButtonItemCustomerReportHandover",
                    Nickname = "主窗体.报告管理.报告.报告单交接",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单交接（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("D108A9CF-268D-4FEC-9357-A2E7E49B0FB4"),
                    Name = "Main.Report.Report.barButtonItemCustomerReportReceive",
                    Nickname = "主窗体.报告管理.报告.报告单领取",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单领取（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("80504481-6667-4D16-9AD5-2FB0B11A4C75"),
                    Name = "Main.Report.Report.barButtonItemCustomerReportQuery",
                    Nickname = "主窗体.报告管理.报告.报告单查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单查询（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("499F47C1-F471-4001-A2A6-2370258C047C"),
                    Name = "Main.Report.Print.barButtonItemPrintPreview",
                    Nickname = "主窗体.报告管理.打印.打印预览",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.打印.打印预览（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("470D3325-9F61-410F-92C1-89923837D8B1"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmZJYSGZL",
                    Nickname = "主窗体.统计查询.统计.总检医生工作量",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.总检医生工作量（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("388C1181-39C3-4D80-8F69-B1638BAF379A"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmJCXMTJ",
                    Nickname = "主窗体.统计查询.统计.检查项目",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.检查项目（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("68108082-37A1-4F0E-9647-DB5872A866AB"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmTCTJ",
                    Nickname = "主窗体.统计查询.统计.套餐统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.套餐统计（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("05B6FF27-265E-49E0-AD47-1E7CD03E1F83"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmKSGZL",
                    Nickname = "主窗体.统计查询.统计.科室工作量统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.科室工作量统计（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("5F401D39-DDD8-4940-B932-6EB0D815AAF8"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmKSHBGZLTJ",
                    Nickname = "主窗体.统计查询.统计.科室环比工作量",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.科室环比工作量（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("65D7CD50-D8D3-46BB-88ED-E6FF03736680"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmHZJCXMTJ",
                    Nickname = "主窗体.统计查询.统计.体检统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.体检统计（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("D4C139FD-21B6-412E-9AA6-A74739ECD864"),
                    Name = "Main.Statistics.Statistics.barButtonItemFrmTJRSTJ",
                    Nickname = "主窗体.统计查询.统计.人数统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.人数统计（已校验 2018年12月5日）
                //Main.Statistics.Statistics.Disease
                 new FormModule
                {
                    Id = new Guid("0A360481-4D5D-43F8-98F2-3A341245E512"),
                    Name = "Main.Statistics.Statistics.Disease",
                    Nickname = "主窗体.统计查询.统计.疾病统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.人数统计（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("A449E2F9-3BB9-4A74-BB6D-207A65F614BE"),
                    Name = "Main.System.Coding.barButtonItemDepartmentList",
                    Nickname = "主窗体.系统.编码.科室",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.科室（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("7866E551-82D3-47A0-837B-A9663D03CD98"),
                    Name = "Main.System.Coding.barButtonItemItemList",
                    Nickname = "主窗体.系统.编码.项目",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.项目（已校验 2018年12月5日）
                //Main.System.Coding.ItemProcExpressList
                  new FormModule
                {
                    Id = new Guid("807AC3F3-3353-42B5-89E5-A3861193374F"),
                    Name = "Main.System.Coding.ItemProcExpressList",
                    Nickname = "主窗体.系统.编码.计算项目",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.项目（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("16232BBC-2C3A-4696-B867-9437C2700E1B"),
                    Name = "Main.System.Coding.barButtonItemBarCodeList",
                    Nickname = "主窗体.系统.编码.条码",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.条码（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("3E7FE050-271A-4CB4-9AC7-52911ED46790"),
                    Name = "Main.System.Coding.barButtonItemItemSuitList",
                    Nickname = "主窗体.系统.编码.套餐",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.套餐（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("F0D2C9BB-B3A5-48AB-89C6-C0CEDFEA89D1"),
                    Name = "Main.System.Coding.barButtonItemUserList",
                    Nickname = "主窗体.系统.编码.用户",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.用户（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("42CAF4FA-B807-439D-9CD0-2E7DB983E8DF"),
                    Name = "Main.System.Coding.barButtonItemBasicDictionaryList",
                    Nickname = "主窗体.系统.编码.字典",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.字典（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("2E606A30-7CF3-47F1-9DCC-EBDF040DBD19"),
                    Name = "Main.System.Config.barButtonItemItemGroupList",
                    Nickname = "主窗体.系统.配置.组合",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.组合（已校验 2018年12月5日）
                //Main.System.Config.barButtonItemInterfaceList
                new FormModule
                {
                    Id = new Guid("2FF0A9DE-330F-4358-8A73-C8BDF276D974"),
                    Name = "Main.System.Config.barButtonItemInterfaceList",
                    Nickname = "主窗体.系统.配置.接口设置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.接口设置（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("074A1679-AF8A-4FF3-9BB7-CCCDF88684B7"),
                    Name = "Main.System.Config.barButtonItemSuggestList",
                    Nickname = "主窗体.医生管理.建议设置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.建议（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("A82401EA-D04B-4B19-90AF-8DD04416AA1F"),
                    Name = "Main.System.Config.barButtonItemWorkTypeList",
                    Nickname = "主窗体.系统.配置.工种",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.工种（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("7079E7B3-D15F-4339-8705-03F0ECCD455F"),
                    Name = "Main.System.Config.barButtonItemPayMethod",
                    Nickname = "主窗体.系统.配置.支付方式",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.支付方式（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("61194A12-2D33-410C-8D19-23491FBD09DA"),
                    Name = "Main.System.Config.barButtonItemRoleManager",
                    Nickname = "主窗体.系统.配置.权限",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.权限（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("3F53A249-DAD5-4B0B-AA6A-AE38D9C3A883"),
                    Name = "Main.System.Config.barButtonItemDiagnosisTop",
                    Nickname = "主窗体.系统.配置.复合判断",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.复合判断（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("97744976-8BC1-40F8-B774-EA1A73FC733E"),
                    Name = "Main.System.Config.barButtonItemDictionarySetting",
                    Nickname = "主窗体.系统.配置.项目字典",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.项目字典（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("FB555C0B-BA39-410D-A35F-94FC107896C7"),
                    Name = "Main.System.Other.barButtonItemCodeConfig",
                    Nickname = "主窗体.系统.其它.编码配置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.编码配置（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("C64C1633-B0AC-4F92-A664-23C0BEAF9FCD"),
                    Name = "Main.System.Other.barButtonItemUpdateCache",
                    Nickname = "主窗体.系统.其它.更新缓存",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.更新缓存（已校验 2018年12月5日）
                new FormModule
                {
                    Id = new Guid("E101B86D-664C-4DEC-9811-C68C068BE923"),
                    Name = "Main.System.Other.barButtonItemFrmCategory",
                    Nickname = "主窗体.系统.其它.人员类别",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.人员类别（已校验 2018年12月5日）
                 //Main.System.Config.barButtonItemPersonnelIndividuationConfigManager
                 new FormModule
                 {
                    Id = new Guid("8146B185-F60F-4B1D-BDC6-D9431E91A07D"),
                    Name = "Main.System.Config.barButtonItemPersonnelIndividuationConfigManager",
                    Nickname = "主窗体.系统.其它.用户配置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.人员类别（已校验 2018年12月5日）
                       //Main.System.Config.ReviewList
                  new FormModule
                  {
                    Id = new Guid("C1294CE7-CB99-4A2F-821E-0002E944B769"),
                    Name = "Main.System.Config.ReviewList",
                    Nickname = "主窗体.系统.其它.复查设置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                  },
                   new FormModule
                  {
                    Id = new Guid("72EBF69E-FA01-49B3-A8FC-0C66E721B4E4"),
                    Name = "Main.System.Coding.reState",
                    Nickname = "主窗体.系统.其它.批量消审",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                  },
                       new FormModule
                  {
                    Id = new Guid("7E562F4F-1C99-49BF-AD62-5207B7DF1ADC"),
                    Name = "Main.System.Coding.HideSam",
                    Nickname = "主窗体.系统.其它.隐藏诊断",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                  },
                             new FormModule
                             {
                    Id = new Guid("C8606239-6FD7-4FCE-9FDF-77000BD379A7"),
                    Name = "Main.System.Coding.Logs",
                    Nickname = "主窗体.系统.其它.日志管理",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
                               new FormModule
                             {
                    Id = new Guid("76DE7CB7-3417-46F1-849B-0C051E523193"),
                    Name = "Main.System.Coding.ClearData",
                    Nickname = "主窗体.系统.其它.清除数据",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
                                   new FormModule
                             {
                    Id = new Guid("99CA5394-010B-426B-A3A6-E619E92D1C7F"),
                    Name = "Main.Report.gzcx",
                    Nickname = "主窗体.报告管理.报告.柜子查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
                                          new FormModule
                             {
                    Id = new Guid("944F6EBA-342F-4DAF-BD2D-57C56DCE3935"),
                    Name = "Main.Report.Print.gblq",
                    Nickname = "主窗体.报告管理.报告.个报领取",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
            new FormModule
                             {
                    Id = new Guid("6D30D21D-2CFF-4613-80DC-42AEBEA6C688"),
                    Name = "Main.Report.Print.tblq",
                    Nickname = "主窗体.报告管理.报告.团报领取",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
             new FormModule
                             {
                    Id = new Guid("EF6FCD3E-D872-4563-BF0B-BEDB3C68CADB"),
                    Name = "Main.Customer.addSearech",
                    Nickname = "主窗体.客服管理.加项统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                             },
              new FormModule
                   {
                    Id = new Guid("1320D514-F834-46DA-A443-0BC339FD1EE4"),
                    Name = "Main.Customer.HillSearch",
                    Nickname = "主窗体.客服管理.危机值统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
                 new FormModule
                   {
                    Id = new Guid("808C0B6D-C678-4EC5-8B48-4513C1BA451E"),
                    Name = "Main.OccupationalDisease.Occupationaldiseasecategory",
                    Nickname = "主窗体.职业健康.职业健康小类",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("74F25DBC-F9DF-4ED3-9AA1-5D2B0A80F7A9"),
                    Name = "Main.OccupationalDisease.Occupationaldiseaseconsultation",
                    Nickname = "主窗体.医生管理.职业健康问诊",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("FA44D8AB-D3F8-412A-BDF4-7CB35F9C669D"),
                    Name = "Main.OccupationalDisease.OccupationaldiseaseConlution",
                    Nickname = "主窗体.职业健康.体检结论统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              //new FormModule
              //     {
              //      Id = new Guid("63A2C6C6-EF0F-496B-9762-57443139B36F"),
              //      Name = "Main.OccupationalDisease.OccupationaldiseaseConlution",
              //      Nickname = "主窗体.职业健康.体检结论统计",
              //      TypeName = Variables.MenuType,
              //      CreationTime = DateTime.Now,
              //      CreatorUserId = 0
              //},
              new FormModule
                   {
                    Id = new Guid("A61F2E96-E370-42FD-B87D-85D1DF1FA8A3"),
                    Name = "Main.OccupationalDisease.OccupationaldiseaseYear",
                    Nickname = "主窗体.职业健康.年度统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("300C5B23-9B20-495E-A800-4CDCE4EB231C"),
                    Name = "Main.OccupationalDisease.OccupationaldiseaseSuspected",
                    Nickname = "主窗体.职业健康.疑似职业健康统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("F9E7B22F-B209-44A7-9E21-C0792FFE234D"),
                    Name = "Main.OccupationalDisease.OccConclusionTarget",
                    Nickname = "主窗体.职业健康.危害因素-目标疾病统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("F57F3D8C-62B5-4C73-BF0C-3AF9FD5FB07D"),
                    Name = "Main.OccupationalDisease.OccBasicDictionary",
                    Nickname = "主窗体.职业健康.职业字典",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("A788194D-BA17-416D-B527-1C38174159C7"),
                    Name = "Main.OccupationalDisease.OccHazardFactor",
                    Nickname = "主窗体.职业健康.危害因素小类",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("89368869-118D-4048-8B67-7D7A52193FFB"),
                    Name = "Main.OccupationalDisease.OccTargetDisease",
                    Nickname = "主窗体.职业健康.目标疾病",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("ECCFD283-B466-4599-9C81-E833BC73B4E4"),
                    Name = "Main.OccupationalDisease.OccDisRequisition",
                    Nickname = "主窗体.职业健康.职业健康通知单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              }
              ,
              new FormModule
                   {
                    Id = new Guid("6A11C930-5F0C-42C5-A805-4792CB2EEA7E"),
                    Name = "Main.OccupationalDisease.OccHarmFul",
                    Nickname = "主窗体.职业健康.有害因素统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              }
              ,
              new FormModule
                   {
                    Id = new Guid("F7839879-4D40-41AB-8DAE-5989426D0849"),
                    Name = "Main.OccupationalDisease.OccAbnormalResult",
                    Nickname = "主窗体.职业健康.异常结果检出统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("C7A00CF2-3DE1-4F87-9FE3-F8039D8B6371"),
                    Name = "Main.OccupationalDisease.OccReview",
                    Nickname = "主窗体.职业健康.复查项目统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
              new FormModule
                   {
                    Id = new Guid("DE39F689-8C4F-4F21-AB5E-FF494AF968E7"),
                    Name = "Main.OccupationalDisease.OccDayStatic",
                    Nickname = "主窗体.职业健康.月度统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
              },
               new FormModule
                   {
                    Id = new Guid("94FF41EB-0D53-4ACA-8C61-BE3598D9CCC3"),
                    Name = "Main.System.Coding.Regist",
                    Nickname = "主窗体.职业健康.程序授权",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
               new FormModule
                   {
                    Id = new Guid("AAEF541C-EF28-41F5-905D-309B948844DD"),
                    Name = "Main.Market.Team.ClientReg",
                    Nickname = "主窗体.体检预约.团体预约",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                new FormModule
                   {
                    Id = new Guid("76898CC5-DC24-4847-B3C3-7EBA048B6EAC"),
                    Name = "Main.Market.Team.RegRL",
                    Nickname = "主窗体.体检预约.预约日历",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                new FormModule
                   {
                    Id = new Guid("{4BD8FB4E-5C42-40F1-8598-58BF97FFD177}"),
                    Name = "Main.Market.Team.PQGL",
                    Nickname = "主窗体.体检预约.排期管理",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                   new FormModule
                   {
                    Id = new Guid("{87E6299A-616A-4887-AF57-3622777F7000}"),
                    Name = "Main.FrontDesk.Statistics.kstjtj",
                    Nickname = "主窗体.体检查询.科室体检人员统计",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                    new FormModule
                   {
                    Id = new Guid("{E3F6363D-C9A3-440C-8B24-5F24C699FF97}"),
                    Name = "Main.FrontDesk.Statistics.ksgzl",
                    Nickname = "主窗体.体检查询.科室工作量",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                         new FormModule
                   {
                    Id = new Guid("{BF194202-3E52-4AB4-B033-48AAF691469B}"),
                    Name = "Main.FrontDesk.Statistics.ZJGZL",
                    Nickname = "主窗体.体检查询.总检工作量",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
               },
                                 new FormModule
                   {
                    Id = new Guid("{C61EDCD4-A587-468F-812E-F973766D939E}"),
                    Name = "Main.FinancialAffairs.Statistics.KSFC",
                    Nickname = "主窗体.财务管理.科室分成",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                 },
                                              new FormModule
                   {
                    Id = new Guid("{5096E738-A040-4CFA-BC09-125D13B12353}"),
                    Name = "Main.FinancialAffairs.Statistics.ttjzd",
                    Nickname = "主窗体.财务管理.团体结账单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                 },
                                               new FormModule
                   {
                    Id = new Guid("{C129F4E3-B45C-4DBE-AF3D-7D410DF8E3BB}"),
                    Name = "Main.FinancialAffairs.Statistics.SFMX",
                    Nickname = "主窗体.财务管理.收费明细",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                 },
                   new FormModule
                   {
                    Id = new Guid("{D69755C4-7800-4496-B211-4D0D74822621}"),
                    Name = "Main.FinancialAffairs.Statistics.RBMX",
                    Nickname = "主窗体.财务管理.日报明细",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                        },
                         new FormModule
                   {
                    Id = new Guid("{E3EF5A4F-BF55-4CC5-9ED8-075D15CFCA1A}"),
                    Name = "Main.FinancialAffairs.Statistics.KSZ",
                    Nickname = "主窗体.财务管理.卡设置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                        },
                              new FormModule
                   {
                    Id = new Guid("{AAC16494-7BE3-4A55-B159-1A65BFCBA232}"),
                    Name = "Main.FinancialAffairs.Statistics.cardSend",
                    Nickname = "主窗体.财务管理.体检卡发卡",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                              },
                                            new FormModule
                   {
                    Id = new Guid("{B0064149-F6A6-45BD-9B0D-F80B0914C1E3}"),
                    Name = "Main.FinancialAffairs.Statistics.cardSeach",
                    Nickname = "主窗体.财务管理.体检卡查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                              },
                                                      new FormModule
                {
                    Id = new Guid("F6239335-5490-4112-B3D6-4E5E3CBCC56A"),
                    Name = "FrmInspectionTotalList.SCGW",
                    Nickname = "主窗体.总检列表.上传公卫",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                      },// 主窗体.医生管理.总检.按钮
                                       new FormModule
                {
                    Id = new Guid("040AD26F-EB0C-4456-AF4E-DC6594BE96E3"),
                    Name = "FrmInspectionTotalList.SCJG",
                    Nickname = "主窗体.总检列表.上传结果",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                       },//ClientCharge.simpleButton1
                                       new FormModule
                {
                    Id = new Guid("736A1CBB-0661-426C-806E-86FEDB9CB7CC"),
                    Name = "ClientCharge.SFSH",
                    Nickname = "主窗体.团体收费.收费申请",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                       },
                   new FormModule
                {
                    Id = new Guid("9CFF920C-D0D9-4255-A92A-E41529A673FA"),
                    Name = "ClientCharge.SF",
                    Nickname = "主窗体.团体收费.收费",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                   },
                   new FormModule
                {
                    Id = new Guid("F054BCAD-8692-4A7C-9F52-FF426B207B20"),
                    Name = "ClientCharge.fz",
                    Nickname = "主窗体.团体收费.封账",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                                  new FormModule
                {
                    Id = new Guid("BCFFC68D-1782-4F85-9CB3-2540BE046A31"),
                    Name = "ClientCharge.JZ",
                    Nickname = "主窗体.团体收费.解帐",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                         new FormModule
                {
                    Id = new Guid("4AD96E96-97FF-40C5-A7CF-72B22B5BFBBA"),
                    Name = "Main.OAApproval.OAZKAdd",
                    Nickname = "主窗体.OA审批.折扣审批",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                               new FormModule
                {
                    Id = new Guid("61264F51-579F-4283-AFDA-F4E39A2C012F"),
                    Name = "Main.OAApproval.OAZKSet",
                    Nickname = "主窗体.OA审批.审批设置",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                               new FormModule
                {
                    Id = new Guid("7FED2EED-EDA7-4E01-8491-E04DE1592C15"),
                    Name = "main.Crissis.CriticalList",
                    Nickname = "主窗体.质控管理.危急值一览",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                               new FormModule
                {
                    Id = new Guid("F7D74291-190B-4315-8CB9-F015ED08C073"),
                    Name = "main.Crissis.CrissisCalSH",
                    Nickname = "主窗体.质控管理.危急值报告",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                               new FormModule
                {
                    Id = new Guid("F52A3A87-D61C-4466-8EFF-C54602D51F01"),
                    Name = "main.Crissis.CrissisCalReport.Mess",
                    Nickname = "主窗体.质控管理.危急值报告.危急值通知",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                               new FormModule
                {
                    Id = new Guid("C9198194-390B-4C52-ABB8-086F7C59A653"),
                    Name = "main.Crissis.CrissisCalReport.OK",
                    Nickname = "主窗体.质控管理.危急值报告.危机值确认",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },

                                       new FormModule
                {
                    Id = new Guid("F7B59934-42E6-4B53-A08D-2A5BE171382E"),
                    Name = "BallCheckStatic.butHS",
                    Nickname = "主窗体.财务管理.体检费用情况(团检).核算",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                                                     },
                                                              new FormModule
                {
                    Id = new Guid("411772C6-F2D0-4A00-BBF8-4ACFD295B76C"),
                    Name = "FrmClientTeamCharge.textEditSW",
                    Nickname = "主窗体.收费设置.商务金额",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                              }
                                                              ,
                                                              new FormModule
                {
                    Id = new Guid("04E41E03-D8E2-4604-8368-A8A30D6E7B81"),
                    Name = "FrmEditTjlClientRegs.butChargeSetting",
                    Nickname = "主窗体.单位预约.收费设置",
                    TypeName = Variables.ButtonType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                              },
                                                              new FormModule  {
                    Id = new Guid("507A5528-5E55-4B86-B2C4-E5F7492648DC"),
                    Name = "Main.Doctor.Check.barButtonItemPer",
                    Nickname = "主窗体.医生管理.初审",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                              },
                                                                new FormModule  {
                    Id = new Guid("AE21AE20-24A6-4CD8-BC2F-6C7B8DE619D9"),
                    Name = "Main.System.Other.BarPrice",
                    Nickname = "主窗体.系统管理.医嘱项目同步",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                              },

                                                                            new FormModule  {
                    Id = new Guid("36F4566E-36BD-41E5-B53D-61FB27B1A478"),
                    Name = "Main.FrontDesk.Examination.barButtonIteme抽血工作站",
                    Nickname = "主窗体.前台管理.体检.抽血工作站",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                              },
                                                    new FormModule  {
                    Id = new Guid("4B0DEDE2-973F-42DA-85A3-7D78A846221E"),
                    Name = "Main.FrontDesk.Statistics.Order",
                    Nickname = "主窗体.体检预约.商务订单",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },
                                                       new FormModule  {
                    Id = new Guid("1093DE74-70A1-45EA-95F6-93552BFDB0FA"),
                    Name = "Main.FrontDesk.Statistics.ContractManager",
                    Nickname = "主窗体.体检预约.合同管理",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },

                                                          new FormModule  {
                    Id = new Guid("4BD0AB42-2285-4721-8812-55AAC811532B"),
                    Name = "Main.FrontDesk.Statistics.ContractManagerAdmin",
                    Nickname = "主窗体.体检预约.合同管理(管理员)",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },

                                                          new FormModule  {
                    Id = new Guid("6C65A111-F318-44E8-98ED-77A4B5401A7D"),
                    Name = "Main.FinancialAffairs.Statistics.GRSF",
                    Nickname = "主窗体.财务管理.个人收费查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },


        new FormModule  {
                    Id = new Guid("9DC61125-A139-474B-A5BD-EDF5A174FD88"),
                    Name = "Main.FinancialAffairs.Statistics.TTSF",
                    Nickname = "主窗体.财务管理.团体收费查询",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },

            new FormModule  {
                    Id = new Guid("307B18B9-5B04-4FBF-9C50-5E6942F38782"),
                    Name = "Main.FrontDesk.Examination.barSimpCustomerReg",
                    Nickname = "主窗体.前台管理.体检.登记",
                    TypeName = Variables.MenuType,
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                                                          },




            };


            // 模板
            //new FormModule
            //{
            //    Id = new Guid("GUID"),
            //    Name = "Name.Name.Name.Name",
            //    Nickname = "名称.名称.名称.名称",
            //    TypeName = Variables.MenuType,
            //    CreationTime = DateTime.Now,
            //    CreatorUserId = 0
            //},// 名称（新生成）

            DeleteModules = new List<FormModule>();
        }

        public void Create()
        {
            foreach (var module in InitialModules)
            {
                CreateEditions(module);
            }

            foreach (var module in DeleteModules)
            {
                DeleteEditions(module);
            }
        }

        private void CreateEditions(FormModule module)
        {
            if (_context.FormModules.Any(l => l.Id == module.Id))
            {
                var entity = _context.FormModules.Find(module.Id);
                if (entity != null)
                {
                    entity.Name = module.Name;
                    entity.Nickname = module.Nickname;
                    entity.TypeName = module.TypeName;
                }
            }
            else
            {
                _context.FormModules.Add(module);
            }

            _context.SaveChanges();
        }

        private void DeleteEditions(FormModule module)
        {
            if (_context.FormModules.Any(l => l.Id == module.Id))
            {
                var entity = _context.FormModules.Find(module.Id);
                if (entity != null)
                {
                    _context.FormModules.Remove(entity);
                    _context.SaveChanges();
                }
            }
        }
    }
}