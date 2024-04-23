#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    [AutoMapFrom(typeof(TjlCustomerRegItem))]
#endif
    public class OccCustomerItemDto
    {
        public virtual Guid CustomerRegId { get; set; }
       
        /// <summary>
        /// 异常结果
        /// </summary>
        [StringLength(3072)]
        public virtual string Sum { get; set; }
       

        /// <summary>
        /// 项目类型1职业健康 2其他
        /// </summary>
        public virtual int? SumTypeBM { get; set; }
    }
}
