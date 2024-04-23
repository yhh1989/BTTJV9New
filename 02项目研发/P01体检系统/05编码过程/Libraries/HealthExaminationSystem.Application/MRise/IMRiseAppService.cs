using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.MRise
{
    public interface IMRiseAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取所有发票抬头
        /// </summary>
        /// <returns></returns>
        List<MRiseDto> GetAllMRise();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MRiseDto AddMRise(MRiseDto input);
    }
}
