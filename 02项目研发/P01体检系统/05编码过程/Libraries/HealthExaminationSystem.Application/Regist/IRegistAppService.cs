using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Regist.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Regist
{
   public interface IRegistAppService
#if !Proxy
        : IApplicationService
#endif
    {
        void SaveRegsit(TbmRegsitDto input);
        List<TbmRegsitDto> SearchRegsit();
    }
}
