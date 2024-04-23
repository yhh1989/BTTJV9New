using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 体检人组合记录
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CustomerItemGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual ICollection<SearchCustomerRegItemDto> CustomerRegItem { get; set; }

        /// <summary>
        /// 体检人预约主键
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(64)]
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual Guid? ItemGroupBM_Id { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 组合小结 bxy
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupSum { get; set; }
        /// <summary>
        /// 组合诊断 bxy
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupDiagnosis { get; set; }
        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }
        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }
    }
}
