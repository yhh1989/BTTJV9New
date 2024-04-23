using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    public class TjlCustomerQuery
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 体检编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 查询时间类型1体检日期，其他登记日期
        /// </summary>
        public virtual int? DateType { get; set; }

        /// <summary>
        /// 预约编号
        /// </summary>
        public virtual Guid? CustomerRegID { get; set; }



        /// <summary>
        /// 总检医生
        /// </summary>
        public virtual long[] arrEmployeeName_Id { get; set; }
        /// <summary>
        /// 总检医生类别1审核医生0其他
        /// </summary>
        public virtual int? EmployeeNameType { get; set; }
        /// <summary>
        /// 是否查询内外科
        /// </summary>
        public virtual bool? IsGetNeiWaiKe { get; set; }
        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientrRegID { get; set; }

        /// <summary>
        /// 总检状态
        /// </summary>
        public virtual int? SumState { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 疾控上传状态
        /// </summary>
        public virtual int? UpZybState { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual List<int> PhysicalTypelist { get; set; }
    }
}