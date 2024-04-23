using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
  public   class InCardSearchDto
    {
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 卡类别标识
        /// </summary>       
        public virtual Guid CardTypeId { get; set; }       

       
        /// <summary>
        /// 开卡开始时间
        /// </summary>
        public virtual DateTime? CstarTime { get; set; }
        /// <summary>
        /// 开卡结束时间
        /// </summary>
        public virtual DateTime? CendTime { get; set; }

        /// <summary>
        /// 开卡人标识
        /// </summary>      
        public virtual long? CreateCardUserId { get; set; }

       

        /// <summary>
        /// 销售员标识
        /// </summary>     
        public virtual long? SellCardUserId { get; set; }

       
        /// <summary>
        /// 反款方式1先返款2后返款
        /// </summary>
        public virtual int PayType { get; set; }

        
        
        /// <summary>
        /// 使用状态0未使用1已使用
        /// </summary>
        public virtual int? HasUse { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }


        /// <summary>
        /// 使用结束时间
        /// </summary>
        public virtual DateTime? UsesendTime { get; set; }
        /// <summary>
        /// 使用开始时间
        /// </summary>
        public virtual DateTime? UsestarTime { get; set; }

    }
}
