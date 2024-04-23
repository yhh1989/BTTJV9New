using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor
{
    public interface IOConDictionaryAppService
#if !Proxy
        : IApplicationService
#endif
    {
        #region 目标疾病
        /// <summary>
        /// 添加目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(OConDictionaryDto input);
        /// <summary>
        /// 删除目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Delete(OConDictionaryDto input);
        /// <summary>
        /// 修改目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OConDictionaryDto Edit(OConDictionaryDto input);
        /// <summary>
        /// 获取条件目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OConDictionaryDto> Get(OConDictionaryDto input);
        /// <summary>
        /// 获取全部目标疾病
        /// </summary>
        List<OConDictionaryDto> GetAll();

        /// <summary>
        /// 插入更新更新类别
        /// </summary>
        /// <param name="input"></param>
        ZYBTypeDto ZYBTypeInsert(ZYBTypeDto input);

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="input"></param>
        void ZYBTypeDelete(ZYBTypeDto input);

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        List<ZYBTypeDto> Get(ZYBTypeDto input);
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        List<ZYBTypeDto> ZYBTypeGetAll();


        #endregion
    }
}
