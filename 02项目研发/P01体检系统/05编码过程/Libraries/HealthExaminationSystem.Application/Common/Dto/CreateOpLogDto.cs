#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common.Dto
{
    /// <summary>
    /// 行政区划
    /// </summary>
#if Application
    [AutoMap(typeof(TjlOperationLog))]
#endif
    public class CreateOpLogDto
    {
        /// <summary>
        ///体检号/单位编码
        /// </summary>
        [StringLength(24)]
        public virtual string LogBM { get; set; }
        /// <summary>
        ///姓名/单位名称
        /// </summary>
        [StringLength(100)]
        public virtual string LogName { get; set; }

        /// <summary>
        ///日志类型
        /// </summary>    
        public virtual int? LogType { get; set; }
        /// <summary>
        ///详情
        /// </summary>
        [StringLength(500)]
        public virtual string LogText { get; set; }

        /// <summary>
        ///详情
        /// </summary>
        [StringLength(500)]
        public virtual string LogDetail { get; set; }

    }
}
