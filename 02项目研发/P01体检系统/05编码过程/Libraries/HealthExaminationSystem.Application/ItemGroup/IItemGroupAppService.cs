using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup
{
    /// <summary>
    /// 项目组合接口
    /// </summary>
    public interface IItemGroupAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加项目组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemGroupDto Add(ItemGroupInput input);
        /// <summary>
        /// 删除项目组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(EntityDto<Guid> input);
        /// <summary>
        /// 修改项目组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemGroupDto Edit(ItemGroupInput input);
        /// <summary>
        /// 获取一个项目组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemGroupDto Get(SearchItemGroupDto input);

        /// <summary>
        /// 获取简单的项目组合列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleItemGroupDto> QuerySimples(SearchItemGroupDto input);
        /// <summary>
        /// 获取完整的项目组合列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ItemGroupDto> QueryNatives(SearchItemGroupDto input);
        /// <summary>
        /// 获取含关联的项目组合列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<FullItemGroupDto> QueryFulls(SearchItemGroupDto input);

        /// <summary>
        /// 分页获取完整的项目组合列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<ItemGroupDto> PageNatives(PageInputDto<SearchItemGroupDto> input);

        /// <summary>
        /// 分页获取含关联的项目组合列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<FullItemGroupDto> PageFulls(PageInputDto<SearchItemGroupDto> input);

        /// <summary>
        /// 获取所有项目组合
        /// </summary>
        /// <returns></returns>
        List<FullItemGroupDto> GetAll();
        void UpdateOrder(ChargeBM input);
        void adMLXM(EntityDto<Guid> deptID);
        /// <summary>
        /// 获取缓存组合
        /// </summary>
        /// <returns></returns>
        int? GetMaxOrderNum();

        List<priceSynDto> GetPriceSyns();
      List<ChargeBM> getItemNames(SearIdsDto input);
        ConfiStrDto getItemConf(ConfiITemDto input);


#if Application
        Task<List<SimpleItemGroupDto>> SimpleGroup();
#elif Proxy
        List<SimpleItemGroupDto> SimpleGroup();
#endif
    }
}