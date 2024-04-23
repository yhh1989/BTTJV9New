using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto
{
   public class SearchtCrisisMessageDto
    {
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
        /// 报告开始时间
        /// </summary>
        public virtual DateTime? CheckDateStart { get; set; }
        /// <summary>
        /// 报告结束时间
        /// </summary>
        public virtual DateTime? CheckDateEnd { get; set; }


        /// <summary>
        /// 单位信息标识
        /// </summary>      
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 通知开始日期
        /// </summary>
        public virtual DateTime? MessageTimeStar { get; set; }

        /// <summary>
        /// 通知结束日期
        /// </summary>
        public virtual DateTime? MessageTimeEnd { get; set; }


        /// <summary>
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? CrisisVisitSate { get; set; }
        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 科室外键
        /// </summary>        
        public virtual Guid? DepartmentId { get; set; }

        /// <summary>
        /// 通知状态1未通知2已通知
        /// </summary>
        public virtual int? MessageState { get; set; }
    }
}
