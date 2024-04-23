#if Application
using Abp.AutoMapper;
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
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class WXClientInfoDto
    {

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
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        [StringLength(64)]
        public virtual string Clientlndutry { get; set; }       

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
        /// 最后修改时间
        /// </summary>
       
        public virtual DateTime? LastDate { get; set; }

    }
}
