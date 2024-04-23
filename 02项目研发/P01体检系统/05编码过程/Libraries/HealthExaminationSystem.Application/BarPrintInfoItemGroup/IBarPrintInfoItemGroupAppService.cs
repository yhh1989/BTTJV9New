using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup
{
    /// <summary>
    /// 条码打印表
    /// </summary>
    public interface IBarPrintInfoItemGroupAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 增加
        /// </summary>
        bool AddBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto);

        /// <summary>
        /// 批量增加
        /// </summary>
        bool AddBarPrintInfoItemGroupApp(List<CreateBarPrintInfoItemGroupDto> CustomerBarPrintInfoDto);

        /// <summary>
        /// 删除
        /// </summary>
        bool DeleteBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto);

        /// <summary>
        /// 查询已打印条码组合
        /// </summary>
        List<BarPrintInfoItemGroupQueryDto> GetLstBarPrintInfoItemGroupApp(CusNameInput cusNameInput);

    }
}
