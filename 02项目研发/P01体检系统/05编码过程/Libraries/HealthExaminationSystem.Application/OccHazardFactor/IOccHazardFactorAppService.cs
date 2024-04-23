using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor
{
    public interface IOccHazardFactorAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutOccHazardFactorDto Add(FullHarardFactor input);
        /// <summary>
        /// 添加照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        RadiationDto AddRadiation(RadiationDto input);
        /// <summary>
        /// 显示照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<RadiationDto> ShowRadiation(RadiationDto input);
        /// <summary>
        /// 删除照射种类
        /// </summary>
        /// <param name="input"></param>
        void DelRadiation(EntityDto<Guid> input);
        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        RadiationDto EditRadiation(RadiationDto input);
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        RadiationDto GetRadiationDto(EntityDto<Guid> input);
        /// <summary>
        /// 删除危害因素
        /// </summary>
        /// <param name="input"></param>
        void Del(EntityDto<Guid> input);

        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutOccHazardFactorDto Edit(FullHarardFactor input);

        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>s
        /// <returns></returns>
        OutOccHazardFactorDto GetOccHazardFactor(EntityDto<Guid> input);
        

        List<OutOccHazardFactorDto> getOccHazardFactors();
        List<OutOccHazardFactorDto> ShowOccHazardFactor(OutOccHazardFactorDto input);
        /// <summary>
        /// 获取防护措施
        /// </summary>
        /// <returns></returns>
        List<HazardFactorsProtective> GetHazardFactorsProtective();

        /// <summary>
        /// 获取危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ShowOccHazardFactorDto> getSimpOccHazardFactors();
    }
}
