using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    public class SearchCustomerRegForPrintPreviewDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        [StringLength(24)]
        public virtual string IdCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public virtual Guid? ClientId { get; set; }
        /// <summary>
        /// 单位分组Id
        /// </summary>
        public virtual Guid? ClientTeamId { get; set; }
        /// <summary>
        /// 套餐Id
        /// </summary>
        public virtual Guid? SuitId { get; set; }
        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }
        /// <summary>
        /// 职业报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? ISZY { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndtDate { get; set; }
        /// <summary>
        /// 时间类型1登记时间2审核时间3体检时间
        /// </summary>
        public virtual int? DateType { get; set; }

        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? StarCabitTime { get; set; }
        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? EndCabitTime { get; set; }
        /// <summary>
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }

        /// <summary>
        /// 开票名称
        /// </summary>      
        public virtual string FPName{ get; set; }
        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }



        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

        /// <summary>
        /// 是否逾期 1是 2否 3全部
        /// </summary>
        public virtual int? isYQ { get; set; }

    }
}