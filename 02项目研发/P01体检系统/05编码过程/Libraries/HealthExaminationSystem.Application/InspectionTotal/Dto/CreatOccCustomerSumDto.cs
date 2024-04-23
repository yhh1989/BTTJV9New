using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{

    /// <summary>
    ///  职业健康总检
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlOccCustomerSum))]
#endif
    public class CreatOccCustomerSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 总检ID
        /// </summary>      
        public virtual Guid? CustomerSummarizeId { get; set; }
        /// <summary>
        /// 体检人预约主键
        /// </summary>      
        public virtual Guid? CustomerRegBMId { get; set; }       

        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(50)]
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 职业健康标准
        /// </summary>
        [StringLength(100)]
        public virtual string Standard { get; set; }

        /// <summary>
        /// 职业健康处理意见
        /// </summary>
        [StringLength(500)]
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

    }
}
