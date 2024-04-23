using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    public class TdbInterfaceDocWhere
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDatetime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDatetime;
        /// <summary>
        /// 档案号
        /// </summary>
        [Required]
        public string inactivenum;
        /// <summary>
        /// 科室id
        /// </summary>
        public Guid? departmentID;
        /// <summary>
        /// 组合id
        /// </summary>
        public Guid?GroupID;
    }
}
