using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting
{
    public class BarSettingAppService : AppServiceApiProxyBase, IBarSettingAppService
    {
        public FullBarSettingDto Add(BarSettingInput input)
        {
            return GetResult<BarSettingInput, FullBarSettingDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullBarSettingDto Edit(BarSettingInput input)
        {
            return GetResult<BarSettingInput, FullBarSettingDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullBarSettingDto Get(SearchBarSettingDto input)
        {
            return GetResult<SearchBarSettingDto, FullBarSettingDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SimpleBarSettingDto> QuerySimples(SearchBarSettingDto input)
        {
            return GetResult<SearchBarSettingDto, List<SimpleBarSettingDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<BarSettingDto> QueryNatives(SearchBarSettingDto input)
        {
            return GetResult<SearchBarSettingDto, List<BarSettingDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<FullBarSettingDto> QueryFulls(SearchBarSettingDto input)
        {
            return GetResult<SearchBarSettingDto, List<FullBarSettingDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<BarSettingDto> PageNatives(PageInputDto<SearchBarSettingDto> input)
        {
            return GetResult<PageInputDto<SearchBarSettingDto>, PageResultDto<BarSettingDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<FullBarSettingDto> PageFulls(PageInputDto<SearchBarSettingDto> input)
        {
            return GetResult<PageInputDto<SearchBarSettingDto>, PageResultDto<FullBarSettingDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<BarCodeViewDto> GetAll(PageInputDto input)
        {
            return GetResult<PageInputDto, PageResultDto<BarCodeViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<BarItembViewDto> GetBarItemGroupFulls(SearchBarItemDto searchBarItemDto)
        {
            return GetResult<SearchBarItemDto, List<BarItembViewDto>>(searchBarItemDto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<BarItemDto> GetBarItems()
        {
            return GetResult<List<BarItemDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
