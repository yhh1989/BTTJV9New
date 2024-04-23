#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    [AutoMapTo(typeof(TjlCustomerReg))]
#endif
    public class InCusInfoDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string OrderId { get; set; }
        /// <summary>
        /// 是否单位（0个人，1单位）
        /// </summary>
        public virtual int IsTT { get; set; }
        /// <summary>
        /// 预约类型（pc预约，1微信预约）
        /// </summary>
        public virtual int RegType { get; set; }
        /// <summary>
        ///租户ID
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
       
        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(16)]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string WeixinH { get; set; }


        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 套餐信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid? ItemSuitBMId { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>
        public virtual ICollection<InCusQusTionDto> Questions { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual ICollection<InCusGroupsDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 加项包
        /// </summary>
        public virtual ICollection<InCusAddPacksDto> TjlCusAddpackages { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual ICollection<INCusAddPackItems> TjlCusAddPackItems { get; set; }

    }
}
