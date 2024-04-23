using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor
{
    /// <inheritdoc cref="IRiskFactorAppService" />
    [AbpAuthorize]
    public class RiskFactorAppService : MyProjectAppServiceBase, IRiskFactorAppService
    {
        private readonly IRepository<Core.Illness.RiskFactor, Guid> _riskFactorRepository;

        /// <inheritdoc />
        public RiskFactorAppService(IRepository<Core.Illness.RiskFactor, Guid> riskFactorRepository)
        {
            _riskFactorRepository = riskFactorRepository;
        }

        /// <summary>
        /// 危机因素添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ORiskFactorDto input)
        {
            if (_riskFactorRepository.GetAll().Any(r => r.Name == input.Name))
            {
                throw new FieldVerifyException("名称已经存在！", "名称已经存在！");
            }

            var all = _riskFactorRepository.GetAll();
            var entity = input.MapTo<Core.Illness.RiskFactor>();
            entity.Id = Guid.NewGuid();
            entity.Order = all.Count();
            _riskFactorRepository.Insert(entity);
            return true;
        }

        /// <summary>
        /// 危机因素修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ORiskFactorDto Edit(ORiskFactorDto input)
        {
            using(CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var itemInfo = _riskFactorRepository.Get(input.Id);
                input.MapTo(itemInfo);
                var result = _riskFactorRepository.Update(itemInfo);
                return result.MapTo<ORiskFactorDto>();
            }
        }

        /// <summary>
        /// 获取单一危机因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ORiskFactorDto> Get(ORiskFactorDto input)
        {
            var oRiskFactorDtos = new List<ORiskFactorDto>();
            if (input.Name != "")
            {
                var varris = _riskFactorRepository.GetAll().Where(p => p.Name.Contains(input.Name));
                oRiskFactorDtos = varris.MapTo<List<ORiskFactorDto>>();
            }

            return oRiskFactorDtos;
        }

        /// <summary>
        /// 获取全部危机因素
        /// </summary>
        /// <returns></returns>
        public List<ORiskFactorDto> GetAll()
        {
            return _riskFactorRepository.GetAll().MapTo<List<ORiskFactorDto>>();
        }

        public void Delete(ORiskFactorDto input)
        {
            var entity = _riskFactorRepository.FirstOrDefault(input.Id);
            if (entity != null)
            {
                _riskFactorRepository.Delete(entity);
            }
        }
    }
}