using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Occupational
{
    /// <summary>
    /// 目标疾病 
    /// </summary>
  public   class TbmOccTargetDisease : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 危害因素Id
        /// </summary>
        [ForeignKey(nameof(OccHazardFactors))]
        public virtual Guid? OccHazardFactorsId { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual TbmOccHazardFactor OccHazardFactors { get; set; }
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
        /// 是否启用1启用0停用
        /// </summary>     
        public virtual int IsActive { get; set; }

        /// <summary>
        /// 症状
        /// </summary>
        public virtual ICollection<TbmOccTargetDiseaseSymptoms> Symptoms { get; set; }
        /// <summary>
        /// 职业病
        /// </summary>
        public virtual ICollection<TbmOccDisease> OccDiseases { get; set; }

        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual ICollection<TbmOccTargetDiseaseContraindication> Contraindications { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>      
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmItemGroup> temGroups { get; set; }
        /// <summary>
        /// 必选项目
        /// </summary>   
        /// <summary>
        /// 包含的必填项目
        /// </summary>     
        [InverseProperty(nameof(TbmItemGroup.OccTargetDiseaseMustIemGroups))]
        public virtual ICollection<TbmItemGroup> MustIemGroups { get; set; }

        /// <summary>
        /// 可选项目
        /// </summary>       
        [InverseProperty(nameof(TbmItemGroup.OccTargetDiseaseMayIemGroups))]
        public virtual ICollection<TbmItemGroup> MayIemGroups { get; set; }

        /// <summary>
        /// 项目依据
        /// </summary>
        public virtual ICollection<TbmItemInfo> ItemInfo { get; set; }
        /// <summary>
        /// 检查周期
        /// </summary>
        [StringLength(500)]
        public virtual string InspectionCycle { get; set; }       

        

        /// <inheritdoc />
        public virtual int TenantId { get; set; }


    }
}
