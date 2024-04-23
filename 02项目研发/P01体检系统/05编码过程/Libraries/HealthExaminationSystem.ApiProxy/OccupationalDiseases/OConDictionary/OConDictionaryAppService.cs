using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OConDictionary
{
    public class OConDictionaryAppService : AppServiceApiProxyBase, IOConDictionaryAppService
    {
        public void Delete(OConDictionaryDto input)
        {
            GetResult<OConDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public OConDictionaryDto Edit(OConDictionaryDto input)
        {
            return GetResult<OConDictionaryDto, OConDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<OConDictionaryDto> Get(OConDictionaryDto input)
        {
            return GetResult<OConDictionaryDto, List<OConDictionaryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<OConDictionaryDto> GetAll()
        {
            return GetResult<List<OConDictionaryDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public bool Insert(OConDictionaryDto input)
        {
            return GetResult<OConDictionaryDto, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 插入更新更新类别
        /// </summary>
        /// <param name="input"></param>
        public ZYBTypeDto ZYBTypeInsert(ZYBTypeDto input)
        {
            return GetResult<ZYBTypeDto, ZYBTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="input"></param>
        public void ZYBTypeDelete(ZYBTypeDto input)
        {
            GetResult<ZYBTypeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<ZYBTypeDto> Get(ZYBTypeDto input)
        {
            return GetResult<ZYBTypeDto, List<ZYBTypeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<ZYBTypeDto> ZYBTypeGetAll()
        {
            return GetResult<List<ZYBTypeDto>>( DynamicUriBuilder.GetAppSettingValue());

        }
    }
}
