using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientInfo))]
#endif
    public class ClientInfosViewDto : EntityDto<Guid>
    {
        // 为什么不使用 ParentId
        // 这里没有多大的必要放入一个 Parent 对象
        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual ClientInfosViewDto Parent { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        public string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpCode { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public virtual string WubiCode { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public virtual string OrganizationCode { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        public virtual string StoreAdressP { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        public virtual string StoreAdressS { get; set; }

        /// <summary>
        /// 所属区
        /// </summary>
        public virtual string StoreAdressQ { get; set; }
        /// <summary>
        /// 所属乡镇
        /// </summary>     
        public virtual string StoreAdressX { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public virtual string Clientlndutry { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 合同性质
        /// </summary>
        public virtual string Clientcontract { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 传真号
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public virtual string PostCode { get; set; }

        /// <summary>
        /// 企业邮箱
        /// </summary>
        public virtual string ClientEmail { get; set; }

        /// <summary>
        /// 企业QQ
        /// </summary>
        public virtual string ClientQQ { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public virtual string ClientBank { get; set; }

        /// <summary>
        /// 开户账号
        /// </summary>
        public virtual string ClientAccount { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public virtual string ClientSource { get; set; }

        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 格式化来源
        ///// </summary>
//#if Application
//        [IgnoreMap]
//#endif
//        public virtual string ClientSourceFormat
//        {
//            get
//            {
//                if (!string.IsNullOrWhiteSpace(ClientSource))
//                {
//                    if (int.TryParse(ClientSource, out var result))
//                    {
//                       return EnumHelper.GetEnumDesc((ExaminationSource)result);
//                    }
//                }

//                return string.Empty;
//            }
//        }

        /// <summary>
        /// 客服专员 默认创建人
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 企业负责人
        /// </summary>
        public virtual string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 是否限制
        /// </summary>
        public virtual int Limit { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        public virtual string ClientSate { get; set; }

        ///// <summary>
        ///// 服务专员
        ///// </summary>
        //public UserForComboDto user { get; set; }
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
        /// 企业规模
        /// </summary>
        public int? Scale { get; set; }

        //
        // 摘要:
        //     Last modification date of this entity.
        public  DateTime? LastModificationTime { get; set; }
        //
        // 摘要:
        //     Last modifier user of this entity.
        public  long? LastModifierUserId { get; set; }

        //
        // 摘要:
        //     Creation time of this entity.
        public  DateTime CreationTime { get; set; }
        //
        // 摘要:
        //     Creator of this entity.
        public  long? CreatorUserId { get; set; }

    }
}