#if Application
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class WXClientTeamInfoDto
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }
        /// <summary>
        /// 限额金额
        /// </summary>
        public virtual decimal? QuotaMoney { get; set; }

        /// <summary>
        /// 支付方式 单位支付，个人支付，单位限额
        /// </summary>
        public virtual int? CostType { get; set; }

        /// <summary>
        /// 加项折扣
        /// </summary>
        public virtual decimal? Jxzk { get; set; }
        /// <summary>
        /// 分组价格
        /// </summary>
        public virtual decimal? TeamMoney { get; set; }


        /// <summary>
        /// 是否限额 1是0否      
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual int? IsQuota
        {
            get
            {               
                if (CostType == 11)
                {
                    return 1;
                }
                else  
                {
                    return 0;
                }
             
            }

        }

    }
}
