using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
   public  class OutSeachCardDto : EntityDto<Guid>
    {
    
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
     
        /// <summary>
        /// 卡名称
        /// </summary>
        public virtual string CardTypeName { get; set; }

        /// <summary>
        /// 面值取卡类别面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 开卡时间
        /// </summary>
        public virtual DateTime? CreatTime { get; set; }
        /// <summary>
        /// 开卡人
        /// </summary>
        public virtual string CreateCardUsername { get; set; }

       

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual string SellCardUserName { get; set; }
        /// <summary>
        /// 反款方式1先返款2后返款
        /// </summary>
        public virtual string  PayType { get; set; }

        /// <summary>
        /// 先返款结算金额
        /// </summary>
        public virtual decimal? SellMoney { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual string Available { get; set; }
        /// <summary>
        /// 使用状态0未使用1已使用
        /// </summary>
        public virtual string  HasUse { get; set; }
        

        /// <summary>
        /// 使用者体检号
        /// </summary>
        public virtual string  CustomerBM { get; set; }
        /// <summary>
        /// 使用者姓名
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }
    }
}
