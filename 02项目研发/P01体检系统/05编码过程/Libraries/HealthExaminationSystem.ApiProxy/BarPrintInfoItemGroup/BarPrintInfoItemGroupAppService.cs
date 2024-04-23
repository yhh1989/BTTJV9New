using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup
{
    public class BarPrintInfoItemGroupAppService : AppServiceApiProxyBase, IBarPrintInfoItemGroupAppService
    {

        /// <summary>
        /// 增加
        /// </summary>
        public bool AddBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto)
        {
            return GetResult<CreateBarPrintInfoItemGroupDto, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        public bool AddBarPrintInfoItemGroupApp(List<CreateBarPrintInfoItemGroupDto> CustomerBarPrintInfoDto)
        {
            return GetResult<List<CreateBarPrintInfoItemGroupDto>, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="CustomerBarPrintInfoDto"></param>
        /// <returns></returns>
        public bool DeleteBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto)
        {
            return GetResult<CreateBarPrintInfoItemGroupDto, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }


        /// <summary>
        /// 查询
        /// </summary>
        public List<BarPrintInfoItemGroupQueryDto> GetLstBarPrintInfoItemGroupApp(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, List<BarPrintInfoItemGroupQueryDto>>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());
        }
        public bool UpdateBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            return GetResult<CustomerBarPrintInfoDto,bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
