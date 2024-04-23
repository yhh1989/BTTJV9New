using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary
{
    public class BasicDictionaryAppService : AppServiceApiProxyBase, IBasicDictionaryAppService
    {
        public BasicDictionaryDto Add(CreateBasicDictionaryDto input)
        {
            return GetResult<CreateBasicDictionaryDto, BasicDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public BasicDictionaryDto Edit(UpdateBasicDictionaryDto input)
        {
            return GetResult<UpdateBasicDictionaryDto, BasicDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public BasicDictionaryDto Get(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, BasicDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void InExcel(List<CreateBasicDictionaryDto> inputlist)
        {
             GetResult<List<CreateBasicDictionaryDto>>(inputlist, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<BasicDictionaryDto> Query(BasicDictionaryInput input)
        {
            return GetResult<BasicDictionaryInput, List<BasicDictionaryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询字典缓存数据
        /// </summary>
        /// <returns></returns>
        public List<BasicDictionaryDto> QueryCache()
        {
            return GetResult<List<BasicDictionaryDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
