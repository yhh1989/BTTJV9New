using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{

#if !Proxy
    [AutoMap(typeof(TjlOccCustomerSum))]
#endif
    public class OccCustomerSumDto : EntityDto<Guid>
    {

        /// <summary>
        /// 总检ID
        /// </summary>
        public virtual cusSumRevITemsDto CustomerSummarize { get; set; }


        
        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual OccCusRegDto CustomerRegBM { get; set; }

        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual List<OccCustomerOccDiseasesDto> OccCustomerOccDiseases { get; set; }

        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<OccCustomerContraindicationDto> OccCustomerContraindications { get; set; }

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
        /// 职业健康处理意见
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
