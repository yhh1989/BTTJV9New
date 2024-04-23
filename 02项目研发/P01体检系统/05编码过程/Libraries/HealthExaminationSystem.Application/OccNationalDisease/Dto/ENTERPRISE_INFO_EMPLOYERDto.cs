using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class ENTERPRISE_INFO_EMPLOYERDto
    {
        /// <summary>
        /// 用工单位名称
        /// </summary>
        [XmlElement("ENTERPRISE_NAME_EMPLOYER")]
        public string ENTERPRISE_NAME_EMPLOYER { get; set; }
        /// <summary>
        /// 用工单位统一社会信用代码
        /// </summary>
        [XmlElement("CREDIT_CODE_EMPLOYER")]
        public string CREDIT_CODE_EMPLOYER { get; set; }

        /// <summary>
        /// 用工单位企业类型代码‐
        /// </summary>
        [XmlElement("ECONOMIC_TYPE_CODE_EMPLOYER")]
        public string    ECONOMIC_TYPE_CODE_EMPLOYER { get; set; }

        /// <summary>
        /// 用工单位行业类别代码
        /// </summary>
        [XmlElement("INDUSTRY_CATEGORY_CODE_EMPLOYER")]
        public string INDUSTRY_CATEGORY_CODE_EMPLOYER { get; set; }

        /// <summary>
        /// 用工单位企业规模代码
        /// </summary>
        [XmlElement("BUSINESS_SCALE_CODE_EMPLOYER")]
        public string BUSINESS_SCALE_CODE_EMPLOYER { get; set; }
        /// <summary>
        /// 用工单位所属地区代码
        /// </summary>
        [XmlElement("ADDRESS_CODE_EMPLOYER")]
        public string ADDRESS_CODE_EMPLOYER { get; set; }

        /// <summary>
        /// 用工单位所在区全名称（如：北京市市辖区海淀区）
        /// </summary>
        [XmlElement("ALL_NAME_EMPLOYER")]
        public string ALL_NAME_EMPLOYER { get; set; }
    }
}
