using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOccTargetDisease))]
#endif
    public class CreateOrUpdateTargetDiseaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 危害因素Id
        /// </summary>
        public virtual Guid? OccHazardFactorsId { get; set; }     
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
        /// 是否启用0启用1停用
        /// </summary>     
        public virtual int IsActive { get; set; }

     
    }
}
