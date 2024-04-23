using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 客户回访页面Dto
    /// </summary>
    public class CustomerServiceCrisisViewDto : EntityDto<Guid>
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
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Moblie { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>
        public string ItemResultChar { get; set; }
        /// <summary>
        /// 设置人姓名
        /// </summary>
        public string SetName { get; set; }
        /// <summary>
        /// 设置说明
        /// </summary>
        public string SetNotice { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public int? MsgState { get; set; }
        /// <summary>
        /// 回访状态
        /// </summary>
        public int CallbackState { get; set; }
        /// <summary>
        /// 设置时间
        /// </summary>
        public DateTime SetTime { get; set; }
    }
}
