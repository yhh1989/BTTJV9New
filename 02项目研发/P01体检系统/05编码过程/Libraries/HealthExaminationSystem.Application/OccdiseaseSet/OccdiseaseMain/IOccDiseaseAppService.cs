using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain
{
  public  interface IOccDiseaseAppService
#if !Proxy
         : Abp.Application.Services.IApplicationService
#endif
    {
        List<OutOccDiseaseDto> GetAllOccDisease(Occdieaserucan input);

        List<OutOccDiseaseDto> Get();

        OutOccDiseaseDto Add(OccDieaseAndStandardDto input);

        void OccDel(EntityDto<Guid> input);

        OutOccDiseaseDto Edit(OccDieaseAndStandardDto input);

        OutOccDiseaseDto GetById(EntityDto<Guid> input);

        List<OccDiseaseStandardDto> GetStandard();
    }
}
