using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview
{
    public interface IOccReviewAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<OutOccReviewDto> GetOutOccReviewDto(OutOccReviewDto input);
        CusVisitDto SaveVisit(CusVisitDto input);
        CusVisitDto SearchVisit(CusVisitDto input);
        List<VisiteCusRegDto> SearchCusRegVisit(searchVisitDto input);
        SaveCusVisitManageDto SaveCusVisitManage(SaveCusVisitManageDto input);
        List<SaveCusVisitManageDto> SearchCusVisitManage(CusVisitDto input);
          void DelCusVisitManage(SaveCusVisitManageDto input);
    }
}
