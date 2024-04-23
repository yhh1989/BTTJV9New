using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo
{
    public interface IItemInfoAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ItemInfoViewDto> QueryItemInfo(SearchItemInfoDto input);

        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ItemInfoDto EditItemInfo(ItemInfoDto input);

        /// <summary>
        /// 删除项目信息
        /// </summary>
        /// <param name="input"></param>
        void DeleteItemInfo(EntityDto<Guid> input);

        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ItemInfoDto AddItemInfo(ItemInfoDto input);

        /// <summary>
        /// 查询科室
        /// </summary>
        /// <returns></returns>
        List<DepartmentIdNameDto> DepartmentGetAll();

        /// <summary>
        /// 查询项目标准
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ItemStandardDto> QueryItemStandardByItemId(EntityDto<Guid> input);
        /// <summary>
        /// 获取所有正常项目标准
        /// </summary>
        /// <returns></returns>
        List<ItemStandardDto> QueryItemStandardBySum();
        /// <summary>
        /// 获取所有项目标准
        /// </summary>
        /// <returns></returns>

#if Application
        Task <List<ItemStandardDto>> GetAllItemStandard();
#elif Proxy
         List<ItemStandardDto> GetAllItemStandard();
#endif
        /// <summary>
        /// 删除项目标准
        /// </summary>
        /// <param name="input"></param>
        void DeleteItemStandard(ItemStandardDto input);

        /// <summary>
        /// 编辑项目标准
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ItemStandardDto EditItemStandard(ItemStandardDto input);

        /// <summary>
        /// 添加项目标准
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ItemStandardDto AddItemStandard(ItemStandardDto input);

        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns></returns>
        List<ItemInfoViewDto> GetAll();

        ItemInfoDto GetById(EntityDto<Guid> input);

        /// <summary>
        /// 获取简单项目信息列表
        /// </summary>
        /// <returns></returns>
        
#if Application
        Task<List<ItemInfoSimpleDto>> QuerySimples();
#elif Proxy
       List<ItemInfoSimpleDto> QuerySimples();
#endif
        /// <summary>
        /// 更新计算型项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ItemProcExpressDto SaveItemExpress(ItemProcExpressDto input);
        void DelteItemExpress(EntityDto<Guid> input);
        void UpdateOrder(ChargeBM input);
        int? GetMaxOrderNum();
        void InputUnit(UpItemUnit input);


    }
}