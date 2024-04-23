using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Dictionary
{
    public class DictionaryAppService : AppServiceApiProxyBase, IDictionaryAppService
    {


        public void DeleteItemDictionary(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<DictionaryItemDictionaryDto> GetById(DictionaryItemDictionaryDto input)
        {
            return GetResult<DictionaryItemDictionaryDto, List<DictionaryItemDictionaryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        public DictionaryItemDictionaryDto GetByDictionaryId(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, DictionaryItemDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void InsertItemDictionary(DictionaryItemDictionaryDto input)
        {
            GetResult<DictionaryItemDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<DictionaryItemInfoDto> QueryInfoDepart(SearchItemInfoDto input)
        {
            return GetResult<SearchItemInfoDto, List<DictionaryItemInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdateItemDictionary(DictionaryItemDictionaryDto input)
        {
            GetResult<DictionaryItemDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
