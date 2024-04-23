using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
    public  class ReSultCusDepartDto
    {
        /// <summary>
        /// 科室ID
        /// </summary>       
        public virtual Guid DepartId { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>       
        public virtual int? DepartOrder { get; set; }
   


        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartName { get; set; }

        /// <summary>
        /// 组合小结
        /// </summary>
        [StringLength(3072)]
        public virtual string DepartSum { get; set; }
    }
}
