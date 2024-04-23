using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
 public   class EXAM_CONCLUSIONDto
    {
        /// <summary>
        /// 职业健康危害因素代码
        /// </summary>
        [XmlElement("ITAM_CODE")]
        public string ITAM_CODE { get; set; }
        /// <summary>
        /// 危害因素名称
        /// </summary>
        [XmlElement("ITAM_NAME")]
        public string ITAM_NAME { get; set; }
        /// <summary>
        /// 体检结论代码
        /// </summary>
        [XmlElement("EXAM_CONCLUSION_CODE")]
        public string EXAM_CONCLUSION_CODE { get; set; }
        /// <summary>
        /// 疑似职业健康代码
        /// </summary>
        [XmlElement("YSZYB_CODE")]
        public string YSZYB_CODE { get; set; }
        /// <summary>
        /// 职业禁忌证名称
        /// </summary>
        [XmlElement("ZYJJZ_NAME")]
        public string ZYJJZ_NAME { get; set; }
        /// <summary>
        /// 其他疾病或异常描述
        /// </summary>
        [XmlElement("QTJB_NAME")]
        public string QTJB_NAME { get; set; }
        /// <summary>
        /// 开始接害日期
        /// </summary>
        [XmlElement("HARM_START_DATE")]
        public string HARM_START_DATE { get; set; }
        /// <summary>
        /// 实际接害工龄- 年
        /// </summary>
        [XmlElement("HARM_AGE_YEAR")]
        public string HARM_AGE_YEAR { get; set; }
        /// <summary>
        /// 实际接害工龄- 月
        /// </summary>
        [XmlElement("HARM_AGE_MONTH")]
        public string HARM_AGE_MONTH { get; set; }
      
    }
}
