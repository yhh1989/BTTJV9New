using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
    [XmlRoot("EventBody")]
    public class EventBodyDto
    {
        /// <summary>
		/// 职业健康档案数据列表节点
		/// </summary>
		[XmlArray("ENTERPRISE_INFO_LIST")]
        [XmlArrayItem("ENTERPRISE_INFO")]
        public List<ENTERPRISE_INFODto> ENTERPRISE_INFO { get; set; }
    }
}
