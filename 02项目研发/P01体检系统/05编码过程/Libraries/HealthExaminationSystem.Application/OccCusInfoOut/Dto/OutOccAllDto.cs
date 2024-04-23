using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
  public   class OutOccAllDto
    {
        /// <summary>
        /// 1.职业健康档案信息
        /// </summary>       
        public virtual List<OutOccCusInfoDto> OutOccCusInfos
        { get; set; }
        /// <summary>
        ///  2.职业健康档案-用人单位信息
        /// </summary>       
        public virtual List<OutOccClientInfoDto> OutOccClientInfos
        { get; set; }

        /// <summary>
        /// 3.职业健康档案-接触危害因素
        /// </summary>       
        public virtual List<OutOccRiskSDto> OutOccRiskS
        { get; set; }

        /// <summary>
        ///4.职业健康档案-体检危害因素
        /// </summary>       
        public virtual List<OutOccRiskSTimeDto> OutOccRiskSTimes
        { get; set; }

        /// <summary>
        ///5.职业健康档案-检查项目
        /// </summary>       
        public virtual List<OutOccCusItemDto> OutOccCusItems
        { get; set; }
        /// <summary>
        /// 6.职业健康档案-体检结论
        /// </summary>       
        public virtual List<OutOccCusSumDto> OutOccCusSums
        { get; set; }


    }
}
