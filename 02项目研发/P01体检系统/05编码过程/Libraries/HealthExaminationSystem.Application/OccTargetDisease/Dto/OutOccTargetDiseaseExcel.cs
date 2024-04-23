using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
   public class OutOccTargetDiseaseExcel: EntityDto<Guid>
    {
        /// <summary>
        /// 危害因素
        /// </summary>
        public string OccHazardFactorsName { get; set; }
        /// <summary>
        /// 检查类型
        /// </summary>
        public string CheckType { get; set; }
        /// <summary>
        /// 职业健康
        /// </summary>
        public string OccDiseases { get; set; }
        /// <summary>
        /// 症状
        /// </summary>
        public string Symptoms { get; set; }
        /// <summary>
        /// 必选项目
        /// </summary>   
        /// <summary>
        /// 包含的必填项目
        /// </summary>     
        public string MustIemGroups { get; set; }
        /// <summary>
        /// 可选项目
        /// </summary>       
        public string MayIemGroups { get; set; }
        /// <summary>
        /// 问询提示
        /// </summary>
        public string InquiryTips { get; set; }
        /// <summary>
        /// 职业禁忌
        /// </summary>
        public string Contraindications { get; set; }
        /// <summary>
        /// 检查对象
        /// </summary>
        public string Crowd { get; set; }

    }
}
