using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthExaminationSystem.Enumerations.Helpers;
#if !Proxy
using AutoMapper;
#endif 

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 团体结账单
    /// </summary>
    public class InquireGroupCustomerRegDto
    {
        /// <summary>
        /// 付费人员信息
        /// </summary>
        public List<Pas> PayPersons { get; set; }

    }
    public class Pas : Peison
    {
        /// <summary>
        /// 加项团付价
        /// </summary>
        public decimal AddItemGroupTeamPrice { get; set; }
    }
    public class Peison
    {
        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
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
#if Application
        [IgnoreMap]
#endif
        public virtual string SexStatus
        {
            get
            {
                if (Sex.HasValue)
                {
                    var SexF = SexHelper.CustomSexFormatter(Sex);
                    return SexF;
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual string MarriageStatus { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public virtual string WorkNumber { get; set; }
        /// <summary>
        /// 套餐金额
        /// </summary>
        public virtual decimal CostPrice { get; set; }
        /// <summary>
        ///未体检
        /// </summary>
        public virtual string NoCheck { get; set; }
        /// <summary>
        ///已体检
        /// </summary>
        public virtual string HasCheck { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatPhysical
        {
            get
            {
                if (CheckSate.HasValue)
                {
                    var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                    return Physical;
                }
                else
                {
                    return "";
                }
            }

        }
#if Application
        [IgnoreMap]
#endif

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal SumPrice { get; set; }
    }
}