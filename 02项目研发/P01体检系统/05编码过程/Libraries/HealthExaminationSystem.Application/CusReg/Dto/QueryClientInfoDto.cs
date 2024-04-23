using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 仅做单位本身查询，这里不关联其他
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class QueryClientInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        public virtual string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpCode { get; set; }
        /// <summary>
        /// 企业邮箱
        /// </summary>
        [StringLength(32)]
        public virtual string ClientEmail { get; set; }
        /// <summary>
        /// 企业负责人
        /// </summary>
        [StringLength(32)]
        public virtual string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(32)]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        public virtual string Address { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 18位社会信用代码
        /// </summary>
        /// [StringLength(100)]
        public string SocialCredit { get; set; }
        /// <summary>
        /// 合同性质
        /// </summary>
        [StringLength(64)]
        public virtual string Clientcontract { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int? Scale { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [StringLength(64)]
        public virtual string Clientlndutry { get; set; }

        /// <summary>
        /// 经济类型
        /// </summary>
        public int? EconomicType { get; set; }


     


    }
}
