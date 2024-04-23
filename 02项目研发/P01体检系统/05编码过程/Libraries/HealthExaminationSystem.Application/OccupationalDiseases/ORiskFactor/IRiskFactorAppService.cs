using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor
{
    /// <summary>
    /// 危害因素应用服务
    /// </summary>
    public interface IRiskFactorAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(ORiskFactorDto input);

        /// <summary>
        /// 删除危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Delete(ORiskFactorDto input);

        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ORiskFactorDto Edit(ORiskFactorDto input);

        /// <summary>
        /// 获取条件危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ORiskFactorDto> Get(ORiskFactorDto input);

        /// <summary>
        /// 获取全部危害因素
        /// </summary>
        List<ORiskFactorDto> GetAll();
    }
}