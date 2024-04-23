using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class WXclientCusDto
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
        /// 查询码
        /// </summary>
        [StringLength(32)]
        public virtual string WebQueryCode { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitBM { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

    }
}
