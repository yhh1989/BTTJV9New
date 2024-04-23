using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
 public    class AUDIT_INFODto
    {
        /// <summary>
        /// 审核状态代码
        /// </summary>
        [XmlElement("AUDITSTATUS")]
        public string AUDITSTATUS { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [XmlElement("AUDITINFO")]
        public string AUDITINFO { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [XmlElement("AUDITDATE")]
        public string AUDITDATE { get; set; }

        /// <summary>
        /// 审核人姓名
        /// </summary>
        [XmlElement("AUDITOR_NAME")]
        public string AUDITOR_NAME { get; set; }

        /// <summary>
        /// 审核机构
        /// </summary>
        [XmlElement("AUDIT_ORG")]
        public string AUDIT_ORG { get; set; }

      
    }
}
