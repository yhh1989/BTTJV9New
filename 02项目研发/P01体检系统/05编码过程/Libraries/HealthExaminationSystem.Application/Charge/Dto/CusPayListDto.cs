#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class CusPayListDto
    {
        /// <summary>
        /// 预约ID
        /// </summary>   
        public virtual Guid? RegID { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
       
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(16)]
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        [StringLength(16)]
        public virtual decimal? allMoney { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        [StringLength(16)]
        public virtual decimal? PayMoney { get; set; }

        /// <summary>
        /// 团付金额
        /// </summary>
        [StringLength(16)]
        public virtual decimal? TTMoney { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [StringLength(16)]
        public virtual int? cusCount { get; set; }

        /// <summary>
        /// 收费人
        /// </summary>
        [StringLength(16)]
        public virtual string ChargeUser { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        [StringLength(16)]
        public virtual string Introducer { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(16)]
        public virtual string SuitName { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(16)]
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 支付方式金额
        /// </summary>

        public virtual ICollection<CusChageListDto> cusPayments { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string SFRemarks
        {
            get
            {
                if (SFRemarklist != null && SFRemarklist.Count > 0)
                {
                    return string.Join(";", SFRemarklist.Where(p => p.Remark != "").Select(p => p.Remark).Distinct().ToList());
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// 收费备注
        /// </summary>

        public virtual ICollection<SFDto> SFRemarklist { get; set; }



    }
}
