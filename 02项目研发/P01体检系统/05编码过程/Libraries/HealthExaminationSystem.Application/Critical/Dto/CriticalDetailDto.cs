#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmCriticalDetail))]
#endif
    public class CriticalDetailDto
    {
        public virtual Guid CriticalSetId { get; set; }
        /// <summary>
        /// 并且/或者
        /// </summary>            
        public virtual string relations { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>   
        public virtual Guid ItemInfoId { get; set; }
     
        /// <summary>
        /// 运算符
        /// </summary>            
        public virtual int Operator { get; set; }

        /// <summary>
        /// 数值结果
        /// </summary>            
        public virtual decimal? ValueNum { get; set; }


        /// <summary>
        /// 包含文字
        /// </summary> 
        public virtual string ValueChar { get; set; }

    }
}
