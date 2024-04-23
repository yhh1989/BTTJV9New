using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
  public   class SearchInterfaceEmpDto
    {
        /// <summary>
        /// 操作人员编码:
        /// </summary>       
        public virtual string EmployeeNum { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        [StringLength(64)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 对应操作人员ID
        /// </summary>
        public virtual string ObverseEmpId { get; set; }

        /// <summary>
        /// 对应操作人员名称
        /// </summary>
        [StringLength(64)]
        public virtual string ObverseEmpName { get; set; }
    }
}
