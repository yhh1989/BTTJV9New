
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    public class SearchOccQueCus
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 职业史扫描状态 1未扫描2已扫描
        /// </summary>
        public virtual int? OccQuesSate { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string  CusRegBM { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public virtual Guid? RegId { get; set; }
    }
}
