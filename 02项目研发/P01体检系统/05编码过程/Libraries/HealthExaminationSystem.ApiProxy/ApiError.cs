using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy
{
    /// <summary>
    /// 应用程序编程接口错误
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("code")]
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [XmlElement("message")]
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        [XmlElement("details")]
        [JsonProperty("details")]
        public string Details { get; set; }

        /// <summary>
        /// 验证错误列表
        /// </summary>
        [XmlElement("validationErrors")]
        [JsonProperty("validationErrors")]
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
