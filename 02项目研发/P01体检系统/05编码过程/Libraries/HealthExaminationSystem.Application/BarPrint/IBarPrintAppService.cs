using System.Collections.Generic;
using System.Data;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint
{
    /// <summary>
    /// 条码打印表
    /// </summary>
    public interface IBarPrintAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 增加
        /// </summary>
        CustomerBarPrintInfoDto AddBarPrintApp(CreateCustomerBarPrintInfoDto CustomerBarPrintInfoDto);

        /// <summary>
        /// 批量增加
        /// </summary>
        void AddBarPrintApp(List<CreateCustomerBarPrintInfoDto> CustomerBarPrintInfoDto);

        /// <summary>
        ///删除
        /// </summary>
        bool DeleteBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto);

        /// <summary>
        ///更新
        /// </summary>
        bool UpdateBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto);

        /// <summary>
        ///批量更新
        /// </summary>
        bool UpdateBarPrintApp(List<CustomerBarPrintInfoDto> CustomerBarPrintInfoDto);

        /// <summary>
        /// 查询已打印条码
        /// </summary>
        List<CustomerBarPrintInfoDto> GetLstBarPrintApp(CusNameInput cusNameInput);
        /// <summary>
        /// 获取所有条码
        /// </summary>
        /// <param name="cusNameInput"></param>
        /// <returns></returns>
        ReportJsonDto GetBarPrint(CusNameInput cusNameInput);
        /// <summary>
        /// 批量打印条码
        /// </summary>
        /// <param name="cusNameInputls"></param>
        /// <returns></returns>
        ReportJsonDto GetAllBarPrint(List<CusNameInput> cusNameInputls);
        /// <summary>
        /// 更新导引单，条码打印状态
        /// </summary>
        /// <param name="input"></param>
        void UpdateCustomerRegisterPrintState(ChargeBM input);
    }
}