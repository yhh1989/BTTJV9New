using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{
    /// <summary>
    /// 目标疾病
    /// </summary>
    public  class OccConclusionTargetDto
    {
        /// <summary>
        /// 危害因素名称
        /// </summary>
        [StringLength(500)]
        public virtual string RiskNames { get; set; }

        /// <summary>
        /// 体检类型
        /// </summary>
        [StringLength(500)]
        public virtual string ChckType { get; set; }
        /// <summary>
        /// 应检人数
        /// </summary>   
        public virtual int? AllCount { get; set; }
        /// <summary>
        /// 实检人数
        /// </summary>   
        public virtual int? HasCount { get; set; }


        /// <summary>
        /// 检查项目
        /// </summary>
        [StringLength(500)]
        public virtual string Groups { get; set; }

        /// <summary>
        /// 目标疾病
        /// </summary>
        [StringLength(500)]
        public virtual string Target { get; set; }
    }
}
