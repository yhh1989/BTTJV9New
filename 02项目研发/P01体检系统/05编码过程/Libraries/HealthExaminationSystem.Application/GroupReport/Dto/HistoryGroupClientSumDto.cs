using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{ 
   public class HistoryGroupClientSumDto
    {
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>       
        public virtual Guid CustomerRegID { get; set; }
  

        /// <summary>
        /// 总检建议标识
        /// </summary>     
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 单位预约日期
        /// </summary>
        public virtual DateTime? ClientRegDate { get; set; }
        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(1024)]
        public virtual string SummAdvice { get; set; }


        /// <summary>
        /// 单位预约Id
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 单位预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }
        /// <summary>
        /// 建议类别 1健康体检建议2职业健康建议3保健建议4专科建议
        /// </summary>
        public virtual int? SummarizeType { get; set; }

        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(128)]
        public virtual string SummarizeName { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        [StringLength(3072)]
        public virtual string Advice { get; set; }

        /// <summary>
        /// 建议顺序
        /// </summary>
        public virtual int? SummarizeOrderNum { get; set; }
        /// <summary>
        /// 是否隐私
        /// </summary>
        public virtual bool? IsPrivacy { get; set; }
    }
}
