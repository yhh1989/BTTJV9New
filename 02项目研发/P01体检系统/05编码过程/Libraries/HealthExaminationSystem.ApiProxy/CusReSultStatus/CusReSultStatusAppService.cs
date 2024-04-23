using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReSultStatus
{
  public class CusReSultStatusAppService : AppServiceApiProxyBase, ICusReSultStatusAppService
    {
        public ReSultSetDto Add(FullReSultSetDto input)
        {
            return GetResult<FullReSultSetDto,ReSultSetDto>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ReSultSetDto> GetReSultDepart()
        {
            return GetResult<List<ReSultSetDto>> (DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ReSultCusInfoDto> getCusResult(ShowResultSetDto input)
        {
            return GetResult<ShowResultSetDto,List<ReSultCusInfoDto>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
