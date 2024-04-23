using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class CreateClientRegDto : EntityDto<Guid>
    {

        /// <summary>
        /// 预约编码
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
        /// 单位负责人
        /// </summary>
        public string linkMan { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndCheckDate { get; set; }

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
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        public int ClientSate { get; set; }

        /// <summary>
        /// 单位状态：1.已完成；2未完成
        /// </summary>
        public int? ClientCheckSate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 客服ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public Guid ClientInfo_Id { get; set; }

        /// <summary>
        /// 登记状态
        /// </summary>
        public bool? RegisterState { get; set; }

        ///// <summary>
        ///// 是否免费
        ///// </summary>
        //public bool? IsFree { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public Guid? PersonnelCategoryId { get; set; }

        /// <summary>
        /// 预约描述
        /// </summary>
      
        public virtual string RegInfo { get; set; }


        /// <summary>
        /// 出报告天数
        /// </summary>
        public int? ReportDays { get; set; }
    }
}