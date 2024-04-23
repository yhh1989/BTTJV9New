using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
   public  class HistoryResultMainDto
    {
       
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual Guid DepartId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartName { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartOrder { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? GroupOrder { get; set; }
        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }
        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual Guid? GroupId { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual Guid ItemId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        ///项目编码
        /// </summary>
        public string ItemBM { get; set; }
        /// <summary>
        /// 本次参考值
        /// </summary>
        public virtual string Stand { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public ICollection<HistoryResultDetailDto> historyResultDetailDtos { get; set; }

    }
}
