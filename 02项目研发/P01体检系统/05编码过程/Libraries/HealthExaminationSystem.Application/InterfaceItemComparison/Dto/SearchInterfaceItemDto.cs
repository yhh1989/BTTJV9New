using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
   public  class SearchInterfaceItemDto
    {
        /// <summary>
        /// 组合名称?
        /// </summary>
        [StringLength(64)]
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 组合名称?
        /// </summary>
        [StringLength(64)]
        public virtual string GroupName { get; set; }
        /// <summary>
        /// 项目名称?
        /// </summary>
        [StringLength(64)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 对应项目ID
        /// </summary>
        public virtual string ObverseItemId { get; set; }

        /// <summary>
        /// 对应项目名称?
        /// </summary>
        [StringLength(64)]
        public virtual string ObverseItemName { get; set; }

        /// <summary>
        /// 仪器型号
        /// </summary>
        [StringLength(64)]
        public virtual string InstrumentModelNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(64)]
        public virtual string Remarks { get; set; }
        
    }
}
