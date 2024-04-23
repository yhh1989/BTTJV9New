using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto
{
   public  class OutRegCusListDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

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
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string SuitName { get; set; }

        /// <summary>
        /// 预约状态
        /// </summary>
        public virtual string State { get; set; }

        /// <summary>
        /// 体检来源 字典ClientSource
        /// </summary>
        public virtual int? InfoSource { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>      
        public Guid? PersonnelCategoryId { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string OrderNum { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string Introducer { get; set; }


        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentTime { get; set; }

    }
}
