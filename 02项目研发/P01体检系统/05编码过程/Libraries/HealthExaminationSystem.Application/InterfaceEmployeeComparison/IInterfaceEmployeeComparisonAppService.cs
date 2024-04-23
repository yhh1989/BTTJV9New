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
   public interface IInterfaceEmployeeComparison
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 根据对应的项目id获取检查
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        InterfaceEmployeeComparisonDto GetJC(TInterfaceresult Interfaceresult);
        /// <summary>
        /// 根据对应的项目id获取审核
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        InterfaceEmployeeComparisonDto GetSH(TInterfaceresult Interfaceresult);
    }
}
