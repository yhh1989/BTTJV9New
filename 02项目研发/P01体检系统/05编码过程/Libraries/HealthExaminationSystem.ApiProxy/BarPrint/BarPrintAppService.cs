using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint
{
   public class BarPrintAppService: AppServiceApiProxyBase, IBarPrintAppService
    {

        /// <summary>
        /// 增加
        /// </summary>
        public CustomerBarPrintInfoDto AddBarPrintApp(CreateCustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            return GetResult<CreateCustomerBarPrintInfoDto, CustomerBarPrintInfoDto>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        public void AddBarPrintApp(List<CreateCustomerBarPrintInfoDto> CustomerBarPrintInfoDto)
        {
            GetResult(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }

        public bool DeleteBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            return GetResult<CustomerBarPrintInfoDto, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        public List<CustomerBarPrintInfoDto> GetLstBarPrintApp(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, List<CustomerBarPrintInfoDto>>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="CustomerBarPrintInfoDto"></param>
        /// <returns></returns>
        public bool UpdateBarPrintApp(CustomerBarPrintInfoDto CustomerBarPrintInfoDto)
        {
            return GetResult<CustomerBarPrintInfoDto, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="CustomerBarPrintInfoDto"></param>
        /// <returns></returns>
        public bool UpdateBarPrintApp(List<CustomerBarPrintInfoDto> CustomerBarPrintInfoDto)
        {
            return GetResult<List<CustomerBarPrintInfoDto>, bool>(CustomerBarPrintInfoDto, DynamicUriBuilder.GetAppSettingValue());
        }
        public ReportJsonDto GetBarPrint(CusNameInput cusNameInput)
        {
            return GetResult<CusNameInput, ReportJsonDto>(cusNameInput, DynamicUriBuilder.GetAppSettingValue());

        }
        public ReportJsonDto GetAllBarPrint(List<CusNameInput> cusNameInputls)
        { 
              return GetResult<List<CusNameInput>, ReportJsonDto>(cusNameInputls, DynamicUriBuilder.GetAppSettingValue());

        }
    public void UpdateCustomerRegisterPrintState(ChargeBM input)
        {
             GetResult<ChargeBM>(input,DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
