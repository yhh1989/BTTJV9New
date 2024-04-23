using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys
{
    public interface IPersonnelCategoryAppService
#if Application
        : IApplicationService
#endif
    {
        /// <summary>
        /// 查询人员类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<PersonnelCategoryViewDto> QueryCategoryList(PersonnelCategoryViewDto dto);

        /// <summary> 
        /// </summary>
        /// <param name="dto"></param>
        PersonnelCategoryViewDto SaveCategory(PersonnelCategoryViewDto dto);

        /// <summary>
        /// 编辑人员类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool EditeCategory(PersonnelCategoryViewDto dto);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool DeleteCategory(PersonnelCategoryViewDto dto);
    }
}