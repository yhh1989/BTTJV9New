using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.IDNumber
{
    /// <summary>
    /// 编码生成应用服务接口
    /// </summary>
    public interface IIDNumberAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 创建科室编码
        /// </summary>
        string CreateDepartmentBM();

        /// <summary>
        /// 创建单位编码
        /// </summary>
        string CreateClientBM();

        /// <summary>
        /// 创建组合编码
        /// </summary>
        string CreateItemGroupBM();

        /// <summary>
        /// 创建项目编码
        /// </summary>
        string CreateItemBM();

        /// <summary>
        /// 创建用户编码
        /// </summary>
        string CreateEmployeeBM();

        /// <summary>
        /// 创建套餐编码
        /// </summary>
        string CreateItemSuitBM();

        /// <summary>
        /// 创建单位预约编码
        /// </summary>
        string CreateClientRegBM();

        /// <summary>
        /// 创建体检人编码
        /// </summary>
        string CreateCustomerBM();

        /// <summary>
        /// 创建体检人预约编码
        /// </summary>
        string CreateCustomerRegBM();

        /// <summary>
        /// 创建体检号编码
        /// </summary>
        string CreateArchivesNumBM();

        /// <summary>
        /// 创建条码号编码
        /// </summary>
        string CreateBarBM();

        /// <summary>
        /// 创建分组编码
        /// </summary>
        string CreateTeamBM();
        /// <summary>
        /// 创建建议编码
        /// </summary>
        string CreateAdviceBM();
        string CreateApplicationBM();
        /// <summary>
        /// 健康证编码
        /// </summary>
        /// <returns></returns>
        string CreateJKZBM();
        /// <summary>
        /// 合格证编码
        /// </summary>
        /// <returns></returns>
        string CreatHGZBM();
        /// <summary>
        /// 登记号
        /// </summary>
        /// <returns></returns>
        int CreateRegNum();
        /// <summary>
        /// 创建单位审批编码
        /// </summary>
        /// <returns></returns>
        string CreateClientZKBM();             
        List<IDNumberDto> GetAllList();

        void Create(CreateIdNumberDto input);

        IDNumberDto GetByName(NameDto input);
    }
}