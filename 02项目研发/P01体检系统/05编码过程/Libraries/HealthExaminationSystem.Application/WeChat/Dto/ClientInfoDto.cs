#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
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
    [AutoMapTo(typeof(TjlClientInfoDto))]
#endif
    public class ClientInfoDto
    {
        /// <summary>
        /// 单位组合记录
        /// </summary>
        public virtual InCusGroupsDto ClientCustomItemSuits { get; set; }

        /// <summary>
        /// 父级单位标识
        /// </summary>
       
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual ClientInfoDto Parent { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ClientInfoDto ClientReg { get; set; }

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
    }
}
