using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif



namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccTargetDisease))]
#endif
    public class OutTbmOccTargetDiseaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 危害因素Id
        /// </summary>
        public virtual Guid? OccHazardFactorsId { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual ParentTargetDiseaseDto OccHazardFactors { get; set; }
        /// <summary>
        /// 检查类型
        /// </summary>
        [StringLength(50)]
        public virtual string CheckType { get; set; }

        /// <summary>
        /// 人群要求
        /// </summary>
        [StringLength(500)]
        public virtual string Crowd { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }

        /// <summary>
        /// 问诊提示
        /// </summary>
        [StringLength(500)]
        public virtual string InquiryTips { get; set; }

        /// <summary>
        /// 检查周期
        /// </summary>
        [StringLength(500)]
        public virtual string InspectionCycle { get; set; }

        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>     
        public virtual int IsActive { get; set; }

        /// <summary>
        /// 症状
        /// </summary>
        public virtual List<TbmOccTargetDiseaseSymptomsDto> Symptoms { get; set; }

        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual List<TbmOccDiseaseDto> OccDiseases { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string GetOccDiseases
        {
            get
            {
                if (OccDiseases == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = OccDiseases.Select(o => o.Text).ToList();
                    return string.Join(",", GetDisease);
                }
            }
        }
        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<TbmOccTargetDiseaseContraindicationDto> Contraindications { get; set; }

        public virtual string GetContraindications
        {
            get
            {
                if (Contraindications == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = Contraindications.Select(o => o.Text).ToList();
                    string GetDiseases = string.Join(",", GetDisease);
                    return GetDiseases.TrimEnd(',');
                }
               


            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 必选项目
        /// </summary>   
        /// <summary>
        /// 包含的必填项目
        /// </summary>     
        public virtual List<TbmItemGroupDto> MustIemGroups { get; set; }

        /// <summary>
        /// 可选项目
        /// </summary>       
        public virtual List<TbmItemGroupDto> MayIemGroups { get; set; }

        public virtual List<TbmItemGroupDto> MayandMustIemandGroups { get; set; }
        /// <summary>
        /// 项目依据
        /// </summary>
        public virtual List<ItemInfoSimpleDto> ItemInfo { get; set; }

        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 组合Id
        /// </summary>
        public virtual Guid ItemGroupId { get; set; }
    }
}
