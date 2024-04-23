using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison
{
    public interface IInterfaceItemAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 根据对应的项目id获取项目id
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        List<InterfaceItemsDto> GetInterfaceItemComparison(SearchInterIFaceItemComparisonDto Interfaceresult);
        /// <summary>
        /// 保存项目对应
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        InterfaceItemsDto SaveInterfaceItems(InsertInterfaceItemDto input);
        /// <summary>
        /// 获取指定条件组合对应
        /// </summary>
        /// <param name="GetInterfaceItemComparison"></param>
        /// <returns></returns>
        List<InterfaceItemGroupsDto> GetInterfaceItemGroupComparison(SearchInterIFaceItemComparisonDto Interfaceresult);
        /// <summary>
        /// 保存组合对应
        /// </summary>
        /// <param name="SaveInterfaceItems"></param>
        /// <returns></returns>
        InterfaceItemGroupsDto SaveInterfaceItemGroups(InsertInterfaceItemGroupDto input);
        /// <summary>
        /// 获取用户对应
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        List<InterfaceUserDto> getInterfaceUser(SearchInterIFaceItemComparisonDto Interfaceresult);
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        InterfaceUserDto SaveInterfaceUser(InsertInterfaceEmpDto input);
        /// <summary>
        /// 获取项目对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        InterfaceItemsDto getInterfaceItems(SearchInterfaceItemDto input);
        /// <summary>
        /// 获取组合对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        InterfaceItemGroupsDto getInterfaceItemGroups(SearchInterfaceItemDto input);
        /// <summary>
        /// 获取医生对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        InterfaceUserDto getInterfaceEmp(SearchInterfaceEmpDto input);
        /// <summary>
        /// 删除对应
        /// </summary>
        /// <param name="input"></param>
        void delInterface(ChargeBM input);
    }
}
