using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation
{
   public interface IOccdiseaseConsulitationAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        OccdieaseBasicInformationDto GetAllCustomer(OccdieaseBasicGet input);
        void SaveCustomer(SaveCusDto input);
        OccQuestionnaireDto GetAllOccupationHistory(OccdieaseHistoryRucan input);

        OccQuesPastHistoryDto Add(OccQuestionPastAddrucan input);

        OccQuesFamilyHistoryDto AddFamily(OccQuestionFamilyrucanDto input);

        OccQuesSymptomDto AddSymptom(OccQustionSymptomrucanDto input);
        OccQuestionnaireDto AddData(dynamic DynamicData);
    }
}
