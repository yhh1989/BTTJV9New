using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto
{
   public  class OutCusInfoDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
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
        /// 挂号科室
        /// </summary>
        public int? NucleicAcidType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startdate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// 是否已打条码 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? BarState { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

    }
}
