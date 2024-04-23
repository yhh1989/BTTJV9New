using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary
{
    [AbpAuthorize]
    public class BasicDictionaryAppService : MyProjectAppServiceBase, IBasicDictionaryAppService
    {
        private readonly IRepository<TbmBasicDictionary, Guid> _basicDictionaryRepository;

        public BasicDictionaryAppService(IRepository<TbmBasicDictionary, Guid> basicDictionaryRepository)
        {
            _basicDictionaryRepository = basicDictionaryRepository;
        }

        public BasicDictionaryDto Add(CreateBasicDictionaryDto input)
        {
            if (string.IsNullOrEmpty(input.Text))
            {
                throw new FieldVerifyException("[文本]不可为空！", "[文本]不可为空！");
            }
            if (string.IsNullOrEmpty(input.Type))
            {
                throw new FieldVerifyException("[字典类型]不可为空！", "[字典类型]不可为空！");
            }
            if (_basicDictionaryRepository.GetAll()
                .Any(r => r.Value == input.Value && r.Type == input.Type))
                throw new FieldVerifyException("[值]重复！", "[值]重复！");
            if (_basicDictionaryRepository.GetAll()
                .Any(r => r.Text == input.Text && r.Type == input.Type))
                throw new FieldVerifyException("[文本]重复！", "[文本]重复！");
            
            var entity = input.MapTo<TbmBasicDictionary>();
            entity.Id = Guid.NewGuid();
            entity = _basicDictionaryRepository.Insert(entity);
            return entity.MapTo<BasicDictionaryDto>();
        }

        public void Del(EntityDto<Guid> input)
        {
            _basicDictionaryRepository.Delete(input.Id);
        }

        public BasicDictionaryDto Edit(UpdateBasicDictionaryDto input)
        {
            if (_basicDictionaryRepository.GetAll().Any(r =>
                r.Value == input.Value && r.Id != input.Id&&r.Type==input.Type))
                throw new FieldVerifyException("[值]重复！", "[值]重复！");
            if (_basicDictionaryRepository.GetAll().Any(r =>
                r.Text == input.Text && r.Id != input.Id && r.Type == input.Type))
                throw new FieldVerifyException("[文本]重复！", "[文本]重复！");
            var entity = _basicDictionaryRepository.Get(input.Id);
            input.MapTo(entity);
            entity = _basicDictionaryRepository.Update(entity);
            return entity.MapTo<BasicDictionaryDto>();
        }

        public BasicDictionaryDto Get(EntityDto<Guid> input)
        {
            var result = _basicDictionaryRepository.Get(input.Id);
            return result.MapTo<BasicDictionaryDto>();
        } 

        public List<BasicDictionaryDto> Query(BasicDictionaryInput input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<BasicDictionaryDto>>();
        }
        /// <summary>
        /// 查询字典缓存数据
        /// </summary>
        /// <returns></returns>
        public async  Task<List<BasicDictionaryDto>> QueryCache()
        {
            var query =  _basicDictionaryRepository.GetAll().AsNoTracking();
            query = query.OrderBy(m => m.OrderNum).ThenBy(m => m.Value).ThenByDescending(m => m.CreationTime);
            return await query.ProjectToListAsync<BasicDictionaryDto>(GetConfigurationProvider<Core.Coding.TbmBasicDictionary, BasicDictionaryDto>());
            //return query.MapTo<List<BasicDictionaryDto>>();
        }
        private IQueryable<TbmBasicDictionary> BuildQuery(BasicDictionaryInput input)
        {
            var query = _basicDictionaryRepository.GetAll().Where(m => m.Type == input.Type);
            if (input.BasicDictionary != null)
            {
                if (input.BasicDictionary.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.BasicDictionary.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty(input.BasicDictionary.Text))
                        query = query.Where(m => m.Text.Contains(input.BasicDictionary.Text));
                    if (!string.IsNullOrEmpty(input.BasicDictionary.Remarks))
                        query = query.Where(m => m.Remarks.Contains(input.BasicDictionary.Remarks));
                }
            }

            query = query.OrderBy(m => m.OrderNum).ThenBy(m => m.Value).ThenByDescending(m => m.CreationTime);
            return query;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void InExcel(List<CreateBasicDictionaryDto> inputlist)
        {
            var type = inputlist.FirstOrDefault()?.Type;

            //获取最大序号
            var dd = _basicDictionaryRepository.GetAll().Where(p => p.Type == type).ToList();
            var maxxh = _basicDictionaryRepository.GetAll().Where(p=>p.Type== type).Max(p => p.OrderNum);
            if (!maxxh.HasValue)
            {
                maxxh = 0;
            }
            maxxh = maxxh + 1;
            int maxvalue = 0;
            var st = _basicDictionaryRepository.GetAll().Where(p => p.Type == type).Count();
            if (st!=0)
            { maxvalue = _basicDictionaryRepository.GetAll().Where(p => p.Type == type).Max(p => p.Value); }
 
            
            maxvalue = maxvalue + 1;
            var occlist = _basicDictionaryRepository.GetAll().Where(p => p.Type == type).ToList();

            foreach (var input in inputlist)
            {
                var entity = occlist.FirstOrDefault(p => p.Text == input.Text && p.Type == input.Type);
                if (entity != null)
                {                     
                    
                    if (!string.IsNullOrEmpty(entity.Code) && string.IsNullOrEmpty(input.Code))
                    { input.Code = entity.Code; }
                    input.OrderNum = entity.OrderNum;
                    input.Value = entity.Value;
                    input.MapTo(entity); // 赋值

                    entity = _basicDictionaryRepository.Update(entity);
                }
                else
                {
                 
                    var entityIN = input.MapTo<TbmBasicDictionary>();
                    entityIN.Id= Guid.NewGuid();
                    entityIN.OrderNum = maxxh;
                    entityIN.Value = maxvalue;
                    var entity1 = _basicDictionaryRepository.Insert(entityIN);
                    maxxh = maxxh + 1;
                    maxvalue = maxvalue + 1;
                }

            }

        }

    }
}