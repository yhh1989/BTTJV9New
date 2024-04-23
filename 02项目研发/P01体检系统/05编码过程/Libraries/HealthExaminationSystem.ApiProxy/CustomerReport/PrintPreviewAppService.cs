using System;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport
{
    public class PrintPreviewAppService : AppServiceApiProxyBase, IPrintPreviewAppService
    {
        public List<ClientInfoesDto> OldGetClientInfos()
        {
            return GetResult<List<ClientInfoesDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CustomerPrintInfoOutput> GetPrintInfo()
        {
            return GetResult<List<CustomerPrintInfoOutput>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientInfoOfPrintPreviewDto> GetClientInfos()
        {
            return GetResult<List<ClientInfoOfPrintPreviewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientRegForPrintPreviewDto> GetClientRegs(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ClientRegForPrintPreviewDto>>(input,
                DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerRegForPrintPreviewDto> GetCustomerRegs(SearchCustomerRegForPrintPreviewDto input)
        {
            return GetResult<SearchCustomerRegForPrintPreviewDto, List<CustomerRegForPrintPreviewDto>>(input,
                DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdateCustomerRegisterPrintState(ChargeBM input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ChargeBM UpdateCustomerRegisterHGZState(EntityDto<Guid> input)
        {
            //GetResult(input, DynamicUriBuilder.GetAppSettingValue());
            return GetResult<EntityDto<Guid>, ChargeBM>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateNoticePrintState(EntityDto<Guid> input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateCustomerSumPrintState(ChargeBM input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据预约Id获取问卷相关
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReportOccQuesDto getOccQue(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ReportOccQuesDto>(input,
                DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据预约Id获取问卷相关
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ReportOccQuesSymptomDto> getOccQuesSymptoms(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ReportOccQuesSymptomDto>>(input,
                DynamicUriBuilder.GetAppSettingValue());
        }
    }
}