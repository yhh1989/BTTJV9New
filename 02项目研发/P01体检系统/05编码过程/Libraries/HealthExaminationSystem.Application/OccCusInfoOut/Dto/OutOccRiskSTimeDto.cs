using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
    /// <summary>
    /// 4.职业健康档案-体检危害因素
    /// </summary>
    public class OutOccRiskSTimeDto
    {

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string 体检编号
        { get; set; }

        /// <summary>
        /// 危害因素编码
        /// </summary>       
        public virtual string 危害因素编码
        { get; set; }

        /// <summary>
        /// 其他危害因素
        /// </summary>       
        public virtual string 其他危害因素名称
        { get; set; }

        /// <summary>
        /// 接害日期
        /// </summary>       
        public virtual DateTime? 开始接害日期
        { get; set; }

        /// <summary>
        /// 接害工龄年
        /// </summary>
        [StringLength(16)]
        public virtual string 接触所监测危害因素工龄年
        { get; set; }
        /// <summary>
        /// 接害工龄月
        /// </summary>
        [StringLength(16)]
        public virtual string 接触所监测危害因素工龄月
        { get; set; }

    }
}
