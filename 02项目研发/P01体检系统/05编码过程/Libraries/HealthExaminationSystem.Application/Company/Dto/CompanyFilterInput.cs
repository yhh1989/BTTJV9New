using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位过滤条件输入数据传输对象
    /// </summary>
    public class CompanyFilterInput
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        [StringLength(24)]
        public string Code { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        public string ClientName { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public List<string> ClientSourceList { get; set; }

        /// <summary>
        /// 客户服务用户标识
        /// </summary>
        public long? CustomerServiceUserId { get; set; }

        /// <summary>
        /// 企业负责人
        /// </summary>
        [StringLength(32)]
        public string LinkMan { get; set; }

        /// <summary>
        /// 创建时间（起）
        /// </summary>
        public DateTime? CreationTimeStart { get; set; }

        /// <summary>
        /// 创建时间（止）
        /// </summary>
        public DateTime? CreateTimeEnd { get; set; }

#if Application
        /// <summary>
        /// 获取 CreationTimeStart 的 Date 值
        /// </summary>
        /// <exception cref="NullReferenceException">如果 CreationTimeStart 为空</exception>
        [Newtonsoft.Json.JsonIgnore]
        public DateTime CreationTimeStartValueDate
        {
            get
            {
                if (CreationTimeStart.HasValue)
                {
                    return CreationTimeStart.Value.Date;
                }

                return DateTime.Today;
            }
        }

        /// <summary>
        /// 获取 CreateTimeEnd 的 Date 值，将在实际值的 Day 值加 1 处理
        /// </summary>
        /// <exception cref="NullReferenceException">如果 CreateTimeEnd 为空</exception>
        [Newtonsoft.Json.JsonIgnore]
        public DateTime CreationTimeEndValueDate
        {
            get
            {
                if (CreateTimeEnd.HasValue)
                {
                    return CreateTimeEnd.Value.Date.AddDays(1);
                }

                return DateTime.Today.AddDays(1);
            }
        }
#endif
    }
}