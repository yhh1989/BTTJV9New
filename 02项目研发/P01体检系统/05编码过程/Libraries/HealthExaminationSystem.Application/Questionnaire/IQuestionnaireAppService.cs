using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Questionnaire
{
    /// <summary>
    /// 问卷接口应用服务
    /// </summary>
    public interface IQuestionnaireAppService : IApplicationService
    {
        /// <summary>
        /// 查询问卷记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QuestionnaireDto> QueryQuestionnaireRecord(QuestionnaireSearchInput input);

        /// <summary>
        /// 查询问卷试题列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QuestionBomDto> QueryQuestionBomRecordByQuestionnaireId(EntityDto<Guid> input);
        /// <summary>
        /// 问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QuestiontemReportDto> QueryQuestionBomRecordByRegID(EntityDto<Guid> input);
        /// <summary>
        /// 更新问卷试题内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateQuestionBomRecord(List<QuestionBomDto> input);
    }
}