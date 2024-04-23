using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface
{
   public class getCardInfoDto
    {
        /// <summary>
        ///会员卡编号
        /// </summary>       
        public virtual string CardNo { get; set; }

        /// <summary>
        ///剩余金额
        /// </summary>       
        public virtual decimal Amount { get; set; }


        /// <summary>
        ///类别编号
        /// </summary>       
        public virtual string CategoryNo { get; set; }

        /// <summary>
        ///类别名称
        /// </summary>       
        public virtual string CategoryName { get; set; }

        /// <summary>
        ///绑定档案号集合（可以为空）
        /// </summary>       
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 返回
        /// </summary>
        public virtual int code { get; set; }
        /// <summary>
        /// 返回
        /// </summary>
        public virtual string err { get; set; }
    }
}
