using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class NInCusInfoDto
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
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 套餐编码
        /// </summary>      
        public virtual string ItemSuitBM { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>
        public virtual List<InCusQusTionDto> Questions { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual List<NCusGroupsDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 个性化加项包
        /// </summary>
        public virtual List<NCusAddPacksDto> TjlCusAddpackages { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        public int? NucleicAcidType { get; set; }

        /// <summary>
        /// 开单医生编码
        /// </summary>
       
        public virtual string OrderUserId { get; set; }
        /// <summary>
        /// 1时代表客户线上付款
        /// </summary>

        public virtual string isWxPay { get; set; }

        /// <summary>
        /// 64位图片（可空）
        /// </summary>
        public string photofile { get; set; }


    }
}
