using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class CusSumBMLisDto
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Uid { get; set; }

        // <summary>
        /// 建议名称
        /// </summary>
        [StringLength(128)]
        public virtual string SummarizeName { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 总检审核日期
        /// </summary>
        public virtual DateTime? ExamineDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastDate { get; set; }

        /// <summary>
        /// 是否全部1是0否
        /// </summary> 
        public virtual int? State { get; set; }
    }
}
