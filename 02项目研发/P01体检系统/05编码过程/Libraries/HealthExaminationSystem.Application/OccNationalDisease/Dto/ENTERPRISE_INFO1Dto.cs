using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class ENTERPRISE_INFO1Dto
    {
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
        /// 所属地区代码
        /// </summary>
        [XmlElement("ADDRESS_CODE")]
        public string ADDRESS_CODE { get; set; }
        /// <summary>
        /// 用人单位详细地址
        /// </summary>
        [XmlElement("ADDRESS_DETAIL")]
        public string ADDRESS_DETAIL { get; set; }
        /// <summary>
        /// 用人单位地址邮政代码
        /// </summary>
        [XmlElement("ADDRESS_ZIP_CODE")]
        public string ADDRESS_ZIP_CODE { get; set; }
        /// <summary>
        /// 单位联系人
        /// </summary>
        [XmlElement("ENTERPRISE_CONTACT")]
        public string ENTERPRISE_CONTACT { get; set; }
        /// <summary>
        /// 用人单位联系人电话
        /// </summary>
        [XmlElement("CONTACT_TELPHONE")]
        public string CONTACT_TELPHONE { get; set; }
        /// <summary>
        /// 用人单位所在区全名称
        /// </summary>
        [XmlElement("ALL_NAME")]
        public string ALL_NAME { get; set; }
      
      
      
    }
}
