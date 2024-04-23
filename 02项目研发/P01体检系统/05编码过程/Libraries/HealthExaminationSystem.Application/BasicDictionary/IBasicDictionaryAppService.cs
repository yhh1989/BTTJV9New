using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary
{
    public interface IBasicDictionaryAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加基本字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        BasicDictionaryDto Add(CreateBasicDictionaryDto input);

        /// <summary>
        /// 删除基本字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(EntityDto<Guid> input);

        /// <summary>
        /// 修改基本字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        BasicDictionaryDto Edit(UpdateBasicDictionaryDto input);

        /// <summary>
        /// 获取一个基本字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        BasicDictionaryDto Get(EntityDto<Guid> input);
        void InExcel(List<CreateBasicDictionaryDto> inputlist);
        /// <summary>
        /// 获取添加基本字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<BasicDictionaryDto> Query(BasicDictionaryInput input);
        /// <summary>
        /// 查询字典缓存数据
        /// </summary>
        /// <returns></returns>
        

#if Application
        Task<List<BasicDictionaryDto>> QueryCache();
#elif Proxy
         List<BasicDictionaryDto> QueryCache();
#endif
    }
}