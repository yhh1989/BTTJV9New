using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard
{
    [AbpAuthorize]
    public class MemberShipCardAppService : MyProjectAppServiceBase, IMemberShipCardAppService
    {
        private readonly IRepository<TbmCardType, Guid> _tbmcardtypeRepository;
        private readonly IRepository<TbmItemSuit, Guid> _itemsuitRepository;

        public MemberShipCardAppService(IRepository<TbmCardType, Guid> tbmcardRepository,
            IRepository<TbmItemSuit, Guid> itemsuitRepository)
        {
            _tbmcardtypeRepository = tbmcardRepository;
            _itemsuitRepository = itemsuitRepository;
        }
        public TbmCardTypeDto Add(FullTbmCardTypeDto input)
        {
            if (_tbmcardtypeRepository.GetAll().Any(r => r.CardNum == input.OneTbmCardType.CardNum))
            {
                throw new FieldVerifyException("体检卡编号重复！", "体检卡编号重复！");
            }
            input.OneTbmCardType.Id = Guid.NewGuid();
            var entity = input.OneTbmCardType.MapTo<TbmCardType>();
            entity.ItemSuits = null;
            entity.ItemSuits = new List<TbmItemSuit>();
            if (input.ManyTbmItemSuit != null && input.ManyTbmItemSuit.Count > 0)
            {               
                foreach (var occdis in input.ManyTbmItemSuit)
                {                   
                    var tbmoccdic = _itemsuitRepository.Get(occdis);
                    entity.ItemSuits.Add(tbmoccdic);
                }
            }
            entity = _tbmcardtypeRepository.Insert(entity);
            var result = entity.MapTo<TbmCardTypeDto>();
            return result;
        }
        public List<ShowTbmCardTypeDto> GetTbmCardTypes(SearchTbmCardTypeDto input)
        {
            var query = _tbmcardtypeRepository.GetAll();
            if (input.CardNum!=0)
            {
                query = query.Where(i => i.CardNum == input.CardNum);
            }
            if (!string.IsNullOrWhiteSpace(input.CardName))
                query = query.Where(o => o.CardName.Contains(input.CardName)); ;
            if (input.Available == 0)
            {
                query = query.Where(i => i.Available == input.Available);
            }
            if (input.Available == 1)
            {
                query = query.Where(i => i.Available == input.Available);
            }
            if (input.StartCheckDate!=null)
                query = query.Where(o => o.CreationTime >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CreationTime <= input.EndCheckDate.Value);

            

            query = query.OrderByDescending(o => o.CreationTime);
            return query.MapTo<List<ShowTbmCardTypeDto>>();
        }

        public void Del(EntityDto<Guid> input)
        {
            _tbmcardtypeRepository.Delete(input.Id);
        }

        public TbmCardTypeDto GetTbmCardType(EntityDto<Guid> input)
        {
            var data = _tbmcardtypeRepository.Get(input.Id);
            return data.MapTo<TbmCardTypeDto>();
        }

        public TbmCardTypeDto Edit(FullTbmCardTypeDto input)
        {                        
            if (input.OneTbmCardType != null && input.OneTbmCardType.Id != null)
            {
                var occtarget = _tbmcardtypeRepository.Get(input.OneTbmCardType.Id);
                input.OneTbmCardType.MapTo(occtarget);
                
                //occtarget.ItemSuits.Clear();
                occtarget.ItemSuits = new List<TbmItemSuit>();
                if (input.ManyTbmItemSuit != null && input.ManyTbmItemSuit.Count() > 0)
                {
                    
                    foreach (var occdis in input.ManyTbmItemSuit)
                    {
                        var tbmoccdic = _itemsuitRepository.Get(occdis);
                        occtarget.ItemSuits.Add(tbmoccdic);

                    }
                }


                //input.ManyTbmItemSuit.MapTo(occtarget);
                var entitys= _tbmcardtypeRepository.Update(occtarget);
                CurrentUnitOfWork.SaveChanges();
                
                var result = entitys.MapTo<TbmCardTypeDto>();
                return result;
            }
            else
            {
                return new TbmCardTypeDto();
            }
        }

        public int? GetAll()
        {
            var result = _tbmcardtypeRepository.Count();
            if (result != 0)
            {
                 result = _tbmcardtypeRepository.GetAll().Max(O => O.CardNum);
            }
            return result;
        }

        public List<ItemSuitsDto> GetItemSuits(ItemSuitsDto input)
        {
            var query = _itemsuitRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ItemSuitName))
            {
                query = query.Where(o => o.ItemSuitName.Contains(input.ItemSuitName));
            }
            query = query.OrderBy(o => o.OrderNum);
            return query.MapTo<List<ItemSuitsDto>>();
        }
    }
}
