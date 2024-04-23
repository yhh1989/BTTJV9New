using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseConsulitation
{
   public class OccdiseaseConsulitationAppService:AppServiceApiProxyBase, IOccdiseaseConsulitationAppService
    {
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <returns></returns>
        public OccdieaseBasicInformationDto GetAllCustomer(OccdieaseBasicGet input)
        {
            return GetResult<OccdieaseBasicGet, OccdieaseBasicInformationDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void SaveCustomer(SaveCusDto input)
        {
              GetResult<SaveCusDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取wenjuan
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuestionnaireDto GetAllOccupationHistory(OccdieaseHistoryRucan input)
        {
            return GetResult<OccdieaseHistoryRucan, OccQuestionnaireDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 添加既往史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuesPastHistoryDto Add(OccQuestionPastAddrucan input)
        {
            return GetResult<OccQuestionPastAddrucan, OccQuesPastHistoryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 添加家族史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuesFamilyHistoryDto AddFamily(OccQuestionFamilyrucanDto input)
        {
            return GetResult<OccQuestionFamilyrucanDto, OccQuesFamilyHistoryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public OccQuesSymptomDto AddSymptom(OccQustionSymptomrucanDto input)
        {
            return GetResult<OccQustionSymptomrucanDto, OccQuesSymptomDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public OccQuestionnaireDto AddData(dynamic DynamicData)
        {
            return GetResult<dynamic, OccQuestionnaireDto>(DynamicData, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
