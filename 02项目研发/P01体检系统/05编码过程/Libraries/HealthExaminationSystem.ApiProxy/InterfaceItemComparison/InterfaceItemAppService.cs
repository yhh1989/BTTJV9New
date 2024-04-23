using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.InterfaceItemComparison
{
    public class InterfaceItemAppService : AppServiceApiProxyBase, IInterfaceItemAppService
    {
        public List<InterfaceItemsDto> GetInterfaceItemComparison(SearchInterIFaceItemComparisonDto input)
        {
            return GetResult<SearchInterIFaceItemComparisonDto, List<InterfaceItemsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public InterfaceItemsDto SaveInterfaceItems(InsertInterfaceItemDto input)
        {
            return GetResult<InsertInterfaceItemDto, InterfaceItemsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<InterfaceItemGroupsDto> GetInterfaceItemGroupComparison(SearchInterIFaceItemComparisonDto input)
        {
            return GetResult<SearchInterIFaceItemComparisonDto, List<InterfaceItemGroupsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public InterfaceItemGroupsDto SaveInterfaceItemGroups(InsertInterfaceItemGroupDto input)
        {
            return GetResult<InsertInterfaceItemGroupDto, InterfaceItemGroupsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<InterfaceUserDto> getInterfaceUser(SearchInterIFaceItemComparisonDto input)
        {
            return GetResult<SearchInterIFaceItemComparisonDto, List<InterfaceUserDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public InterfaceUserDto SaveInterfaceUser(InsertInterfaceEmpDto input)
        {
            return GetResult<InsertInterfaceEmpDto, InterfaceUserDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public InterfaceItemsDto getInterfaceItems(SearchInterfaceItemDto input)
        {
            return GetResult<SearchInterfaceItemDto, InterfaceItemsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public InterfaceItemGroupsDto getInterfaceItemGroups(SearchInterfaceItemDto input)
        {
            return GetResult<SearchInterfaceItemDto, InterfaceItemGroupsDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public InterfaceUserDto getInterfaceEmp(SearchInterfaceEmpDto input)
        {
            return GetResult<SearchInterfaceEmpDto, InterfaceUserDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void delInterface(ChargeBM input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
