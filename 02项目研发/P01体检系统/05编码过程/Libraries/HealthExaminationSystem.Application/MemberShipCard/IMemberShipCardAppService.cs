using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard
{
   public interface IMemberShipCardAppService
#if !Proxy
        : IApplicationService
#endif
    {
        TbmCardTypeDto Add(FullTbmCardTypeDto input);

        List<ShowTbmCardTypeDto> GetTbmCardTypes(SearchTbmCardTypeDto input);

        void Del(EntityDto<Guid> input);

        TbmCardTypeDto GetTbmCardType(EntityDto<Guid> input);

        TbmCardTypeDto Edit(FullTbmCardTypeDto input);

        int? GetAll();
        List<ItemSuitsDto> GetItemSuits(ItemSuitsDto input);
        

    }
}
