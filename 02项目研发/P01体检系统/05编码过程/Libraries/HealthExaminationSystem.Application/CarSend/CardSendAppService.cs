using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend
{
    [AbpAuthorize]
    public class CardSendAppService : MyProjectAppServiceBase, ICardSendAppService
    {
        private readonly IRepository<TbmCardType, Guid> _tbmCardType;
        private readonly IRepository<TbmCard, Guid> _tbmTbmCard;
        public CardSendAppService(IRepository<TbmCardType, Guid> tbmCardType,
            IRepository<TbmCard, Guid> tbmTbmCard
         )
        {
            _tbmCardType = tbmCardType;
            _tbmTbmCard = tbmTbmCard;
        }
        /// <summary>
        /// 获取所有卡类别列表
        /// </summary>
        /// <returns></returns>
        public List<SimpCardTypeDto> getSimpCardTypeList()
        {

            var que = _tbmCardType.GetAll().Where(o => o.Available == 1).ToList();
            return que.MapTo<List<SimpCardTypeDto>>();
        }
        /// <summary>
        /// 获取所有卡列表
        /// </summary>
        /// <returns></returns>
        public List<SimpCardListDto> getSimpCardList(SearchCardDto input)
        {
            

            var que = _tbmTbmCard.GetAll().Where(o => o.Available == 1);
            if (input.Id != Guid.Empty)
            {
                var re = que.Where(o=>o.Id==input.Id);
            }
            if (!string.IsNullOrEmpty(input.CardNo))
            {
                que = que.Where(o=>o.CardNo==input.CardNo);
            }
            if (input.CardType.HasValue)
            {
                que = que.Where(o => o.CardTypeId == input.CardType.Value);
            }
            if (input.CreatUser.HasValue)
            {
                que = que.Where(o => o.CreateCardUserId == input.CreatUser.Value);
            }
            if (input.SendUser.HasValue)
            {
                que = que.Where(o => o.SellCardUserId == input.SendUser.Value);
            }
            if (input.SendType.HasValue && input.SendType!=3)
            {
                que = que.Where(o => o.PayType == input.SendType.Value);
            }
            var reout = que.Select(o => new SimpCardListDto { CardNo = o.CardNo,
                CardTypeName = o.CardType.CardName,
                PayTypefomart = o.PayType == 1 ? "先反款" : "后返款",
                ChageUser = o.SellCardUser.Name,
                CreateCardUserName = o.CreateCardUser.Name,
                CreateTime = o.CreationTime,
                EndTime = o.EndTime,
                FaceValue = o.FaceValue,
                 Id=o.Id,
                  SellMoney=o.SellMoney,
                   CardCategory=o.CardCategory,
                    ClientRegId=o.ClientRegId,
                     ClientTeamInfoId=o.ClientTeamInfoId,
                      DiscountRate=o.DiscountRate
}).OrderByDescending(o => o.CreateTime).ToList();
            return reout;
        }
        /// <summary>
        /// 保存卡信息
        /// </summary>
        /// <returns></returns>
        public SaveTbmCardDto SaveTbmCard(SaveTbmCardDto input)
        {
            var Entity = new SaveTbmCardDto();          
            if (input.Id != Guid.Empty)
            {
                var card = _tbmTbmCard.Get(input.Id);
                 card= input.MapTo(card);
                _tbmTbmCard.Update(card);
                Entity = card.MapTo<SaveTbmCardDto>();
            }
            else
            {                
                var tbmCard=  input.MapTo<TbmCard>();
                tbmCard.Id = Guid.NewGuid();
                _tbmTbmCard.Insert(tbmCard);
            }
            return Entity;
        }
        /// <summary>
        /// 获取卡信息
        /// </summary>
        /// <returns></returns>
        public SaveTbmCardDto getTbmCard(SaveTbmCardDto input)
        {
            return _tbmTbmCard.Get(input.Id).MapTo<SaveTbmCardDto>();
        }
        /// <summary>
        /// 注销卡
        /// </summary>
        /// <returns></returns>
        public void OffCard(EntityDto<Guid> input)
        {
            var tbmcard = _tbmTbmCard.Get(input.Id);
            tbmcard.Available = 0;
            _tbmTbmCard.Update(tbmcard);
           
        }
        /// <summary>
        /// 体检卡查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutSeachCardDto> getSeachCardList(InCardSearchDto input)
        {
            var list = _tbmTbmCard.GetAll();
            if (!string.IsNullOrEmpty(input.CardNo))
            {
                list = list.Where(o=>o.CardNo==input.CardNo);
            }
            if (input.CardTypeId != null && input.CardTypeId != Guid.Empty)
            {
                list = list.Where(o => o.CardTypeId==input.CardTypeId);
            }
            if (input.CreateCardUserId.HasValue )
            {
                list = list.Where(o => o.CreateCardUserId == input.CreateCardUserId);
            }
            if (input.CendTime.HasValue && input.CstarTime.HasValue)
            {
                list = list.Where(o => o.CreationTime>= input.CstarTime && o.CreationTime < input.CendTime);
            }
            if (input.HasUse.HasValue && input.HasUse!=2)
            {
                if(input.HasUse == 1)
                list = list.Where(o =>o.HasUse==1);
                else
                 list = list.Where(o => o.HasUse != 1);
            }
            if (input.PayType==1 || input.PayType == 2)
            {
                list = list.Where(o => o.PayType == input.PayType);
            }
            if (input.Available.HasValue && input.Available!=2)
            {
                list = list.Where(o => o.Available == input.Available);
            }
            if (input.SellCardUserId.HasValue)
            {
                list = list.Where(o => o.SellCardUserId == input.SellCardUserId);
            }
            if (input.UsestarTime.HasValue && input.UsesendTime.HasValue)
            {
                list = list.Where(o => o.UseTime >= input.UsestarTime && o.UseTime< input.UsesendTime);
            }
            var outlist = list.Select(o => new OutSeachCardDto
            {
                Available = o.Available==1?"未注销":"已注销",
                UseTime = o.UseTime,
                CardNo = o.CardNo,
                CardTypeName = o.CardType.CardName,
                CreateCardUsername = o.CreateCardUser.Name,
                CustomerBM = o.CustomerReg==null?"": o.CustomerReg.CustomerBM,
                CustomerName = o.CustomerReg == null ? "" : o.CustomerReg.Customer.Name,
                EndTime = o.EndTime,
                FaceValue = o.FaceValue,
                HasUse = o.HasUse == 1 ? "已使用" : "未使用",
                Id = o.Id,
                PayType = o.PayType == 1 ? "先反款" : "后返款",
                SellCardUserName = o.SellCardUser.Name,
                SellMoney = o.SellMoney,
                CreatTime = o.CreationTime


            }).OrderByDescending(o => o.CreatTime).ToList();
            return outlist;
        }

    }
}
