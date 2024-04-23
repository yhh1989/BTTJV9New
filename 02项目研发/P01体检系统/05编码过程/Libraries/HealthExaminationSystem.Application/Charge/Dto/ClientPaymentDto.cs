#if !Proxy
using AutoMapper;
#endif
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlMReceiptInfo), typeof(TjlClientReg))]
#endif
    public class ClientPaymentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }      
       
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }
        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int FZState { get; set; }

        /// <summary>
        /// 单位预约 人员
        /// </summary>
        public virtual ICollection<CustomerGroupMoneyDto> CustomerReg { get; set; }
        /// <summary>
        /// 单位信息
        /// </summary>     
        public virtual ClientInfoSimpDto ClientInfo { get; set; }
        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual ICollection<ReceiptClientDto> MReceiptInfo { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual decimal YingShouJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMoney);
                return sumMcusPayMoneys ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 实检金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual decimal ShiJianJinE
        {
            get
            {

                //var McusPayMoneys = CustomerReg?.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
                //var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
                //return sumMcusPayMoneys ?? decimal.Parse("0.00");
                //o=>o.CustomerItemGroup.All(r => r.CheckState == complete || r.CheckState == part)
                int complete = (int)ProjectIState.Complete;
                int part = (int)ProjectIState.Part;
                var customeritemgroup = CustomerReg?.Select(o => o.CustomerItemGroup.Where(n=>n.CheckState==complete || n.CheckState==part).Sum(m=>m.TTmoney) );
                var sumgroup = customeritemgroup?.Sum();
                return sumgroup ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 加项金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual decimal AddJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientAddMoney);
                return sumMcusPayMoneys ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 体检人数
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual int TiJianRenShu
        {
            get
            {
                var count = CustomerReg?.Count;
                return count ?? 0;
            }
        }
        /// <summary>
        /// 实检人数
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual int SumSJRS
        {
            get
            {
                var count = CustomerReg?.Where(r => r.CheckSate != 1).Count();
                return count ?? 0;
            }
        }

        /// <summary>
        /// 已收金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual decimal SumChargeMoney
        {
            get
            {
                int sf = (int)InvoiceStatus.Valid;
                var count = MReceiptInfo.Where(o => o.ReceiptSate == sf).Sum(o => o?.Actualmoney);
                return count ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 结算状态
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string ChageSt
        {
            get
            {
                int sf = (int)InvoiceStatus.Valid;
                var count = MReceiptInfo.Where(o => o.ReceiptSate == sf).Count();
                if (FZState == 1)
                {
                    return "已结算";
                }
                else if (FZState == 2 && count > 0)
                {
                    return "部分结算";
                }
                else
                {
                    return "未结算";
                }
            }
        }
    }
}
