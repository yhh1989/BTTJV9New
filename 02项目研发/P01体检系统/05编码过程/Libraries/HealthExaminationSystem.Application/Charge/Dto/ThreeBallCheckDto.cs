using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;


namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class ThreeBallCheckDto
    {

        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 团体应收金额
        /// </summary>
        public virtual decimal? ClientMoney { get; set; }
        
        /// <summary>
        /// 申请单号
        /// </summary>
        public virtual string SQDH { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public virtual int? SQSTATUS { get; set; }

        /// <summary>
        /// 体检人数
        /// </summary>
#if !Proxy
#endif
        [JsonIgnore]
        public virtual string fSQSTATUS
        {
            get
            {
                if (SQSTATUS == 1)
                    return "未收费";
                else if (SQSTATUS == 2)
                    return "已收费";
                else if (SQSTATUS == 3)
                    return "已作废";
                else if (SQSTATUS == 4)
                    return "部分收费";
                else
                    return "无";
            }
        }
        /// <summary>
        /// 收费申请日期
        /// </summary>
        public string SQDHData { get; set; }               
        /// <summary>
        /// 发票号
        /// </summary>
        public virtual string FPH { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal? nvoiceMoney { get; set; }
        /// <summary>
        /// 发票总金额
        /// </summary>
        public virtual decimal? AllnvoiceMoney { get; set; }
        /// <summary>
        /// 开票日期
        /// </summary>
        public virtual string InvoiceDate { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Discount { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 开票名称
        /// </summary>
        public virtual string FPName { get; set; }



        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int? FZState { get; set; }
        /// <summary>
        /// 封账时间
        /// </summary>        
        public virtual DateTime? FZTime { get; set; }

        /// <summary>
        /// 结算状态0未结算，1已结算
        /// </summary>
        public virtual int? JSState { get; set; }
        /// <summary>
        /// 自费金额
        /// </summary>
        public virtual decimal? GRPayMoney { get; set; }

        /// <summary>
        /// 自费金额
        /// </summary>
        public virtual decimal? REZKJE { get; set; }
    }
}
