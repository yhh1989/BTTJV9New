#if !Proxy 
using AutoMapper;
using HealthExaminationSystem.Enumerations.Helpers;

#endif
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class ReportHSCusDto
    {
        /// <summary>
        /// 人员编号
        /// </summary>       
        public virtual int? ClientCusBM { get; set; }


        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        ///未体检
        /// </summary>
        public virtual string NoCheck { get; set; }
        /// <summary>
        ///已体检
        /// </summary>
        public virtual string HasCheck { get; set; }

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
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(16)]
        public virtual string WorkNumber { get; set; }

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
        public virtual string MarriageStatusF
        {
            get
            {
                if (MarriageStatus.HasValue)
                {
                    var Physical = MarrySateHelper.MarrySateFormatter(MarriageStatus);
                    return Physical;
                }
                else
                {
                    return "";
                }
            }

        }

    }
}
