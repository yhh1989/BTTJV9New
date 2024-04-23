using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport
{
    public interface IPrintPreviewAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<ClientInfoesDto> OldGetClientInfos();

        List<CustomerPrintInfoOutput> GetPrintInfo();

        List<ClientInfoOfPrintPreviewDto> GetClientInfos();

        List<ClientRegForPrintPreviewDto> GetClientRegs(EntityDto<Guid> input);

        List<CustomerRegForPrintPreviewDto> GetCustomerRegs(SearchCustomerRegForPrintPreviewDto input);

        void UpdateCustomerRegisterPrintState(ChargeBM input);
        /// <summary>
        /// 通知单
        /// </summary>
        /// <param name="input"></param>
        void UpdateNoticePrintState(EntityDto<Guid> input);

        ChargeBM UpdateCustomerRegisterHGZState(EntityDto<Guid> input);
        void UpdateCustomerSumPrintState(ChargeBM input);
        /// <summary>
        /// 根据预约Id获取问卷相关
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ReportOccQuesDto getOccQue(EntityDto<Guid> input);
        /// <summary>
        /// 获取症状
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ReportOccQuesSymptomDto> getOccQuesSymptoms(EntityDto<Guid> input);
    }
}