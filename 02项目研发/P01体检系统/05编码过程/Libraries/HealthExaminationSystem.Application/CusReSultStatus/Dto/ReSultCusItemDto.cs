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
    [AutoMap(typeof(TbmReSultCusItem))]
#endif
        public class ReSultCusItemDto
    {
        ///// <summary>
        ///// 体检人检查项目组合id
        ///// </summary>    
        //public virtual Guid? CustomerItemGroupBMid { get; set; }
        //public virtual Guid DepartmentId { get; set; }
        //public virtual Guid? ItemGroupBMId { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>       
        public virtual int? DepartOrder { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>       
        public virtual int? GroupOrder { get; set; }
        /// <summary>
        /// 项目
        /// </summary>       
        public virtual int? ItemOrder { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemCodeBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>
        public virtual string xmbs { get; set; }

        public virtual Guid ItemInfoId { get; set; }
    }
}
