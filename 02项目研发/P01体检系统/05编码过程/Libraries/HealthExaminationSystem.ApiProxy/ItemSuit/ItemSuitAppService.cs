using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit
{
    public class ItemSuitAppService : AppServiceApiProxyBase, IItemSuitAppService
    {
        public FullItemSuitDto Add(ItemSuitInput input)
        {
            return GetResult<ItemSuitInput, FullItemSuitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullItemSuitDto Edit(ItemSuitInput input)
        {
            return GetResult<ItemSuitInput, FullItemSuitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullItemSuitDto Get(SearchItemSuitDto input)
        {
            return GetResult<SearchItemSuitDto, FullItemSuitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SimpleItemSuitDto> QuerySimples(SearchItemSuitDto input)
        {
            return GetResult<SearchItemSuitDto, List<SimpleItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询简单套餐信息缓存
        /// </summary>
        public List<SimpleItemSuitDto> QuerySimplesCache()
        {
            return GetResult<List<SimpleItemSuitDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SimpleSubsetItemSuitDto> QuerySimpleSubsets(SearchItemSuitDto input)
        {
            return GetResult<SearchItemSuitDto, List<SimpleSubsetItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemSuitDto> QueryNatives(SearchItemSuitDto input)
        {
            return GetResult<SearchItemSuitDto, List<ItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FullItemSuitDto> QueryFulls(SearchItemSuitDto input)
        {
            return GetResult<SearchItemSuitDto, List<FullItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<ItemSuitDto> PageNatives(PageInputDto<SearchItemSuitDto> input)
        {
            return GetResult<PageInputDto<SearchItemSuitDto>, PageResultDto<ItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<FullItemSuitDto> PageFulls(PageInputDto<SearchItemSuitDto> input)
        {
            return GetResult<PageInputDto<SearchItemSuitDto>, PageResultDto<FullItemSuitDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemSuitItemGroupContrastFullDto> GetAllItemGroup()
        {
            return GetResult<List<ItemSuitItemGroupContrastFullDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ItemSuitGroupSimpDto> GetAllSuitItemGroup(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ItemSuitGroupSimpDto>>(input,DynamicUriBuilder.GetAppSettingValue());

        }
        public List<ItemSuitItemGroupContrastFullDto> GetAllSuitItemGroups(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ItemSuitItemGroupContrastFullDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
