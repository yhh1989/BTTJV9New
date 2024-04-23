using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CarSend
{
    public class CardSendAppService : AppServiceApiProxyBase, ICardSendAppService
    {
        public List<SimpCardListDto> getSimpCardList(SearchCardDto input)
        {
            return GetResult<SearchCardDto, List<SimpCardListDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SimpCardTypeDto> getSimpCardTypeList()
        {
            return GetResult<List<SimpCardTypeDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public SaveTbmCardDto SaveTbmCard(SaveTbmCardDto input)
        {
            return GetResult<SaveTbmCardDto, SaveTbmCardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public SaveTbmCardDto getTbmCard(SaveTbmCardDto input)
        {
            return GetResult<SaveTbmCardDto, SaveTbmCardDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void OffCard(EntityDto<Guid> input)
        {
             GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutSeachCardDto> getSeachCardList(InCardSearchDto input)
        {
            return GetResult<InCardSearchDto, List<OutSeachCardDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
