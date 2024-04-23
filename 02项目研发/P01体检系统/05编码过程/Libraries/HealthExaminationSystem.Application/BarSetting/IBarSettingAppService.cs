using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting
{
    /// <summary>
    /// 条码设置接口
    /// </summary>
    public interface IBarSettingAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加条码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullBarSettingDto Add(BarSettingInput input);

        /// <summary>
        /// 删除条码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(EntityDto<Guid> input);

        /// <summary>
        /// 修改条码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullBarSettingDto Edit(BarSettingInput input);

        /// <summary>
        /// 获取一个条码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullBarSettingDto Get(SearchBarSettingDto input);

        /// <summary>
        /// 获取简单的条码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleBarSettingDto> QuerySimples(SearchBarSettingDto input);

        /// <summary>
        /// 获取完整的条码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<BarSettingDto> QueryNatives(SearchBarSettingDto input);

        /// <summary>
        /// 获取含关联的条码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<FullBarSettingDto> QueryFulls(SearchBarSettingDto input);

        /// <summary>
        /// 分页获取完整的条码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<BarSettingDto> PageNatives(PageInputDto<SearchBarSettingDto> input);

        /// <summary>
        /// 分页获取含关联的条码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<FullBarSettingDto> PageFulls(PageInputDto<SearchBarSettingDto> input);

        PageResultDto<BarCodeViewDto> GetAll(PageInputDto input);

        /// <summary>
        /// 获取未打印的项目组合
        /// </summary>
        /// <param name="searchBarItemDto">所有组合id 已打印组合id</param>
        /// <returns></returns>
        List<BarItembViewDto> GetBarItemGroupFulls(SearchBarItemDto searchBarItemDto);
        /// <summary>
        /// 获取所有条码组合明细表
        /// </summary>
        /// <returns></returns>
        List<BarItemDto> GetBarItems();
    }
}