using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.Application.Services;
using Abp.Application.Services.Dto;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease
{
    /// <summary>
    /// 目标疾病
    /// </summary>
    public interface ITargetDiseaseAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 根据危害因素和岗位类别查询
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        SearchOccupationalDiseaseIncludeItemGroupDto GetOccupationalDiseaseIncludeItemGroup(QueryTargetDisease TargetDisease);
        /// <summary>
        /// 查询症状
        /// </summary>
        /// <returns></returns>
        List<UpdateSymptomDto> GetSymptom(UpdateSymptomDto dto);
        /// <summary>
        /// 修改症状
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        List<UpdateSymptomDto> UpdateSymptom(UpdateSymptomDto UpdateSymptomDto);
        /// <summary>
        /// 修改必选项目和可选项目
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        SearchOccupationalDiseaseIncludeItemGroupDto UpdateItemGroup(SearchOccupationalDiseaseIncludeItemGroupDto Dto);
        /// <summary>
        /// 根据危害因素和岗位类别查询符合条件的数据集合
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        List<SearchOccupationalDiseaseIncludeItemGroupDto> GetOccupationalDiseaseIncludeItemGroupDto(QueryTargetDiseaseDto TargetDisease);
        /// <summary>
        /// 查询所有体格检查信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<UpdateHumanBodySystemDto> SelectHumanBodySystemAll(UpdateHumanBodySystemDto dto);
        /// <summary>
        /// 体格检查
        /// </summary>
        /// <returns></returns>
        List<UpdateHumanBodySystemDto> UpdateHumanBodySystem(UpdateHumanBodySystemDto dto);
        /// <summary>
        /// 修改和增加职业健康或禁忌证
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<SearchOccupationalDiseaseIncludeItemGroupDto> UpdateUpdateDiseaseOrContraindication(UpdateDiseaseContraindicationExplainDto dto);

        /// <summary>
        /// 根据id查询疾病禁忌证及解释
        /// </summary>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        DiseaseContraindicationExplainDto GetDiseaseContraindicationExplainDtoForId(EntityDto<Guid> Id);
        /// <summary>
        /// 查询必选项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<SimpleItemGroupDto> GetHaveItemGroup(QueryTargetDisease query);
    }
}
