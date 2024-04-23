using System;
using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData
{
    public class InitializeStartupMenuBar
    {
        private readonly MyProjectDbContext _context;

        private static List<StartupMenuBar> InitialStartupMenuBars { get; set; }

        private static List<StartupMenuBar> DeleteStartupMenuBars { get; set; }

        public InitializeStartupMenuBar(MyProjectDbContext context)
        {
            _context = context;
        }

        static InitializeStartupMenuBar()
        {
            InitialStartupMenuBars = new List<StartupMenuBar>
            {
                new StartupMenuBar
                {
                    Id = new Guid("{57B8DAE0-D697-40AC-BACA-0DA5EC59D70C}"),
                    BarButtonItemName = "barButtonItemFrmTjlClientInfoes",
                    BarButtonItemCaption = "主窗体.销售管理.单位.建档",
                    CreatorUserId = 0,
                    CreationTime = DateTime.Now
                },// 主窗体.销售管理.单位.建档（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{949CE6D0-6154-4C39-B1B8-5A861976F0CD}"),
                    BarButtonItemName = "barButtonItemComposeTool",
                    BarButtonItemCaption = "主窗体.销售管理.团体.组单",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.团体.组单（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{4177E47E-37AB-4DC2-A418-92A78F919F0D}"),
                    BarButtonItemName = "barButtonItemAppointmentStatisticsByYears",
                    BarButtonItemCaption = "主窗体.销售管理.统计.历年预约统计",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.销售管理.统计.历年预约统计（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{9140928B-13A7-4DC6-9BC2-7CBDA0BA4F05}"),
                    BarButtonItemName = "barButtonItemCustomerReg",
                    BarButtonItemCaption = "主窗体.前台管理.体检.登记",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.登记（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{D559495D-78EA-4DF3-8E4A-543D0D579C6C}"),
                    BarButtonItemName = "barButtonItemBatchCustomerReg",
                    BarButtonItemCaption = "主窗体.前台管理.体检.批量登记",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.批量登记（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{75353D12-F8B4-47E3-85C7-3FA987FBA28F}"),
                    BarButtonItemName = "barButtonItemFrmClientRegList",
                    BarButtonItemCaption = "主窗体.前台管理.体检.团体导引单",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.团体导引单（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{4C6B933C-756A-4C11-9D18-53A15B124447}"),
                    BarButtonItemName = "barButtonItemCrossTable",
                    BarButtonItemCaption = "主窗体.前台管理.体检.交表",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.交表（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{72E7E060-0B7B-4B9F-912C-81D2B5B7275E}"),
                    BarButtonItemName = "barButtonItemGuideListCollectionSetting",
                    BarButtonItemCaption = "主窗体.前台管理.体检.导引单领取",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.体检.导引单领取（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{8A4DB79C-059A-44A2-82DE-8CB01AA4F1FD}"),
                    BarButtonItemName = "barButtonItemScheduleOfGrid",
                    BarButtonItemCaption = "主窗体.前台管理.预约.排期",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.预约.排期（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{7E4AAFF5-199A-482C-BFED-E666129263AB}"),
                    BarButtonItemName = "barButtonItemScheduleQuery",
                    BarButtonItemCaption = "主窗体.前台管理.预约.排期查询",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.预约.排期查询（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{543CD28A-FFFC-4845-A133-B79D0DBBDBD5}"),
                    BarButtonItemName = "barButtonItemRosterSetting",
                    BarButtonItemCaption = "主窗体.前台管理.统计.花名册",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.前台管理.统计.花名册（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{844608C3-18DE-48F4-BFA8-E08F190257D7}"),
                    BarButtonItemName = "barButtonItemDoctorDesk",
                    BarButtonItemCaption = "主窗体.医生管理.医生工作站.工作站",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.医生工作站.工作站（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{B1391EB0-7BBF-4CAA-8941-65E9C5DA06EE}"),
                    BarButtonItemName = "barButtonItemFrmInspectionTotalList",
                    BarButtonItemCaption = "主窗体.医生管理.总检.审核",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{F0C4B80A-B783-40EF-9BE7-09CEC0B80FE5}"),
                    BarButtonItemName = "barButtonItemFrmInspectionTotalList1",
                    BarButtonItemCaption = "主窗体.医生管理.总检.审核1",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核1（已校验 2019年1月21日）

              
                                new StartupMenuBar
                {
                    Id = new Guid("BBF76233-FC7F-40CB-BBA9-B7E214AFD2FE"),
                    BarButtonItemName = "barButtonItemFrmInspectionTotalNew",
                    BarButtonItemCaption = "主窗体.医生管理.总检.新总检",                  
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.医生管理.总检.审核（已校验 2018年12月5日）
                new StartupMenuBar
                {
                    Id = new Guid("{CCC4A083-C4B6-4ED8-9D63-5D6F323182C2}"),
                    BarButtonItemName = "barButtonItemPersonCharge",
                    BarButtonItemCaption = "主窗体.财务管理.收费.个人收费",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.收费.个人收费（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{D7E577DF-EA80-4DAD-9D16-D0B0A141D55B}"),
                    BarButtonItemName = "barButtonItemClientCharge",
                    BarButtonItemCaption = "主窗体.财务管理.收费.团体结算",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.收费.团体结算（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{8B53BEAE-C92E-421F-A5F0-52CF7EB10340}"),
                    BarButtonItemName = "barButtonItemFrmInvoiceManagement",
                    BarButtonItemCaption = "主窗体.财务管理.发票.发票管理",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.发票.发票管理（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{DFA9CD9B-635E-4A82-AA64-A1BFBE4F0F1A}"),
                    BarButtonItemName = "barButtonItemInvalidInvoice",
                    BarButtonItemCaption = "主窗体.财务管理.发票.退费",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.发票.退费（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{D25CEA31-7ADA-4E72-B769-1A2240921A38}"),
                    BarButtonItemName = "barButtonItemChecklistReport",
                    BarButtonItemCaption = "主窗体.财务管理.打印.体检清单",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.打印.体检清单（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{85949811-9DC7-48AC-9B24-2B8DCB2D601C}"),
                    BarButtonItemName = "barButtonItemDailyReport",
                    BarButtonItemCaption = "主窗体.财务管理.统计.日报表",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.统计.日报表（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{5F5DE53C-65BF-4DD7-B018-FD3C992242A3}"),
                    BarButtonItemName = "barButtonItemFrmTTJZD",
                    BarButtonItemCaption = "主窗体.财务管理.统计.团体结账单",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.财务管理.统计.团体结账单（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{CFDE86B9-9420-40FC-9492-E55B7C7B11F7}"),
                    BarButtonItemName = "barButtonItemGroupReportList",
                    BarButtonItemCaption = "主窗体.报告管理.报告.团体报告",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.团体报告（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{2F8C169C-CB21-4C74-9786-D7E7CFD1CEF5}"),
                    BarButtonItemName = "barButtonItemCustomerReportHandover",
                    BarButtonItemCaption = "主窗体.报告管理.报告.报告单交接",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单交接（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{9CA24390-F818-499E-B72F-FA9D2A48C1F0}"),
                    BarButtonItemName = "barButtonItemCustomerReportReceive",
                    BarButtonItemCaption = "主窗体.报告管理.报告.报告单领取",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单领取（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{2C3D24CD-BA33-400E-B07B-FF04F9BEEE15}"),
                    BarButtonItemName = "barButtonItemCustomerReportQuery",
                    BarButtonItemCaption = "主窗体.报告管理.报告.报告单查询",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.报告.报告单查询（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{055FF229-AA5E-4B7F-969C-50D0EAED593B}"),
                    BarButtonItemName = "barButtonItemPrintPreview",
                    BarButtonItemCaption = "主窗体.报告管理.打印.打印预览",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.报告管理.打印.打印预览（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{897DB918-E3F3-4001-A14D-FBE2CFFFDF38}"),
                    BarButtonItemName = "barButtonItemFrmZJYSGZL",
                    BarButtonItemCaption = "主窗体.统计查询.统计.总检医生工作量",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.总检医生工作量（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{AAD0AD03-6A0C-46A6-A856-803F05D49AF4}"),
                    BarButtonItemName = "barButtonItemFrmJCXMTJ",
                    BarButtonItemCaption = "主窗体.统计查询.统计.检查项目",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.检查项目（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{9125829B-CCFC-4EE6-AFBB-C9C568A0E3A6}"),
                    BarButtonItemName = "barButtonItemFrmTCTJ",
                    BarButtonItemCaption = "主窗体.统计查询.统计.套餐统计",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.套餐统计（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{58218DC3-B60E-4C48-A1DF-B3D580A886D1}"),
                    BarButtonItemName = "barButtonItemFrmKSGZL",
                    BarButtonItemCaption = "主窗体.统计查询.统计.科室工作量统计",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.科室工作量统计（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{CDB1E4D9-ED4C-4D72-A418-FD9829FA6052}"),
                    BarButtonItemName = "barButtonItemFrmKSHBGZLTJ",
                    BarButtonItemCaption = "主窗体.统计查询.统计.科室环比工作量",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.科室环比工作量（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{F85E49B0-B236-4487-91F8-66EBF10200B1}"),
                    BarButtonItemName = "barButtonItemFrmHZJCXMTJ",
                    BarButtonItemCaption = "主窗体.统计查询.统计.体检统计",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.体检统计（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{16005FBB-BC8B-40A3-BC46-7369B6DE5F3C}"),
                    BarButtonItemName = "barButtonItemFrmTJRSTJ",
                    BarButtonItemCaption = "主窗体.统计查询.统计.人数统计",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.统计查询.统计.人数统计（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{3EBF3688-B097-4EC1-A840-3971DA27CB46}"),
                    BarButtonItemName = "barButtonItemDepartmentList",
                    BarButtonItemCaption = "主窗体.系统.编码.科室",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.科室（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{7ED3302E-E4F4-4017-B62A-B1D802474427}"),
                    BarButtonItemName = "barButtonItemItemList",
                    BarButtonItemCaption = "主窗体.系统.编码.项目",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.项目（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{533348C7-F3BA-4226-B62B-69E47574E0BB}"),
                    BarButtonItemName = "barButtonItemBarCodeList",
                    BarButtonItemCaption = "主窗体.系统.编码.条码",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.条码（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{1C7169AF-A60C-4808-9291-50DF92B8B9C1}"),
                    BarButtonItemName = "barButtonItemItemSuitList",
                    BarButtonItemCaption = "主窗体.系统.编码.套餐",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.套餐（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{ABC43777-D332-4EDA-9033-B9E6D2FC4FBA}"),
                    BarButtonItemName = "barButtonItemUserList",
                    BarButtonItemCaption = "主窗体.系统.编码.用户",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.用户（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{24493124-AD8A-449C-AD26-D44CAF9809CA}"),
                    BarButtonItemName = "barButtonItemBasicDictionaryList",
                    BarButtonItemCaption = "主窗体.系统.编码.字典",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.编码.字典（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{5A7E8695-37BB-4EAF-8F32-5FB1C80F41D4}"),
                    BarButtonItemName = "barButtonItemItemGroupList",
                    BarButtonItemCaption = "主窗体.系统.配置.组合",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.组合（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{01C08C66-041A-4B3E-B4E9-3FB1EB8AFFAB}"),
                    BarButtonItemName = "barButtonItemSuggestList",
                    BarButtonItemCaption = "主窗体.系统.配置.建议",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.建议（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{014A589E-4AAC-48CD-B201-E8AFD9608DE6}"),
                    BarButtonItemName = "barButtonItemWorkTypeList",
                    BarButtonItemCaption = "主窗体.系统.配置.工种",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.工种（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{7635577C-20EB-402B-8DE5-EA960B66C1B8}"),
                    BarButtonItemName = "barButtonItemPayMethod",
                    BarButtonItemCaption = "主窗体.系统.配置.支付方式",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.支付方式（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{15706464-6B8D-49D8-B473-D353B4B56E02}"),
                    BarButtonItemName = "barButtonItemRoleManager",
                    BarButtonItemCaption = "主窗体.系统.配置.权限",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.权限（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{A893B199-F0EC-48BC-AC1E-6652E979759D}"),
                    BarButtonItemName = "barButtonItemDiagnosisTop",
                    BarButtonItemCaption = "主窗体.系统.配置.复合判断",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.复合判断（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{5585C154-D2C1-4D2C-9D4A-A11EE98DC085}"),
                    BarButtonItemName = "barButtonItemDictionarySetting",
                    BarButtonItemCaption = "主窗体.系统.配置.项目字典",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.配置.项目字典（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{668CE2C3-95B8-4E4B-85E2-C14FC87DA635}"),
                    BarButtonItemName = "barButtonItemCodeConfig",
                    BarButtonItemCaption = "主窗体.系统.其它.编码配置",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.编码配置（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{AEB9B471-D780-4FCE-9DDE-68C96EF90D8A}"),
                    BarButtonItemName = "barButtonItemUpdateCache",
                    BarButtonItemCaption = "主窗体.系统.其它.更新缓存",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                },// 主窗体.系统.其它.更新缓存（已校验 2019年1月17日）
                new StartupMenuBar
                {
                    Id = new Guid("{B8B63CC0-17C6-4103-ADC2-0687188E6BE9}"),
                    BarButtonItemName = "barButtonItemFrmCategory",
                    BarButtonItemCaption = "主窗体.系统.其它.人员类别",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                }// 主窗体.系统.其它.人员类别（已校验 2019年1月17日）
            };

            DeleteStartupMenuBars = new List<StartupMenuBar>();
        }

        public void Create()
        {
            foreach (var module in InitialStartupMenuBars)
            {
                CreateEditions(module);
            }

            foreach (var module in DeleteStartupMenuBars)
            {
                DeleteEditions(module);
            }
        }

        private void CreateEditions(StartupMenuBar module)
        {
            if (_context.StartupMenuBars.Any(l => l.Id == module.Id))
            {
                var entity = _context.StartupMenuBars.Find(module.Id);
                if (entity != null)
                {
                    entity.BarButtonItemName = module.BarButtonItemName;
                    entity.BarButtonItemCaption = module.BarButtonItemCaption;
                }
            }
            else
            {
                _context.StartupMenuBars.Add(module);
            }

            _context.SaveChanges();
        }

        private void DeleteEditions(StartupMenuBar module)
        {
            if (_context.StartupMenuBars.Any(l => l.Id == module.Id))
            {
                var entity = _context.StartupMenuBars.Find(module.Id);
                if (entity != null)
                {
                    _context.StartupMenuBars.Remove(entity);
                    _context.SaveChanges();
                }
            }
        }
    }
}