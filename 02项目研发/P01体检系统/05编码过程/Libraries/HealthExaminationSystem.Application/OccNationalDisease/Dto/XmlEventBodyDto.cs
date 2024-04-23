using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
    [XmlRoot("EventBody")] 
    public class XmlEventBodyDto
    {
        /// <summary>
		/// 职业健康档案数据列表节点
		/// </summary>
		[XmlArray("HEALTH_EXAM_RECORD_LIST")]
        [XmlArrayItem("HEALTH_EXAM_RECORD")]
        public List<HEALTH_EXAM_RECORDDto> HEALTH_EXAM_RECORD { get; set; }
    }
}
