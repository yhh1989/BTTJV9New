using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CustomerItemGroupDto : EntityDto<Guid>
    {
        ///// <summary>
        ///// 体检人项目
        ///// </summary>
        //public virtual ICollection<CustomerRegItemDto> CustomerRegItem { get; set; }

        ///// <summary>
        ///// 体检人预约ID
        ///// </summary>
        //public virtual QueryCustomerRegDto CustomerRegBM { get; set; }
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
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        ///// <summary>
        ///// 项目组合ID
        ///// </summary>
        //public virtual ItemGroupDto ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 是否条码核收 1未核收2已核收
        /// </summary>

        public virtual int? CollectionState { get; set; }
    }
}