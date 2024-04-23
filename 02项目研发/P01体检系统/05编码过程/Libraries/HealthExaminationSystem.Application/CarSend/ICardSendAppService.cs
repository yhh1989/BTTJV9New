using Abp.Application.Services;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.Domain.Entities;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend
{
   public  interface ICardSendAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取所有卡类别列表
        /// </summary>
        /// <returns></returns>
        List<SimpCardTypeDto> getSimpCardTypeList();

        /// <summary>
        /// 获取所有卡列表
        /// </summary>
        /// <returns></returns>
        List<SimpCardListDto> getSimpCardList(SearchCardDto input);
        /// <summary>
        /// 保存卡信息
        /// </summary>
        /// <returns></returns>
        SaveTbmCardDto SaveTbmCard(SaveTbmCardDto input);
        /// <summary>
        /// 获取卡信息
        /// </summary>
        /// <returns></returns>
        SaveTbmCardDto getTbmCard(SaveTbmCardDto input);
        /// <summary>
        /// 注销卡
        /// </summary>
        /// <param name="input"></param>

        void OffCard(EntityDto<Guid> input);
        /// <summary>
        /// 体检卡查询 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         List<OutSeachCardDto> getSeachCardList(InCardSearchDto input);
    }
}
