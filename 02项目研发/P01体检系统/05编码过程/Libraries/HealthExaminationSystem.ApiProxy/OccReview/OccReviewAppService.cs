using Sw.Hospital.HealthExaminationSystem.Application.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccReview
{
   public class OccReviewAppService : AppServiceApiProxyBase, IOccReviewAppService
    {
        public List<OutOccReviewDto> GetOutOccReviewDto(OutOccReviewDto input)
        {
            return GetResult<OutOccReviewDto, List<OutOccReviewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    
        public CusVisitDto SaveVisit(CusVisitDto input)
        {
            return GetResult<CusVisitDto, CusVisitDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public CusVisitDto SearchVisit(CusVisitDto input)
        {
            return GetResult<CusVisitDto, CusVisitDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<VisiteCusRegDto> SearchCusRegVisit(searchVisitDto input)
        {
            return GetResult<searchVisitDto, List<VisiteCusRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public SaveCusVisitManageDto SaveCusVisitManage(SaveCusVisitManageDto input)
        {
            return GetResult<SaveCusVisitManageDto, SaveCusVisitManageDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<SaveCusVisitManageDto> SearchCusVisitManage(CusVisitDto input)
        {
            return GetResult<CusVisitDto, List<SaveCusVisitManageDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void DelCusVisitManage(SaveCusVisitManageDto input)
        {
              GetResult<SaveCusVisitManageDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }

    }
}
