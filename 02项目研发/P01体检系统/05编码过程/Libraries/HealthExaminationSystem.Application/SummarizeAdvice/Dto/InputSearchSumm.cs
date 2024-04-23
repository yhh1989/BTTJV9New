using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
    /// <summary>
    /// 查询体检人建议 入参
    /// </summary>
    public class InputSearchSumm
    {

        /// <summary>
        /// 科室Id集合
        /// </summary>
        public virtual List<Guid?> DepartmentId { get; set; }

        /// <summary>
        ///     适用性别 1男2女3不限
        /// </summary>
        public virtual int? SexState { get; set; }
        
        /// <summary>
        ///     年龄
        /// </summary>
        public virtual int? Age { get; set; }
        
        /// <summary>
        ///     建议名称
        /// </summary>
        public virtual string Name { get; set; }

    }
}
