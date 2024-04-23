using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室工作量项目明细
    /// </summary>
    public class SearchKSGZLItemStatisticsDto
    {
        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 项目状态 放弃数1
        /// </summary>
        public virtual int? GiveUpNum { get; set; }
        /// <summary>
        /// 项目状态 已检数2
        /// </summary>
        public virtual int? CompleteNum { get; set; }
        /// <summary>
        /// 项目状态 待查3
        /// </summary>
        public virtual int? AwaitNum { get; set; }

    }
}
