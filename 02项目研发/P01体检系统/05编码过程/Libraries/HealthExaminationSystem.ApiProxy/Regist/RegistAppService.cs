using Sw.Hospital.HealthExaminationSystem.Application.Regist;
using Sw.Hospital.HealthExaminationSystem.Application.Regist.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Regist
{
  public   class RegistAppService : AppServiceApiProxyBase, IRegistAppService
    {
        public void SaveRegsit(TbmRegsitDto input)
        {
             GetResult<TbmRegsitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TbmRegsitDto> SearchRegsit()
        {
           return GetResult<List<TbmRegsitDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
