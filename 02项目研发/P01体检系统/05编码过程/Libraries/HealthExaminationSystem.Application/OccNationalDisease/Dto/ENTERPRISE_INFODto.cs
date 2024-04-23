using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
  public   class ENTERPRISE_INFODto
    {
        /// <summary>
        /// Id
        /// </summary>

        [XmlAttribute("id")]
        public string id { get; set; }
        /// <summary>
        /// 用人单位名称
        /// </summary>

        [XmlElement("ENTERPRISE_NAME")]
        public string ENTERPRISE_NAME { get; set; }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        [XmlElement("CREDIT_CODE")]
        public string CREDIT_CODE { get; set; }
        /// <summary>
        /// 所属地区代码
        /// </summary>
        [XmlElement("ADDRESS_CODE")]
        public string ADDRESS_CODE { get; set; }
        /// <summary>
        /// 企业类型代码
        /// </summary>
        [XmlElement("ECONOMIC_TYPE_CODE")]
        public string ECONOMIC_TYPE_CODE { get; set; }
        /// <summary>
        /// 行业类别代码
        /// </summary>
        [XmlElement("INDUSTRY_CATEGORY_CODE")]
        public string INDUSTRY_CATEGORY_CODE { get; set; }
        /// <summary>
        /// 企业规模代码
        /// </summary>
        [XmlElement("BUSINESS_SCALE_CODE")]
        public string BUSINESS_SCALE_CODE { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [XmlElement("ADDRESS_DETAIL")]
        public string ADDRESS_DETAIL { get; set; }
        /// <summary>
        /// 邮政代码
        /// </summary>
        [XmlElement("ADDRESS_ZIP_CODE")]
        public string ADDRESS_ZIP_CODE { get; set; }
        /// <summary>
        /// 单位联系人
        /// </summary>
        [XmlElement("ENTERPRISE_CONTACT")]
        public string ENTERPRISE_CONTACT { get; set; }
        /// <summary>
        /// 单位联系电话
        /// </summary>
        [XmlElement("CONTACT_TELPHONE")]
        public string CONTACT_TELPHONE { get; set; }
        /// <summary>
        /// 是否为子公司
        /// </summary>
        [XmlElement("ISSUBSIDIARY")]
        public string ISSUBSIDIARY { get; set; }
        /// <summary>
        /// 子公司二级代码
        /// </summary>
        [XmlElement("TWOLEVELCODE")]
        public string TWOLEVELCODE { get; set; }
        /// <summary>
        /// 创建地区代码
        /// </summary>
        [XmlElement("AREA_CODE")]
        public string AREA_CODE { get; set; }
        /// <summary>
        /// 创建机构代码
        /// </summary>
        [XmlElement("ORG_CODE")]
        public string ORG_CODE { get; set; }
        /// <summary>
        /// 填表单位名称
        /// </summary>
        [XmlElement("WRITE_UNIT")]
        public string WRITE_UNIT { get; set; }

        /// <summary>
        /// 填表人姓名
        /// </summary>
        [XmlElement("WRITE_PERSON")]
        public string WRITE_PERSON { get; set; }

        /// <summary>
        /// 填表人联系电话
        /// </summary>
        [XmlElement("WRITE_PERSON_TEL")]
        public string WRITE_PERSON_TEL { get; set; }

        /// <summary>
        /// 填表日期
        /// </summary>
        [XmlElement("WRITE_DATE")]
        public string WRITE_DATE { get; set; }

        /// <summary>
        /// 报告单位名称
        /// </summary>
        [XmlElement("REPORT_UNIT")]
        public string REPORT_UNIT { get; set; }

        /// <summary>
        /// 报告人姓名
        /// </summary>
        [XmlElement("REPORT_PERSON")]
        public string REPORT_PERSON { get; set; }

        /// <summary>
        /// 报告人电话
        /// </summary>
        [XmlElement("REPORT_PERSON_TEL")]
        public string REPORT_PERSON_TEL { get; set; }

        /// <summary>
        /// 报告日期
        /// </summary>
        [XmlElement("REPORT_DATE")]
        public string REPORT_DATE { get; set; }

        [XmlElement("AUDIT_INFO")]
        public AUDIT_INFODto AUDIT_INFO { get; set; }

    }
}
