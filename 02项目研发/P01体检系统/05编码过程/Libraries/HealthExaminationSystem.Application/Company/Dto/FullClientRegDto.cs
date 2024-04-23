using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using System.Collections.Generic;
using System.Linq;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位预约列表信息
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientReg))]
#endif
    public class FullClientRegDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public ClientInfoRegDto ClientInfo { get; set; }
        /// <summary>
        /// 人员组合Dto
        /// </summary>
        public ICollection<ChargeCusStateDto> CustomerReg { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
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
        public virtual decimal ShiJianJinE
        {
            get
            {

                var McusPayMoneys = CustomerReg?.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
                return sumMcusPayMoneys ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
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
        /// 加项金额
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        public virtual decimal JianxiangJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMinusMoney);
                return sumMcusPayMoneys ?? decimal.Parse("0.00");
            }
        }
        /// <summary>
        /// 体检人数
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
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
        public virtual int SumSJRS
        {
            get
            {
                var count = CustomerReg?.Where(r => r.CheckSate != 1).Count();
                return count ?? 0;
            }
        }

        /// <summary>
        /// 登记人数
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        public virtual int RegRS
        {
            get
            {
                var count = CustomerReg?.Where(r => r.RegisterState != 1).Count();
                return count ?? 0;
            }
        }

#if !Proxy
        [IgnoreMap]
#endif
        public virtual int WJRS
        {
            get
            {
                var count = CustomerReg?.Where(r => r.CheckSate == 1).Count();
                return count ?? 0;
            }
        }
#if !Proxy
        [IgnoreMap]
#endif
        /// <summary>
        ///总人数
        /// </summary>
        public virtual int ZongRenShu
        {
            get
            {
                return SumSJRS+WJRS;
            }
        }
        /// <summary>
        /// 预约ID
        /// </summary>
        public string ClientRegBM { get; set; }

        /// <summary>
        /// 预约次数
        /// </summary>
        public int? ClientRegNum { get; set; }

        /// <summary>
        /// 预约人数
        /// </summary>
        public int? RegPersonCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 单位负责人 默认单位负责人
        /// </summary>
        public string linkMan { get; set; }

        /// <summary>
        /// 客服专员 默认创建人
        /// </summary>
        public UserForComboDto user { get; set; }
        
        /// <summary>
        /// 是否盲检 1正常2盲检
        /// </summary>
        public int BlindSate { get; set; }

        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public int FZState { get; set; }

        /// <summary>
        /// 锁定状态 1锁定2未锁定
        /// </summary>
        public int SDState { get; set; }

        /// <summary>
        /// 是否控制检查时间 默认不控制
        /// </summary>
        public int ControlDate { get; set; }

        /// <summary>
        /// 到检人数
        /// </summary>

        public int? CheckNumber { get; set; }

        /// <summary>
        /// 总检人数
        /// </summary>
        public int? GeneralNumber { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        public int ClientSate { get; set; }

        /// <summary>
        /// 单位状态：1.已完成；2未完成
        /// </summary>
        public int? ClientCheckSate { get; set; }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string CheckSate
        {
            get
            {
                return ClientCheckSate == 1 ? "已完成" : "未完成";
            }
        }
        public int TenantId { get; set; }
    }
}