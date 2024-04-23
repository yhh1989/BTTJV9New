using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    public class CustomerRegsInputDto
    {
        /// <summary>
        /// 体检状态
        /// </summary>
        public int? RegState { get; set; }

        /// <summary>
        /// 搜索值
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public Guid? ClientinfoId { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }

        /// <summary>
        /// 体检状态
        /// </summary>
        public int? CheckState { get; set; }
    }
}