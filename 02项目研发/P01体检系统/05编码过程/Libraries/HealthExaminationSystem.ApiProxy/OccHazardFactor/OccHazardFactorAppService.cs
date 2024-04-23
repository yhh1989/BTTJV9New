using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor
{
 public    class OccHazardFactorAppService : AppServiceApiProxyBase, IOccHazardFactorAppService
    {
        /// <summary>
        /// 添加危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto Add(FullHarardFactor input)
        {
            return GetResult<FullHarardFactor, OutOccHazardFactorDto>(input,DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 添加照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public RadiationDto AddRadiation(RadiationDto input)
        {
            return GetResult<RadiationDto, RadiationDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 显示照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<RadiationDto> ShowRadiation(RadiationDto input)
        {
            return GetResult<RadiationDto, List<RadiationDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 删除照射种类
        /// </summary>
        /// <param name="input"></param>
        public void DelRadiation(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public RadiationDto EditRadiation(RadiationDto input)
        {
            return GetResult<RadiationDto, RadiationDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public RadiationDto GetRadiationDto(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, RadiationDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 删除危害因素
        /// </summary>
        /// <param name="input"></param>
        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto Edit(FullHarardFactor input)
        {
            return GetResult<FullHarardFactor, OutOccHazardFactorDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto GetOccHazardFactor(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, OutOccHazardFactorDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutOccHazardFactorDto> getOccHazardFactors()
        {
            return GetResult<List<OutOccHazardFactorDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutOccHazardFactorDto> ShowOccHazardFactor(OutOccHazardFactorDto input)
        {
            return GetResult<OutOccHazardFactorDto, List<OutOccHazardFactorDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取防护措施
        /// </summary>
        /// <returns></returns>
        public List<HazardFactorsProtective> GetHazardFactorsProtective()
        {
            return GetResult<List<HazardFactorsProtective>>(DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取简单危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ShowOccHazardFactorDto> getSimpOccHazardFactors()
        {
            return GetResult<List<ShowOccHazardFactorDto>>(DynamicUriBuilder.GetAppSettingValue());
        }



    }

}
