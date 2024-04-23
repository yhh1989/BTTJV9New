using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Company;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegister;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerRegisterSummarize;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.DynamicColumnDirectory;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Foreground;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Market;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SchedulingSecondEdition;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Market;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Users;

namespace Sw.Hospital.HealthExaminationSystem.Common.UserCache
{
    /// <summary>
    /// 已定义的 API 代理
    /// </summary>
    public class DefinedApiProxy
    {
        /// <summary>
        /// 已定义的 API 代理
        /// </summary>
        public DefinedApiProxy()
        {
            DepartmentAppService = new DepartmentAppService();

            FormModuleAppService = new FormModuleAppService();

            UserAppService = new UserAppService();

            CommonAppService = new CommonAppService();

            SummarizeAdviceAppService = new SummarizeAdviceAppService();

            ItemSuitAppService = new ItemSuitAppService();

            BasicDictionaryAppService = new BasicDictionaryAppService();

            ClientRegAppService = new ClientRegAppService();

            ItemInfoAppService = new ItemInfoAppService();

            GroupAppService = new ItemGroupAppService();
            InspectionTotalService = new InspectionTotalAppService();
            CustomerRegisterSummarizeAppService = new CustomerRegisterSummarizeAppService();
            CustomerRegisterDepartmentSummaryAppService = new CustomerRegisterDepartmentSummaryAppService();
            CustomerRegisterItemAppService = new CustomerRegisterItemAppService();
            CustomerRegisterAppService = new CustomerRegisterAppService();
            CustomerRegisterItemPictureAppService = new CustomerRegisterItemPictureAppService();
            PictureController = new PictureController();

            ComplaintAppService = new ComplaintAppService();

            BloodWorkstationAppService = new BloodWorkstationAppService();

            ContractAppService = new ContractAppService();

            ContractAdjunctController = new ContractAdjunctController();
            ManualSchedulingAppService = new ManualSchedulingAppService();
            ClientInfoesAppService = new ClientInfoesAppService();
            DynamicColumnAppService = new DynamicColumnAppService();
            CompanyRelatedAppService = new CompanyRelatedAppService();
            CustomerRegisterSummarizeAdviceAppService = new CustomerRegisterSummarizeAdviceAppService();
            CustomerRegisterItemGroupAppService = new CustomerRegisterItemGroupAppService();
        }

        /// <summary>
        /// 科室应用服务
        /// </summary>
        public IDepartmentAppService DepartmentAppService { get; }

        /// <summary>
        /// 窗体模块应用服务
        /// </summary>
        public IFormModuleAppService FormModuleAppService { get; }

        /// <summary>
        /// 用户应用服务
        /// </summary>
        public IUserAppService UserAppService { get; }

        /// <summary>
        /// 共用应用服务
        /// </summary>
        public ICommonAppService CommonAppService { get; set; }

        /// <summary>
        /// 总检建议应用服务
        /// </summary>
        public ISummarizeAdviceAppService SummarizeAdviceAppService { get; set; }

        /// <summary>
        /// 项目套餐应用服务
        /// </summary>
        public IItemSuitAppService ItemSuitAppService { get; set; }

        /// <summary>
        /// 基础字典应用服务
        /// </summary>
        public IBasicDictionaryAppService BasicDictionaryAppService { get; set; }

        /// <summary>
        /// 单位预约应用服务
        /// </summary>
        public IClientRegAppService ClientRegAppService { get; set; }

        /// <summary>
        /// 项目缓存
        /// </summary>
        public IItemInfoAppService ItemInfoAppService { get; set; }

        /// <summary>
        /// 组合缓存
        /// </summary>
        public IItemGroupAppService GroupAppService { get; set; }

        /// <summary>
        /// 建议缓存
        /// </summary>
        public IInspectionTotalAppService InspectionTotalService { get; set; }

        /// <summary>
        /// 参考值缓存
        /// </summary>
        private IInspectionTotalAppService _itemStandardRepository { get; set; }

        /// <summary>
        /// 体检人预约总检汇总应用服务
        /// </summary>
        public ICustomerRegisterSummarizeAppService CustomerRegisterSummarizeAppService { get; set; }

        /// <summary>
        /// 体检人预约科室小结汇总应用服务
        /// </summary>
        public ICustomerRegisterDepartmentSummaryAppService CustomerRegisterDepartmentSummaryAppService { get; set; }

        /// <summary>
        /// 体检人预约项目体检记录应用服务
        /// </summary>
        public ICustomerRegisterItemAppService CustomerRegisterItemAppService { get; set; }

        /// <summary>
        /// 体检人预约应用服务
        /// </summary>
        public ICustomerRegisterAppService CustomerRegisterAppService { get; set; }

        /// <summary>
        /// 体检人预约项目结果图像应用服务
        /// </summary>
        public ICustomerRegisterItemPictureAppService CustomerRegisterItemPictureAppService { get; set; }

        /// <summary>
        /// 图像控制器
        /// </summary>
        public PictureController PictureController { get; set; }

        /// <summary>
        /// 投诉应用服务接口
        /// </summary>
        public IComplaintAppService ComplaintAppService { get; }

        /// <summary>
        /// 抽血工作站应用服务接口
        /// </summary>
        public IBloodWorkstationAppService BloodWorkstationAppService { get; }

        /// <summary>
        /// 合同应用服务接口
        /// </summary>
        public IContractAppService ContractAppService { get; }

        /// <summary>
        /// 合同附件管理器
        /// </summary>
        public ContractAdjunctController ContractAdjunctController { get; }

        /// <summary>
        /// 人工行程安排应用服务接口
        /// </summary>
        public IManualSchedulingAppService ManualSchedulingAppService { get; }

        /// <summary>
        /// 公司信息应用服务接口
        /// </summary>
        public IClientInfoesAppService ClientInfoesAppService { get; }

        /// <summary>
        /// 动态列应用服务
        /// </summary>
        public IDynamicColumnAppService DynamicColumnAppService { get; }

        /// <summary>
        /// 团体及团体相关的应用服务接口
        /// </summary>
        public ICompanyRelatedAppService CompanyRelatedAppService { get; }

        /// <summary>
        /// 体检人预约总检建议记录应用服务接口
        /// </summary>
        public ICustomerRegisterSummarizeAdviceAppService CustomerRegisterSummarizeAdviceAppService { get; }

        /// <summary>
        /// 体检人预约的项目组合应用服务接口
        /// </summary>
        public ICustomerRegisterItemGroupAppService CustomerRegisterItemGroupAppService { get; }
    }
}