using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup
{
    public class ItemGroupAppService : AppServiceApiProxyBase, IItemGroupAppService
    {
        public FullItemGroupDto Add(ItemGroupInput input)
        {
            return GetResult<ItemGroupInput, FullItemGroupDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullItemGroupDto Edit(ItemGroupInput input)
        {
            return GetResult<ItemGroupInput, FullItemGroupDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullItemGroupDto Get(SearchItemGroupDto input)
        {
            return GetResult<SearchItemGroupDto, FullItemGroupDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SimpleItemGroupDto> QuerySimples(SearchItemGroupDto input)
        {
            return GetResult<SearchItemGroupDto, List<SimpleItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ItemGroupDto> QueryNatives(SearchItemGroupDto input)
        {
            return GetResult<SearchItemGroupDto, List<ItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<FullItemGroupDto> QueryFulls(SearchItemGroupDto input)
        {
            return GetResult<SearchItemGroupDto, List<FullItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<ItemGroupDto> PageNatives(PageInputDto<SearchItemGroupDto> input)
        {
            return GetResult<PageInputDto<SearchItemGroupDto>, PageResultDto<ItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<FullItemGroupDto> PageFulls(PageInputDto<SearchItemGroupDto> input)
        {
            return GetResult<PageInputDto<SearchItemGroupDto>, PageResultDto<FullItemGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FullItemGroupDto> GetAll()
        {
            return GetResult<List<FullItemGroupDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateOrder(ChargeBM input)
        {
             GetResult<ChargeBM>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void adMLXM(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SimpleItemGroupDto> SimpleGroup()
        {
            return GetResult<List<SimpleItemGroupDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public int? GetMaxOrderNum()
        {
            return GetResult<int?>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<priceSynDto> GetPriceSyns()
        {
            return GetResult< List<priceSynDto>>( DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ChargeBM> getItemNames(SearIdsDto input)
        {
            return GetResult<SearIdsDto, List<ChargeBM>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        public ConfiStrDto getItemConf(ConfiITemDto input)
        {
            return GetResult<ConfiITemDto, ConfiStrDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}