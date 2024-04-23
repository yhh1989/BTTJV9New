using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
  public   class ReSultCusOccSumDto
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public virtual Guid? CustomerregId { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(5000)]
        public virtual string Conclusion { get; set; }
      

        /// <summary>
        /// 职业健康处理意见
        /// </summary>
        [StringLength(50)]
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

        /// <summary>
        /// 参考处理意见
        /// </summary>       
        public virtual string Opinions { get; set; }

        /// <summary>
        /// 接害因素名称
        /// </summary>       
        public virtual string RiskName { get; set; }
    }
}
