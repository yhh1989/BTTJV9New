using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.MemberShipCard
{
   public class MemberShipCardAppService : AppServiceApiProxyBase, IMemberShipCardAppService
    {
        public TbmCardTypeDto Add(FullTbmCardTypeDto input)
        {
            return GetResult<FullTbmCardTypeDto,TbmCardTypeDto> (input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ShowTbmCardTypeDto> GetTbmCardTypes(SearchTbmCardTypeDto input)
        {
            return GetResult<SearchTbmCardTypeDto, List<ShowTbmCardTypeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public TbmCardTypeDto GetTbmCardType(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, TbmCardTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public TbmCardTypeDto Edit(FullTbmCardTypeDto input)
        {
            return GetResult<FullTbmCardTypeDto, TbmCardTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public int? GetAll()
        {
            return GetResult<int?>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<ItemSuitsDto> GetItemSuits(ItemSuitsDto input)
        {
            return GetResult<ItemSuitsDto,List<ItemSuitsDto>>(input,DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
