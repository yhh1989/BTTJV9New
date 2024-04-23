#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class BusiCusGRDtocs
    {
        /// <summary>
        /// 订单编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }  

        /// <summary>
        /// 体检号
        /// </summary>     
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 介绍人名字
        /// </summary>    
        public virtual string Introducer { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>    
        public virtual List<string> payment { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string strpayment
        {
            get
            {
                if (payment != null && payment.Count>0)
                {
                    return  string.Join(",", payment);
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal? hasMoney { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public virtual decimal? MayMoney { get; set; }
        
    }
}
