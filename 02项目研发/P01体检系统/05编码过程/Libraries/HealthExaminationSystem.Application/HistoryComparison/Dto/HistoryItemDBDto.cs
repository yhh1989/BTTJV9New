using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(HistoryItemValueDto))]
#endif
    public class HistoryItemDBDto
    {
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string CustomerBM { get; set; }
      
        /// <summary>
        /// 登记日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public virtual DateTime? CheckDate1 { get; set; }
        

        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartName { get; set; }


        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string GroupName { get; set; }


        /// <summary>
        /// 项目编码
        /// </summary>
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public virtual string ItemValue { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public virtual string ItemValue1 { get; set; }
        /// <summary>
        /// 参考值
        /// </summary>
        public virtual string Stand { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>
        public virtual string Symbol { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>
        public virtual string Symbol1 { get; set; }

        /// <summary>
        /// 趋势
        /// </summary>
        public virtual string qs { get; set; }


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
    }
}
