using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
    /// <summary>
    /// 3.职业健康档案-接触危害因素
    /// </summary>
    public class OutOccRiskSDto
    {

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string 体检编号
        { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(32)]
        public virtual string 危害因素编码 { get; set; }

        /// <summary>
        /// 其他危害因素
        /// </summary>
        [StringLength(32)]
        public virtual string 其他危害因素名称
 { get; set; }


    }
}
