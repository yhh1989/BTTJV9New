using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    /// 职业健康总检
    /// </summary>
    public class TjlOccCustomerHazardSum : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ForeignKey("CustomerSummarize")]
        public virtual Guid? CustomerSummarizeId { get; set; }
        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual TjlCustomerSummarize CustomerSummarize { get; set; }
        /// <summary>
        /// 主键
        /// </summary>     
        [NotMapped]
        [Obsolete("停止使用！", true)]
        public virtual Guid? OccCustomerSummarizeId { get; set; }
        /// <summary>
        /// 职业总检
        /// </summary>
        [NotMapped]
        [Obsolete("停止使用！", true)]
        public virtual TjlOccCustomerSum OccCustomerSummarize { get; set; }

        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }


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
        /// 职业病 
        /// </summary>
        public virtual List<TbmOccDisease> OccCustomerOccDiseases { get; set; }

        /// <summary>
        /// 职业禁忌证
        /// </summary>
        public virtual List<TbmOccDictionary> OccDictionarys { get; set; }
        /// <summary>
        /// 是否主要 1 为主要
        /// </summary>
        public virtual int IsMain { get; set; }
        /// <summary>
        /// 职业病
        /// </summary>
        public virtual string OccCustomerOccDiseasesIds { get; set; }

        /// <summary>
        /// 职业禁忌证
        /// </summary>
        public virtual string OccDictionarysIDs { get; set; }

        /// <summary>
        /// 参考处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }


        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(5000)]
        public virtual string Conclusion { get; set; }
        

        /// <summary>
        /// 职业健康处理意见
        /// </summary>
        [StringLength(1000)]
        public virtual string Advise { get; set; }


        /// <summary>
        /// 职业健康结论描述
        /// </summary>
        [StringLength(1000)]
        public virtual string Description { get; set; }



        /// <summary>
        /// 医学建议
        /// </summary>
        [StringLength(1000)]
        public virtual string MedicalAdvice { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
