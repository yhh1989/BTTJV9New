#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmReSultCusGroup))]
#endif
    public class ReSultCusGroupDto
    {
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>    
        public virtual Guid? CustomerItemGroupBMid { get; set; }
        public virtual Guid DepartmentId { get; set; }

        public virtual Guid ItemGroupId { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>       
        public virtual int? DepartOrder { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>       
        public virtual int? GroupOrder { get; set; }        


        /// <summary>
        /// 组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 组合小结
        /// </summary>
        [StringLength(3072)]
        public virtual string GroupSum { get; set; }
        
    }
}
