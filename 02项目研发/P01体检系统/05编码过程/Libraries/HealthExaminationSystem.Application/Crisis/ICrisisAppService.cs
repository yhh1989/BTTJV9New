using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis
{
    /// <summary>
    /// 危急值服务接口
    /// </summary>
    public interface ICrisisAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        /// <summary>
        /// 查询危急值
        /// </summary>
        CrisisSetViewDto QueryCrisisInfos(SearchCrisisInfosDto input);
        /// <summary>
        /// 设置危急值
        /// </summary>
        void SetCrisisList(SetCrisisDto input);
        /// <summary>
        /// 查询危急值列表
        /// </summary>
        List<CustomerServiceCrisisViewDto> QueryCrisisList(SearchCrisisInfosDto input);
        /// <summary>
        /// 查询回访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CallBackDto> QueryCallBackList(EntityDto<Guid> input);
        /// <summary>
        /// 保存回访信息
        /// </summary>
        /// <param name="input"></param>
        void SaveCallBack(CallBackDto input);
        /// <summary>
        /// 保存复查设置
        /// </summary>
        /// <param name="input"></param>
        void SaveReviewSet(ReviewSetDto input);

        void DelReviewSet(ReviewSetDto input);

        List<ReviewSetDto> getAllReview();

        List<CrisVisitDto> PageFulls(CrisVisitSelectDto input);

        List<CrisisCustomerDto> GetCrisisCustome();

        //处理完成
        CrisisCustomerDto UpdateCrisis(CrisisCustomerDto CrisisCustomerDto); 
        int  UpdateTjlCrisVisit(CrisisCustomerDto CrisisCustomerDto);  
        string acc();

    }
}
