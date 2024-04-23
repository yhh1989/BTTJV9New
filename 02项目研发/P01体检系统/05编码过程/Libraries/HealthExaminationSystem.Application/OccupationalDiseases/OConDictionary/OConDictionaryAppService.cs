using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor
{
    [AbpAuthorize]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class OConDictionaryAppService : MyProjectAppServiceBase, IOConDictionaryAppService
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly IRepository<TbmOConDictionary, Guid> _riskfactorrepository;

        private readonly IRepository<TbmZYBType, Guid> _TbmZYBTypeDto;

        public OConDictionaryAppService(IRepository<TbmOConDictionary, Guid> riskfactorrepository,
             IRepository<TbmZYBType, Guid> TbmZYBTypeDto)
        {
            _riskfactorrepository = riskfactorrepository;
            _TbmZYBTypeDto = TbmZYBTypeDto;
        }

       
        /// <summary>
        /// 危机因素添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(OConDictionaryDto input)
        {
             _riskfactorrepository.Insert(input.MapTo<TbmOConDictionary>());
            return true;
        }
        /// <summary>
        /// 危机因素修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OConDictionaryDto Edit(OConDictionaryDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var itemInfo = _riskfactorrepository.Get(input.Id);
                input.MapTo(itemInfo);
                var result = _riskfactorrepository.Update(itemInfo);
                return result.MapTo<OConDictionaryDto>();
            }

        }
        /// <summary>
        /// 获取单一危机因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OConDictionaryDto> Get(OConDictionaryDto input)
        {
            List<OConDictionaryDto> oRiskFactorDtos = new List<OConDictionaryDto>();
            if (input.RiskNames != "")
            {
                var varris = _riskfactorrepository.GetAll().Where(p => p.RiskNames.Contains(input.RiskNames));
                oRiskFactorDtos = varris.MapTo<List<OConDictionaryDto>>();
            }
            else
            {
                var varris = _riskfactorrepository.GetAll().Where(p => p.OPostStates.Contains(input.OPostStates));
                oRiskFactorDtos = varris.MapTo<List<OConDictionaryDto>>();
            }

            return oRiskFactorDtos;


        }
        /// <summary>
        /// 获取全部危机因素
        /// </summary>
        /// <returns></returns>
        public List<OConDictionaryDto> GetAll()
        {
            return (_riskfactorrepository.GetAll()).MapTo<List<OConDictionaryDto>>();
        }

        public void Delete(OConDictionaryDto input)
        {
            var entity = _riskfactorrepository.FirstOrDefault(input.Id);
            if (entity != null)
                _riskfactorrepository.Delete(entity);
        }
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<ZYBTypeDto> ZYBTypeGetAll()
        {
            return (_TbmZYBTypeDto.GetAll().OrderBy(o => o.Order)).MapTo<List<ZYBTypeDto>>();
        }
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<ZYBTypeDto> Get(ZYBTypeDto input)
        {
            List<ZYBTypeDto> ZYBTypeDtos = new List<ZYBTypeDto>();
            if (input.TypeName != "")
            {
                var varris = _TbmZYBTypeDto.GetAll().Where(p => p.TypeName.Contains(input.TypeName)).OrderBy(o=>o.Order);
                ZYBTypeDtos = varris.MapTo<List<ZYBTypeDto>>();
            }
            else if(input.Name != "")
            {
                var varris = _TbmZYBTypeDto.GetAll().Where(p => p.Name.Contains(input.Name)).OrderBy(o => o.Order);
                ZYBTypeDtos = varris.MapTo<List<ZYBTypeDto>>();
            }

            return ZYBTypeDtos;

        }
        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="input"></param>
        public void ZYBTypeDelete(ZYBTypeDto input)
        {
            var entity = _TbmZYBTypeDto.FirstOrDefault(input.Id);
            if (entity != null)
                _TbmZYBTypeDto.Delete(entity);
        }
        /// <summary>
        /// 插入更新更新类别
        /// </summary>
        /// <param name="input"></param>
        public ZYBTypeDto ZYBTypeInsert(ZYBTypeDto input)
        {
            if (input.Id != Guid.Empty)
            {
                var entity = _TbmZYBTypeDto.FirstOrDefault(input.Id);
                if (entity != null)
                {
                    input.MapTo(entity);
                    var result = _TbmZYBTypeDto.Update(entity);
                    return result.MapTo<ZYBTypeDto>();
                }
                else
                {
                    var type = input.MapTo<TbmZYBType>();
                    type.Id = Guid.NewGuid();
                    var sresult = _TbmZYBTypeDto.Insert(type);
                  return sresult.MapTo<ZYBTypeDto>();
                }
            }
           else
            {
                var type = input.MapTo<TbmZYBType>();
                type.Id = Guid.NewGuid();
                var sresult = _TbmZYBTypeDto.Insert(type);
                return sresult.MapTo<ZYBTypeDto>();

            }
          
          
        }


    }
}
