
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System.ComponentModel.DataAnnotations.Schema;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummarizeBM))]
#endif
    public class GroupClientSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>       
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual GroupCusReg CustomerReg { get; set; }      

        /// <summary>
        /// 总检建议标识
        /// </summary>     
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual SummarizeAdviceDto SummarizeAdvice { get; set; }

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
