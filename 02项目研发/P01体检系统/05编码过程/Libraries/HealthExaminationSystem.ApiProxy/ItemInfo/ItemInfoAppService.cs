using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo
{
    public class ItemInfoAppService : AppServiceApiProxyBase, IItemInfoAppService
    {
        public ItemInfoDto AddItemInfo(ItemInfoDto input)
        {
            return GetResult<ItemInfoDto, ItemInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ItemStandardDto AddItemStandard(ItemStandardDto input)
        {
            return GetResult<ItemStandardDto, ItemStandardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemInfoViewDto> GetAll()
        {
            return GetResult<List<ItemInfoViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public ItemInfoDto GetById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ItemInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteItemInfo(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteItemStandard(ItemStandardDto input)
        {
            GetResult<ItemStandardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ItemInfoDto EditItemInfo(ItemInfoDto input)
        {
            return GetResult<ItemInfoDto, ItemInfoDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ItemStandardDto EditItemStandard(ItemStandardDto input)
        {
            return GetResult<ItemStandardDto, ItemStandardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<DepartmentIdNameDto> DepartmentGetAll()
        {
            return GetResult<List<DepartmentIdNameDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemInfoViewDto> QueryItemInfo(SearchItemInfoDto input)
        {
            return GetResult<SearchItemInfoDto, List<ItemInfoViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemStandardDto> QueryItemStandardByItemId(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<ItemStandardDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemStandardDto> QueryItemStandardBySum()
        {
            return GetResult< List<ItemStandardDto>>( DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemInfoSimpleDto> QuerySimples()
        {
            return GetResult<List<ItemInfoSimpleDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 更新计算型项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ItemProcExpressDto SaveItemExpress(ItemProcExpressDto input)
        {
            return GetResult<ItemProcExpressDto, ItemProcExpressDto> (input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void DelteItemExpress(EntityDto<Guid> input)
        {
             GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateOrder(ChargeBM input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemStandardDto> GetAllItemStandard()
        {
            throw new NotImplementedException();
        }
        public int? GetMaxOrderNum()
        {
           return GetResult<int?>(DynamicUriBuilder.GetAppSettingValue());
        }
        public void InputUnit(UpItemUnit input)
        {
              //GetResult<UpItemUnit>(DynamicUriBuilder.GetAppSettingValue());

            GetResult<UpItemUnit>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
