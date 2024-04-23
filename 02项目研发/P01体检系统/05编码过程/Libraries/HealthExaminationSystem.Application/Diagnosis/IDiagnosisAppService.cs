using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis
{
    public interface IDiagnosisAppService
#if !Proxy
        : IApplicationService
#endif
    {
        List<ItemInfoGroupDto> QueryInfoGroup(ItemInfoGroupDto input);

        #region 复合判断
        /// <summary>
        /// 查询
        /// </summary>
        PageResultDto<TbmDiagnosisDto> QueryDiagnosis(PageInputDto<TbmDiagnosisDto> input);


        TbmDiagnosisDto GetById(EntityDto<Guid> input);

            /// <summary>
            /// 添加
            /// </summary>
            void InsertDiagnosis(TbmDiagnosisDto input);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteDiagnosis(EntityDto<Guid> input);
        /// <summary>
        /// 获取阳性结果
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        List<CusRegInfoDto> getIllCount(SearchItem intput);


        #endregion
    }
}