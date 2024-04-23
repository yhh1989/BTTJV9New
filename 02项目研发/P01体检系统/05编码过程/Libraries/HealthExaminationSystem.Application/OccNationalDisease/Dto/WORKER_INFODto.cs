using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class WORKER_INFODto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [XmlElement("WORKER_NAME")]
        public string WORKER_NAME { get; set; }
        /// <summary>
        /// 身份证件类型代码
        /// </summary>
        [XmlElement("ID_CARD_TYPE_CODE")]
        public string ID_CARD_TYPE_CODE { get; set; }
        /// <summary>
        /// 身份证件号码
        /// </summary>
        [XmlElement("ID_CARD")]
        public string ID_CARD { get; set; }
        /// <summary>
        /// 性别代码
        /// </summary>
        [XmlElement("GENDER_CODE")]
        public string GENDER_CODE { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [XmlElement("BIRTH_DATE")]
        public string BIRTH_DATE { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [XmlElement("WORKER_TELPHONE")]
        public string WORKER_TELPHONE { get; set; }
        


    }
}
