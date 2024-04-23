using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public class SearchImportData
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public Guid? ClientRegId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 时间类别
        /// </summary>
        public int? TimeType { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public Guid? SuitId { get; set; }

        /// <summary>
        /// 科室类别
        /// </summary>
        public virtual int? DepartType { get; set; }


    }
}
