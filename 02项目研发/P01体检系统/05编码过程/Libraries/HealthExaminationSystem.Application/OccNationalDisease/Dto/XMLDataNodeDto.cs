using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
    [XmlRoot("DataExchange")]
    public partial class XMLDataNodeDto
    {
        [XmlElement("Header")]
        public HeaderDto Header { get; set; }

        [XmlElement("EventBody")]
        public XmlEventBodyDto EventBody { get; set; }
    }
}
