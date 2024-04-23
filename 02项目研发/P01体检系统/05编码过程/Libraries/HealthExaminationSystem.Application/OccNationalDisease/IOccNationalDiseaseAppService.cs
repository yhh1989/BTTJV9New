using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease
{
    public interface IOccNationalDiseaseAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {
        /// <summary>
        /// 用人单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DataNodeDto GetEnterpriseInfo(InSearchDto input);
        /// <summary>
        /// 职业健康档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        XMLDataNodeDto GetExamRecord(InSearchDto input);
        /// <summary>
        /// 保存国家疾控设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CreateCountrySet SaveCountry(CreateCountrySet input);
        /// <summary>
        /// 获取国家疾控设置
        /// </summary>
        /// <returns></returns>

        CreateCountrySet GetCountry();
    }
}
