using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto
{
   public class SearchTbmCardTypeDto
    {
        /// <summary>
        /// 编号
        /// </summary>       
        public virtual int CardNum { get; set; }
        /// <summary>
        /// 卡名
        /// </summary>
        public virtual string CardName { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }

        public string ItemSuitName { get; set; }
    }
}
