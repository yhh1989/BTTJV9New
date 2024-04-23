using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Dictionary
{

    public interface IDictionaryAppService
#if Application
        : IApplicationService
#endif
    {
        
        List<DictionaryItemInfoDto> QueryInfoDepart(SearchItemInfoDto input);

        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        List<DictionaryItemDictionaryDto> GetById(DictionaryItemDictionaryDto input);


        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        DictionaryItemDictionaryDto GetByDictionaryId(EntityDto<Guid> input);


        /// <summary>
        /// 删除项目字典
        /// </summary>
        /// <param name="input">主键</param>
        void DeleteItemDictionary(EntityDto<Guid> input);

        /// <summary>
        /// 添加项目字典
        /// </summary>
        /// <param name="input">类</param>
        void InsertItemDictionary(DictionaryItemDictionaryDto input);

        /// <summary>
        /// 修改项目字典
        /// </summary>
        /// <param name="input">类</param>
        void UpdateItemDictionary(DictionaryItemDictionaryDto input);
    }

}