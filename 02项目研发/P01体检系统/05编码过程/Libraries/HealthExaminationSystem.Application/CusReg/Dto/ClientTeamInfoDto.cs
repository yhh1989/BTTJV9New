using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
#endif


namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 前台登记单位分组信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class ClientTeamInfoDto : EntityDto<Guid>
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
        /// 单位预约id
        /// </summary>
        public virtual Guid ClientReg_Id { get; set; }
        /// <summary>
        /// 单位预约登记
        /// </summary>
        //public virtual ClientRegDto ClientReg { get; set; }
        /// <summary>
        /// 加项支付方式 单位支付，个人支付，固定金额
        /// </summary>
        public virtual int JxType { get; set; }
        /// <summary>
        /// 加项折扣
        /// </summary>
        public virtual decimal Jxzk { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }
        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }
        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }
        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }
        /// <summary>
        /// 是否早餐
        /// </summary>
        public virtual int? BreakfastStatus { get; set; }

        /// <summary>
        /// 是否短信
        /// </summary>
        public virtual int? MessageStatus { get; set; }

        /// <summary>
        /// 是否邮寄
        /// </summary>
        public virtual int? EmailStatus { get; set; }

        /// <summary>
        /// 是否健康管理
        /// </summary>
        public virtual int? HealthyMGStatus { get; set; }

        /// <summary>
        /// 是否盲检查 1正常2盲检
        /// </summary>
        public virtual int BlindSate { get; set; }

        /// <summary>
        /// 体检类型 字典 TJType
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(256)]
        public virtual string RiskName { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual List<ShowOccHazardFactorDto> OccHazardFactors { get; set; }

        /// <summary>
        /// 检查类别
        /// </summary>
        [StringLength(256)]
        public virtual string CheckType { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(256)]
        public virtual string WorkShop { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(256)]
        public virtual string WorkType { get; set; }

    }
}