using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class OutOccCustomerSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary> 
        public virtual Guid? CustomerRegBMId { get; set; } 

        /// <summary>
        /// 职业健康
        /// </summary>
        public virtual List<CraetOccCustomerOccDiseasesDto> OccCustomerOccDiseases { get; set; }

        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<CreatOccCustomerContraindicationDto> OccCustomerContraindications { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
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
        [StringLength(50)]
        public virtual string Advise { get; set; }


        /// <summary>
        /// 职业健康处理意见
        /// </summary>
        [StringLength(1000)]
        public virtual string Description { get; set; }
    }
}
