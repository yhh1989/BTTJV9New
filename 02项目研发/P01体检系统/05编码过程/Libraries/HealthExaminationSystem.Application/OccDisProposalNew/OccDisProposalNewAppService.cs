using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew
{
    [AbpAuthorize]
    public  class OccDisProposalNewAppService : MyProjectAppServiceBase, IOccDisProposalNewAppService
    {
        private readonly IRepository<TbmOccDictionary, Guid> _tbmoccdictionaryRepository;
         

        public OccDisProposalNewAppService(IRepository<TbmOccDictionary, Guid> tbmoccdictionaryRepository)     
        {
            _tbmoccdictionaryRepository = tbmoccdictionaryRepository;            
        }
        public TbmOccDictionaryDto Add(TbmOccDictionaryDto input)
        {
            if (input.Id == Guid.Empty) input.Id = Guid.NewGuid();
            var entity = input.MapTo<TbmOccDictionary>();
            var entity1 = _tbmoccdictionaryRepository.Insert(entity);
            var dto = entity.MapTo<TbmOccDictionaryDto>();
            return dto;

        }

        public TbmOccDictionaryDto Edit(TbmOccDictionaryDto input)
        {
            var entity = _tbmoccdictionaryRepository.Get(input.Id);
            input.MapTo(entity); // 赋值
            entity = _tbmoccdictionaryRepository.Update(entity);
            var dto = entity.MapTo<TbmOccDictionaryDto>();
            return dto;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void SaveDictionary( List<TbmOccDictionaryDto> inputlist)
        {
            var type = inputlist.FirstOrDefault()?.Type;
            //先删除没有平台编码的数据
            if (type == "WorkType")
            {
                _tbmoccdictionaryRepository.GetAll().Where(p => type== "WorkType" &&  (p.code == null
                || p.code == "")).Update(r => new TbmOccDictionary { IsDeleted = true });
                CurrentUnitOfWork.SaveChanges();
            }

            //获取最大序号
            var maxxh = _tbmoccdictionaryRepository.GetAll().Where(p=>p.Type== type).Max(p=>p.OrderNum);
            if (!maxxh.HasValue)
            {
                maxxh = 0;
            }
            else
            { maxxh = maxxh + 1; }
           

            var occlist = _tbmoccdictionaryRepository.GetAll().Where(p => p.Type == type).ToList();

            foreach (var input in inputlist)
            {
                var entity = occlist.FirstOrDefault(p=>p.Text== input.Text && p.Type== input.Type);
                if (entity != null)
                {
                    input.Id = entity.Id;
                    if (!string.IsNullOrEmpty(entity.HelpChar) &&  string.IsNullOrEmpty(input.HelpChar) )
                    {
                        input.HelpChar = entity.HelpChar;
                    }
                    if (!string.IsNullOrEmpty(entity.code) && string.IsNullOrEmpty(input.code))
                    { input.code = entity.code; }
                    input.OrderNum = entity.OrderNum;
                    if (!string.IsNullOrEmpty(entity.Remarks) && string.IsNullOrEmpty(input.Remarks))
                    {
                        input.Remarks = entity.Remarks;
                    }
                    input.MapTo(entity); // 赋值
                    
                    if (entity.Remarks == null)
                    {
                        entity.Remarks = "";
                    }
                   entity = _tbmoccdictionaryRepository.Update(entity);
                }
                else
                {
                   input.Id = Guid.NewGuid();
                    var entityIN = input.MapTo<TbmOccDictionary>();
                    entityIN.OrderNum = maxxh;
                    if (entityIN.Remarks == null)
                    {
                        entityIN.Remarks = "";
                    }
                    var entity1 = _tbmoccdictionaryRepository.Insert(entityIN);
                    maxxh = maxxh + 1;
                }
   
            }
            
        }

        public List<ShowOccDictionary> GetAll(ShowOccDictionary input)
        {
            var OccDictionary = _tbmoccdictionaryRepository.GetAllIncluding();
            if (!string.IsNullOrWhiteSpace(input.Text))
                OccDictionary = OccDictionary.Where(i => i.Text == input.Text);
            if (input.IsActive == 3)
            {
                OccDictionary = OccDictionary;
            }
            else if(input.IsActive == 0)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            }
            else if (input.IsActive == 1)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            }
            if (input.Type != null)
                OccDictionary = OccDictionary.Where(i => i.Type == input.Type);
            OccDictionary = OccDictionary.OrderBy(m => m.OrderNum);
            var result = OccDictionary.Select(o=> new ShowOccDictionary {  HelpChar=o.HelpChar,
             ParentName=o.Parent.Text,Text=o.Text,OrderNum=o.OrderNum,IsActive=o.IsActive,Id=o.Id, code=o.code} ).ToList();
            return result;
        }

        public void Del(EntityDto<Guid> input)
        {
            _tbmoccdictionaryRepository.Delete(input.Id);
        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public TbmOccDictionaryDto GetOccDictionarys(EntityDto<Guid> input)
        {            
                var data = _tbmoccdictionaryRepository.Get(input.Id);
                return data.MapTo<TbmOccDictionaryDto>();          
        }      
        /// <summary>
        /// 根据类别获取相应字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  List< OutOccDictionaryDto> getOutOccDictionaryDto(ChargeBM input)
        {
            var que = _tbmoccdictionaryRepository.GetAll().Where(o => o.IsActive == 1 && o.Type == input.Name).Select(o => new OutOccDictionaryDto
            {
                HelpChar = o.HelpChar,
                Id = o.Id,
                OrderNum = o.OrderNum,
                ParentName = o.Parent==null?"":o.Parent.Text,
                Text = o.Text,
                 Remarks=o.Remarks
            }).OrderBy(p=>p.OrderNum).ToList();
            return que;
        }
    }
}
