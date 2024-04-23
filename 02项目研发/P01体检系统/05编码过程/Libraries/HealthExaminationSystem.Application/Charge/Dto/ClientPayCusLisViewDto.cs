using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
#if Application
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#elif Proxy
using System.Linq;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class ClientPayCusLisViewDto : EntityDto<Guid>
    {
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string RegisterStates
        {
            get
            {
                if (RegisterState.HasValue)
                {
                    if (RegisterState.Value == 1)
                    {
                        return "未登记";
                    }

                    return "已登记";
                }

                return "未知";
            }
        }

        /// <summary>
        /// 关联组合
        /// </summary>
        public virtual ICollection<CusGroupTTDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 人员信息
        /// </summary>
        public virtual CusTTDto Customer { get; set; }

        /// <summary>
        /// 团体应收已收
        /// </summary>
        public virtual CusPayTTDto McusPayMoney { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual ChargeTeamNameDto ClientTeamInfo { get; set; }


        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
       #if Application
        [IgnoreMap]
      #endif
        [JsonIgnore]
        [Obsolete("暂停使用", true)]
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual GroupClientNameDto ClientInfo { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        [Obsolete("暂停使用", true)]
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string CheckSates
        {
            get
            {
                if (CheckSate.HasValue)
                {
                    if (CheckSate.Value == 1 || CheckSate.Value == 2 || CheckSate.Value == 3)
                    {
                        if (CheckSate.Value == 1)
                        {
                            return "未体检";
                        }

                        if (CheckSate.Value == 2)
                        {
                            return "体检中";
                        }

                        if (CheckSate.Value == 3)
                        {
                            return "体检完成";
                        }

                        return "未知";
                    }

                    return "未知";
                }

                return "未知";
            }
        }

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

#if Proxy
/// <summary>
/// 已检金额
/// </summary>
        [JsonIgnore]
        public virtual decimal ChekAllMoney
        {
            get
            {
                var sumMcusPayMoneys = CustomerItemGroup?.Where(r => r.CheckState == 2 || r.CheckState == 3).Sum(r => r.TTmoney);
                return sumMcusPayMoneys ?? 0.00m;
            }
        }
#endif

       
    }
}