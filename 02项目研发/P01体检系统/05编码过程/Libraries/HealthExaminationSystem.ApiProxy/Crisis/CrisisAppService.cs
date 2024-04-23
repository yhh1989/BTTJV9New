using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis
{
    public class CrisisAppService : AppServiceApiProxyBase, ICrisisAppService
    {      
        /// <summary>
        /// 查询危急值
        /// </summary>
        public CrisisSetViewDto QueryCrisisInfos(SearchCrisisInfosDto input)
        {
            return GetResult<SearchCrisisInfosDto,CrisisSetViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 设置危急值
        /// </summary>
        public void SetCrisisList(SetCrisisDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询危急值列表
        /// </summary>
        public List<CustomerServiceCrisisViewDto> QueryCrisisList(SearchCrisisInfosDto input)
        {
            return GetResult<SearchCrisisInfosDto, List<CustomerServiceCrisisViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询回访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CallBackDto> QueryCallBackList(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<CallBackDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存回访信息
        /// </summary>
        /// <param name="input"></param>
        public void SaveCallBack(CallBackDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存复查设置
        /// </summary>
        /// <param name="input"></param>
        public void SaveReviewSet(ReviewSetDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void DelReviewSet(ReviewSetDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<ReviewSetDto> getAllReview()
        {
            return GetResult<List<ReviewSetDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
         
        public List<CrisVisitDto> PageFulls(CrisVisitSelectDto input)
        {
            return GetResult<CrisVisitSelectDto, List<CrisVisitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
           
        }

        public string acc()
        {
            var a = DynamicUriBuilder.GetAppSettingValue();
            GetResult(DynamicUriBuilder.GetAppSettingValue());
            return "1";
        }

        public List<CrisisCustomerDto> GetCrisisCustome()
        {
            return GetResult<List<CrisisCustomerDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public CrisisCustomerDto UpdateCrisis(CrisisCustomerDto input)
        {
            return GetResult<CrisisCustomerDto, CrisisCustomerDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public int UpdateTjlCrisVisit(CrisisCustomerDto CrisisCustomerDto)
        {
            return GetResult<CrisisCustomerDto, int>(CrisisCustomerDto, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
