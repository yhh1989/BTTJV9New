using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Questionnaire
{
    /// <inheritdoc cref="IQuestionnaireAppService" />
    public class QuestionnaireAppService : AppServiceApiProxyBase, IQuestionnaireAppService
    {
        /// <inheritdoc />
        public List<QuestionnaireDto> QueryQuestionnaireRecord(QuestionnaireSearchInput input)
        {
            return GetResult<QuestionnaireSearchInput, List<QuestionnaireDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <inheritdoc />
        public List<QuestionBomDto> QueryQuestionBomRecordByQuestionnaireId(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<QuestionBomDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<QuestiontemReportDto> QueryQuestionBomRecordByRegID(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<QuestiontemReportDto>>(input, DynamicUriBuilder.GetAppSettingValue());


        }

        /// <inheritdoc />
        public bool UpdateQuestionBomRecord(List<QuestionBomDto> input)
        {
            return GetResult<List<QuestionBomDto>, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
