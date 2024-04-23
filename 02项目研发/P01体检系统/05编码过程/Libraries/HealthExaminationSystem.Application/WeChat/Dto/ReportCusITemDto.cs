using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class ReportCusITemDto
    {


        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }

        public virtual DepartmentSimpleDto CustomerItemGroupBM { get; set; }

        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>       
        public virtual Guid? CustomerItemGroupBMid { get; set; }


        /// <summary>
        /// 项目ID
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemCodeBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目类别 1说明性2数值型3计算性
        /// </summary>
        public virtual int? ItemTypeBM { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 危急值状态 1正常2危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }
        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 检查人ID
        /// </summary>
        public virtual UserViewDto InspectEmployeeBM { get; set; }


    }
}
