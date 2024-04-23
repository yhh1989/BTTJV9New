using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison
{
   public interface IInterfaceItemGroupComparison
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 根据对应的项目id获取项目id
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        InterfaceItemGroupComparisonDto Get(TInterfaceresult Interfaceresult);
    }
}
