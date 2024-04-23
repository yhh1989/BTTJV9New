using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    public class CustomerPrintInfoOutput
    {
        /// <summary>
        /// 预约标识
        /// </summary>
        public Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime? ExaminationDate { get; set; }

        /// <summary>
        /// 总检状态
        /// </summary>
        public int? SummSate { get; set; }

        /// <summary>
        /// 打印状态
        /// </summary>
        public int? PrintSate { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 总检人
        /// </summary>
        public long? EmployeeBMId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public long? ShEmployeeBMId { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PrintTimes { get; set; }

        /// <summary>
        /// 第一次打印日期
        /// </summary>
        public DateTime? FirstPrintTime { get; set; }

        /// <summary>
        /// 最有一次打印日期
        /// </summary>
        public DateTime? LastPrintTime { get; set; }

        /// <summary>
        /// 第一次打印者
        /// </summary>
        public string FirstPrintUser { get; set; }

        /// <summary>
        /// 最后一次打印者
        /// </summary>
        public string LastPrintUser { get; set; }
    }
}