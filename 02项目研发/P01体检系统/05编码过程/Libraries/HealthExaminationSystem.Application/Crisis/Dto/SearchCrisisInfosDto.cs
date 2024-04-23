using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 检索危急值信息
    /// </summary>
    public class SearchCrisisInfosDto
    {
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位登记idList
        /// </summary>
        public List<Guid> ClientRegId { get; set; }
        /// <summary>
        /// 回访状态
        /// </summary>
        public int? CallBackState { get; set; }
        /// <summary>
        /// 推送状态
        /// </summary>
        public int? MsgState { get; set; }
        /// <summary>
        /// 体检开始时间
        /// </summary>
        public DateTime?  StartCheckTime { get; set; }
        /// <summary>
        /// 体检结束时间
        /// </summary>
        public DateTime? EndCheckTime { get; set; }
    }
}
