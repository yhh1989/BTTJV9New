using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface
{
  public   class ChargCardDto
    {
        /// <summary>
        ///医院编号
        /// </summary>       
        public virtual string hospitalno { get; set; }

        /// <summary>
        ///会员卡号
        /// </summary>       
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 消费金额，正数为消费，负数为退费
        /// </summary>       
        public virtual decimal Amount { get; set; }

        /// <summary>
        ///档案号
        /// </summary>       
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        ///套餐名称（用于套餐卡控制套餐）
        /// </summary>       
        public virtual string SuitName { get; set; }
        /// <summary>
        ///体检项目数量
        /// </summary>       
        public virtual int CheckItemCount { get; set; }

    }
}
