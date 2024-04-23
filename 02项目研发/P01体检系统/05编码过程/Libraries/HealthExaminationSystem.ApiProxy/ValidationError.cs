using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy
{
    /// <summary>
    /// 验证错误
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// 信息
        /// </summary>
        [XmlElement("message")]
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 成员列表
        /// </summary>
        [XmlElement("members")]
        [JsonProperty("members")]
        public List<string> Members { get; set; }
    }
}
