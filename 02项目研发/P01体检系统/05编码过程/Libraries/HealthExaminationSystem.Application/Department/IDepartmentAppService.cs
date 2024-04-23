using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Department
{
    public interface IDepartmentAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取所有科室信息/部门名称
        /// </summary>
        PageResultDto<TbmDepartmentDto> QueryDepartment(PageInputDto<TbmDepartmentDto> input);


        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        TbmDepartmentDto GetById(EntityDto<Guid> input);

        /// <summary>
        /// 删除科室信息
        /// </summary>
        /// <param name="input">主键</param>
        void DeleteDepartment(EntityDto<Guid> input);
        
        /// <summary>
        /// 添加科室信息
        /// </summary>
        /// <param name="input">类</param>
        void InsertDepartmentn(CreateDepartmentDto input);

        /// <summary>
        /// 获取所有科室信息
        /// </summary>
        /// <returns></returns>
#if Application
        Task<List<TbmDepartmentDto>> GetAll();
#elif Proxy
        List<TbmDepartmentDto> GetAll();
#endif
        /// <summary>
        /// 通过名称模糊搜索科室
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<TbmDepartmentDto> GetByName(SearchDepartmentNameDto input);

        void Update(UpdateDepartmentDto input);

        /// <summary>
        /// 获取简单科室信息列表
        /// </summary>
        /// <returns></returns>
        List<DepartmentSimpleDto> QuerySimples();
        /// <summary>
        /// 科室排序
        /// </summary>
        /// <param name="input"></param>
        void UpdateOrder(ChargeBM input);
        int? GetMaxOrderNum();
        
    }
}