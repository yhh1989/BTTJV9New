using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    public class SearchClass
    {
        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        ///科室ID
        /// </summary>
        public Guid? DepartId { get; set; }


        /// <summary>
        ///组合ID
        /// </summary>
        public Guid? GroupId { get; set; }


        /// <summary>
        ///项目ID
        /// </summary>
        public Guid? ItemId { get; set; }

     

    }
}
