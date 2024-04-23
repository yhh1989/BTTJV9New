using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientInfo))]
#endif
    public class CompanyInformationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父级单位标识
        /// </summary>
        public virtual Guid? ParentId { get; set; }

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
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        public virtual string Address { get; set; }

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
        /// 用户标识
        /// </summary>
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
        /// 是否限制
        /// </summary>
        public virtual int Limit { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        [StringLength(2)]
        public virtual string ClientSate { get; set; }
    }
}