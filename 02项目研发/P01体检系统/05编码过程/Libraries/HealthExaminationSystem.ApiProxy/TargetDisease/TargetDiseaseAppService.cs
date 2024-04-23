using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease
{
    /// <summary>
    /// 目标疾病
    /// </summary>
    public class TargetDiseaseAppService :  AppServiceApiProxyBase, ITargetDiseaseAppService
    {
        /// <summary>
        /// 根据危害因素和岗位类别查询
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public SearchOccupationalDiseaseIncludeItemGroupDto GetOccupationalDiseaseIncludeItemGroup(QueryTargetDisease TargetDisease)
        {
            return GetResult<QueryTargetDisease, SearchOccupationalDiseaseIncludeItemGroupDto>(TargetDisease, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询症状
        /// </summary>
        /// <returns></returns>
        public List<UpdateSymptomDto> GetSymptom(UpdateSymptomDto dto)
        {
            return GetResult<UpdateSymptomDto, List<UpdateSymptomDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public List<UpdateSymptomDto> UpdateSymptom(UpdateSymptomDto UpdateSymptomDto)
        {
            return GetResult<UpdateSymptomDto, List<UpdateSymptomDto>>(UpdateSymptomDto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改必选项目和可选项目
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        public SearchOccupationalDiseaseIncludeItemGroupDto UpdateItemGroup(SearchOccupationalDiseaseIncludeItemGroupDto Dto)
        {
            return GetResult<SearchOccupationalDiseaseIncludeItemGroupDto, SearchOccupationalDiseaseIncludeItemGroupDto>(Dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// <summary>
        /// 根据危害因素和岗位类别查询符合条件的数据集合
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public List<SearchOccupationalDiseaseIncludeItemGroupDto> GetOccupationalDiseaseIncludeItemGroupDto(QueryTargetDiseaseDto TargetDisease)
        {
            return GetResult<QueryTargetDiseaseDto, List<SearchOccupationalDiseaseIncludeItemGroupDto>>(TargetDisease, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询所有体格检查信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<UpdateHumanBodySystemDto> SelectHumanBodySystemAll(UpdateHumanBodySystemDto dto)
        {
            return GetResult<UpdateHumanBodySystemDto, List<UpdateHumanBodySystemDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 体格检查
        /// </summary>
        /// <returns></returns>
        public List<UpdateHumanBodySystemDto> UpdateHumanBodySystem(UpdateHumanBodySystemDto dto)
        {
            return GetResult<UpdateHumanBodySystemDto, List<UpdateHumanBodySystemDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 修改和增加职业健康或禁忌证
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<SearchOccupationalDiseaseIncludeItemGroupDto> UpdateUpdateDiseaseOrContraindication(UpdateDiseaseContraindicationExplainDto dto)
        {
            return GetResult<UpdateDiseaseContraindicationExplainDto, List<SearchOccupationalDiseaseIncludeItemGroupDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
        }



        /// <summary>
        /// 根据id查询疾病禁忌证及解释
        /// </summary>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public DiseaseContraindicationExplainDto GetDiseaseContraindicationExplainDtoForId(EntityDto<Guid> Id)
        {
            return GetResult<EntityDto<Guid>, DiseaseContraindicationExplainDto>(Id, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 查询必选项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SimpleItemGroupDto> GetHaveItemGroup(QueryTargetDisease query)
        {
            return GetResult<QueryTargetDisease, List<SimpleItemGroupDto>>(query, DynamicUriBuilder.GetAppSettingValue());
        }
    }

}