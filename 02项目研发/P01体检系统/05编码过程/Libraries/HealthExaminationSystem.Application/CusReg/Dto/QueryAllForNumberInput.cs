using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    public class QueryAllForNumberInput
    {
        /// <summary>
        /// 体检类别
        /// </summary>
        public string CheckType { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 时间类别
        /// </summary>
        public int? DateType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateEnd { get; set; }
        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime? DateStarts { get; set; }
        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime? DateEnds { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public int? CheckState { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public int? RegisterState { get; set; }
        /// <summary>
        /// 人员类别ID
        /// </summary>
        public Guid? PersonalTypeId { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        public int? SelectType { get; set; }

        /// <summary>
        /// 介绍人名字
        /// </summary>
        public string Introducer { get; set; }

        /// <summary>
        /// 是否查询周
        /// </summary>
        public bool WeekQuery { get; set; }

    }
    public class CacheDtos
    {

        public virtual string CreatTime { get; set; }

        public virtual string GroupOrPersonal { get; set; }

    }
}
