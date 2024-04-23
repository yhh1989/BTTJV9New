using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 单位信息
    /// </summary>
    public class TjlClientInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 单位组合记录
        /// </summary>
        public virtual ICollection<TjlClientCustomItemSuit> ClientCustomItemSuits { get; set; }

        /// <summary>
        /// 父级单位标识
        /// </summary>
        [ForeignKey(nameof(Parent))]
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual TjlClientInfo Parent { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ICollection<TjlClientReg> ClientReg { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [StringLength(24)]
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        [StringLength(256)]
        public virtual string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public virtual string HelpCode { get; set; }

        /// <summary>
        /// 五笔编码
        /// </summary>
        [StringLength(128)]
        public virtual string WubiCode { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        [StringLength(64)]
        public virtual string OrganizationCode { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        [StringLength(16)]
        public virtual string StoreAdressP { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        [StringLength(16)]
        public virtual string StoreAdressS { get; set; }

        /// <summary>
        /// 所属区
        /// </summary>
        [StringLength(32)]
        public virtual string StoreAdressQ { get; set; }

        /// <summary>
        /// 所属乡镇
        /// </summary>
        [StringLength(32)]
        public virtual string StoreAdressX { get; set; }


        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 登记，需要相应的员工登记才可以查看，默认值1
        /// </summary>
        public virtual int? ClientDegree { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [StringLength(128)]
        public virtual string ClientSource { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        [StringLength(64)]
        public virtual string Clientlndutry { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        [StringLength(64)]
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 合同性质
        /// </summary>
        [StringLength(64)]
        public virtual string Clientcontract { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 传真号
        /// </summary>
        [StringLength(32)]
        public virtual string Fax { get; set; }

        /// <summary>
        /// 企业负责人
        /// </summary>
        [StringLength(32)]
        public virtual string LinkMan { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [StringLength(16)]
        public virtual string PostCode { get; set; }

        /// <summary>
        /// 服务专员
        /// </summary>
        public virtual User user { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        [ForeignKey(nameof(user))]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 企业邮箱
        /// </summary>
        [StringLength(32)]
        public virtual string ClientEmail { get; set; }

        /// <summary>
        /// 企业QQ
        /// </summary>
        [StringLength(32)]
        public virtual string ClientQQ { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [StringLength(32)]
        public virtual string ClientBank { get; set; }

        /// <summary>
        /// 开户账号
        /// </summary>
        [StringLength(32)]
        public virtual string ClientAccount { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(32)]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 是否限制（暂用职业病上传状态）
        /// </summary>
        public virtual int Limit { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        [MaxLength(2)]
        public virtual string ClientSate { get; set; }

        /// <summary>
        /// 单位储值表
        /// </summary>
        public ICollection<TjlMClientStoreds> MClientStoredses { get; set; }

        /// <summary>
        /// 单位排期记录
        /// </summary>
        [InverseProperty(nameof(TjlScheduling.ClientInfo))]
        public virtual ICollection<TjlScheduling> Schedulings { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 18位社会信用代码
        /// </summary>
        /// [StringLength(100)]
        public string SocialCredit { get; set; }
        /// <summary>
        /// 经济类型
        /// </summary>
        public int? EconomicType { get; set; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int? Scale { get; set; }


        /// <summary>
        /// 第三方编码
        /// </summary>
        [StringLength(32)]
        public virtual string Code { get; set; }
    }
}