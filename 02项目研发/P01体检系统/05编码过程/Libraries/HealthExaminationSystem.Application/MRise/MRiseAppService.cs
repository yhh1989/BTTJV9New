using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.MRise
{
    [AbpAuthorize]
    public class MRiseAppService : MyProjectAppServiceBase, IMRiseAppService
    {
        private readonly IRepository<TjlMRise, Guid> _tjlMRiseRepository;
        public MRiseAppService(IRepository<TjlMRise, Guid> tjlMRiseRepository)
        {
            _tjlMRiseRepository = tjlMRiseRepository;
        }
        public MRiseDto AddMRise(MRiseDto input)
        {
            var exist = _tjlMRiseRepository.GetAll().Any(t=>t.Name==input.Name);
            if(exist)
                throw new FieldVerifyException("抬头名称已存在！", "抬头名称已存在！");
            input.Id = Guid.NewGuid();
            var result= _tjlMRiseRepository.Insert(input.MapTo<TjlMRise>());
            return result.MapTo<MRiseDto>();
        }

        public List<MRiseDto> GetAllMRise()
        {
            var data = _tjlMRiseRepository.GetAll();
            return data.MapTo<List<MRiseDto>>();
        }
    }
}
