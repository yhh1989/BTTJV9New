using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
#if !Proxy
using AutoMapper;
#endif
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlMReceiptInfo))]
#endif
    public class CustomerSFTypeDto : EntityDto<Guid>
    { 
        /// <summary>
      /// 体检人信息标识
      /// </summary>      
        public virtual Guid? CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerSFTypeCusRegDto CustomerReg { get; set; }

#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string cusIsFree
        {
            get
            {
                if (CustomerReg?.PersonnelCategory?.IsFree == true)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
               
            }
        }
        /// <summary>
        /// 收费状态:1正常收费2已作废
        /// </summary>
#if Application
        [IgnoreMap]
#endif     
        public virtual int? Count { get; set; }
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual decimal cusPayMoney
        {
            get
            {
                if (CustomerReg?.PersonnelCategory?.IsFree == true)
                {
                    int normal= (int)AddMinusType.Normal;
                    int add = (int)AddMinusType.Add;
                    int adjustadd = (int)AddMinusType.AdjustAdd;
                    return MReceiptDetailses.Where(o=>o.IsAddMinus== normal ||o.IsAddMinus==add || o.IsAddMinus==adjustadd).Sum(o=>o.GroupsDiscountMoney);
                }
                else
                {
                    return Actualmoney;
                }

            }
        }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual GroupCustomerDto Customer { get; set; }
        /// <summary>
        /// 结算明细记录
        /// </summary>
        public virtual ICollection<MReceiptInfoDetailedViewDto> MReceiptDetailses { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string ISclient
        {
            get
            {
                if (!string.IsNullOrEmpty(ClientName) && ClientName!= "个人" && ClientName != "单位自费")
                {
                    return "团体";
                }
                else
                {
                    return "个人";
                }

            }
        }

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual UserViewDto User { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        public virtual DateTime ChargeDate { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }
        /// <summary>
        /// 收费状态:1正常收费2已作废
        /// </summary>
        public virtual int ReceiptSate { get; set; }

    }
}
