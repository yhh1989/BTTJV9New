using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto;
using Abp.Application.Services;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClearData
{
   public interface IClearDataAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 时间删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DataBaseDto TimeDeleteData(ClearDataDto input);
        /// <summary>
        /// 删除全部
        /// </summary>
        /// <returns></returns>
        DataBaseDto AllDeleteData(InputClearData input);
        DataBaseDto delTableByTiem(ClearDataDto input);
    }
}
