using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class WxCusReview
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 疾病名编码
        /// </summary>
        [StringLength(256)]
        public virtual string adviceBM { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(256)]
        public virtual string IllName { get; set; }

        /// <summary>
        /// 复查周期/天
        /// </summary>       
        public virtual DateTime? ReviewDate { get; set; }
    
        /// <summary>
        /// 复查建议
        /// </summary>      
        [StringLength(256)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>       
        public virtual DateTime? LastDate { get; set; }
        /// <summary>
        /// 复查项目
        /// </summary>
        public virtual List<WxReviewItemGroup> ItemGroups { get; set; }
    }
}
