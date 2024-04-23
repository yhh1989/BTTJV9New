using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
   public  class SearchInterIFaceItemComparisonDto
    {
        /// <summary>
      /// 科室列表
      /// </summary>           
        public virtual Guid departmentId { get; set; }

        /// <summary>
        /// 组合列表
        /// </summary>   

        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 科室类别列表
        /// </summary>   

        public virtual List<string> DeptTypes { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [StringLength(64)]
        public virtual string CheckType { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>      
        public virtual Guid ItemID { get; set; }

        /// <summary>
        /// 医生ID
        /// </summary>       
        public virtual long EmpID { get; set; }

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
    }
}
