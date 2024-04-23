using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 结账单按分组计算
    /// </summary>
    public class StatisticsTTJZDTeamTTJZDDto
    {
        /// <summary>
        /// 加项总金额
        /// </summary>
        public decimal isAddMoney { get; set; }
        /// <summary>
        /// 加项折后金额
        /// </summary>
        public decimal isDisAddMoney { get; set; }
        /// <summary>
        /// 加项已检查金额
        /// </summary>
        public decimal isCheckedAddMoney { get; set; }
        /// <summary>
        /// 统计数据
        /// </summary>
        public List<TeamTTJZD> TeamTTJZDs { get; set; }
    }
    /// <summary>
    /// 团体团体结账单
    /// </summary>
    public class TeamTTJZD
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string ItemGroupName { get; set; }
        /// <summary>
        /// 项目类别 团体/个人
        /// </summary>
        public string ItemCategory { get; set; }
        /// <summary>
        /// 已检人数
        /// </summary>
        public int CheckedNum { get; set; }
        /// <summary>
        /// 未检金额
        /// </summary>
        public int UnCheckedNum { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal SumMoney { get; set; }
        /// <summary>
        /// 折后金额
        /// </summary>
        public decimal PriceAfterDis { get; set; }
        /// <summary>
        /// 已检金额
        /// </summary>
        public decimal CheckMoney { get; set; }
        /// <summary>
        /// 加项/正常项目
        /// </summary>
        public string IsAdd { get; set; }
        /// <summary>
        /// 加减正常项目标志
        /// </summary>
        public int? IsAddMinus { get; set; }
    }
}
