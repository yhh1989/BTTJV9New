using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    public class QueryInfoDto
    {
        /// <summary>
        /// 交表状态
        /// </summary>
        public int? SendToConfrim { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerRegNum { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int? ItemState { get; set; }
    }
}