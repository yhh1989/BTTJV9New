using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit
{
    /// <summary>
    /// 套餐接口
    /// </summary>
    public interface IItemSuitAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemSuitDto Add(ItemSuitInput input);
        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(EntityDto<Guid> input);
        /// <summary>
        /// 修改套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemSuitDto Edit(ItemSuitInput input);
        /// <summary>
        /// 获取一个套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullItemSuitDto Get(SearchItemSuitDto input);
       // List<ItemSuitOutput> GetList();

        /// <summary>
        /// 获取简单的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleItemSuitDto> QuerySimples(SearchItemSuitDto input);
        /// <summary>
        /// 查询简单套餐信息缓存
        /// </summary>
#if Application
        Task<List<SimpleItemSuitDto>> QuerySimplesCache();
#elif Proxy
        List<SimpleItemSuitDto> QuerySimplesCache();
#endif
        /// <summary>
        /// 获取简单带子集的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleSubsetItemSuitDto> QuerySimpleSubsets(SearchItemSuitDto input);
        /// <summary>
        /// 获取完整的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ItemSuitDto> QueryNatives(SearchItemSuitDto input);
        /// <summary>
        /// 获取含关联的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<FullItemSuitDto> QueryFulls(SearchItemSuitDto input);

        /// <summary>
        /// 分页获取完整的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<ItemSuitDto> PageNatives(PageInputDto<SearchItemSuitDto> input);

        /// <summary>
        /// 分页获取含关联的套餐列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<FullItemSuitDto> PageFulls(PageInputDto<SearchItemSuitDto> input);
        /// <summary>
        /// 查询关联的套餐详细列表
        /// </summary>
        /// <returns></returns>
        List<ItemSuitItemGroupContrastFullDto> GetAllItemGroup();
        /// <summary>
        /// 套餐关联项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        List<ItemSuitGroupSimpDto> GetAllSuitItemGroup(EntityDto<Guid> input);
        /// <summary>
        /// 复制套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ItemSuitItemGroupContrastFullDto> GetAllSuitItemGroups(EntityDto<Guid> input);
    }
}
