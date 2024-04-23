using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{ 
   public  class HEALTH_EXAM_RECORDDto  
    {
        /// <summary>
        /// Id
        /// </summary>

        [XmlAttribute("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 用人单位名称
        /// </summary>

        [XmlElement("ENTERPRISE_INFO")]
        public ENTERPRISE_INFO1Dto ENTERPRISE_INFO { get; set; }
        /// <summary>
        /// 用工单位名称
        /// </summary>

        [XmlElement("ENTERPRISE_INFO_EMPLOYER")]
        public ENTERPRISE_INFO_EMPLOYERDto ENTERPRISE_INFO_EMPLOYER { get; set; }
        
        /// <summary>
        /// 劳动者信息
        /// </summary>
        [XmlElement("WORKER_INFO")]
        public WORKER_INFODto WORKER_INFO { get; set; }
        /// <summary>
        /// 监测类型代码
        /// </summary>
        [XmlElement("JC_TYPE")]
        public string JC_TYPE { get; set; }
        /// <summary>
        /// 体检类型代码
        /// </summary>
        [XmlElement("EXAM_TYPE_CODE")]
        public string EXAM_TYPE_CODE { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        [XmlElement("EXAM_DATE")]
        public string EXAM_DATE { get; set; }
        /// <summary>
        /// 接触的职业健康危害因素代码
        /// </summary>
        [XmlElement("CONTACT_FACTOR_CODE")]
        public string CONTACT_FACTOR_CODE { get; set; }
        /// <summary>
        /// 接触的其他职业健康危害因素
        /// </summary>
        [XmlElement("CONTACT_FACTOR_OTHER")]
        public string CONTACT_FACTOR_OTHER { get; set; }
        /// <summary>
        /// 体检危害因素代码
        /// </summary>
        [XmlElement("ACTOR_CODE")]
        public string ACTOR_CODE { get; set; }
        /// <summary>
        /// 体检的其他危害因素具体名称
        /// </summary>
        [XmlElement("FACTOR_OTHER")]
        public string FACTOR_OTHER { get; set; }
        /// <summary>
        /// 工种代码
        /// </summary>
        [XmlElement("WORK_TYPE_CODE")]
        public string WORK_TYPE_CODE { get; set; }
        /// <summary>
        /// 其他工种名称
        /// </summary>
        [XmlElement("OTHER_WORK_TYPE")]
        public string OTHER_WORK_TYPE { get; set; }
        /// <summary>
        /// 是否复查
        /// </summary>
        [XmlElement("IS_REVIEW")]
        public string IS_REVIEW { get; set; }
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
        /// 体检结论信息
        /// </summary>
        [XmlArray("EXAM_CONCLUSION_LIST")]
        [XmlArrayItem("EXAM_CONCLUSION")]       
        public List<EXAM_CONCLUSIONDto> EXAM_CONCLUSION { get; set; }
        /// <summary>
        /// 检查项目信息
        /// </summary>
        [XmlArray("EXAM_ITEM_RESULT_LIST")]
        [XmlArrayItem("EXAM_ITEM_RESULT")]
        public List<EXAM_ITEM_RESULTDto> EXAM_ITEM_PNAME { get; set; }
        /// <summary>
        /// 填表人姓名
        /// </summary>
        [XmlElement("WRITE_PERSON")]
        public string WRITE_PERSON { get; set; }
        /// <summary>
        /// 填表人联系电话
        /// </summary>
        [XmlElement("WRITE_PERSON_TELPHONE")]
        public string WRITE_PERSON_TELPHONE { get; set; }
        /// <summary>
        /// 报告出具日期
        /// </summary>
        [XmlElement("WRITE_DATE")]
        public string WRITE_DATE { get; set; }
        /// <summary>
        /// 检查单位名称
        /// </summary>
        [XmlElement("REPORT_ORGAN_CREDIT_CODE")]
        public string REPORT_ORGAN_CREDIT_CODE { get; set; }
        /// <summary>
        /// 报告人姓名
        /// </summary>
        [XmlElement("REPORT_PERSON")]
        public string REPORT_PERSON { get; set; }
        /// <summary>
        /// 报告人联系电话
        /// </summary>
        [XmlElement("REPORT_PERSON_TEL")]
        public string REPORT_PERSON_TEL { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        [XmlElement("REPORT_DATE")]
        public string REPORT_DATE { get; set; }
        /// <summary>
        /// 报告单位名称
        /// </summary>
        [XmlElement("REPORT_UNIT")]
        public string REPORT_UNIT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("REMARK")]
        public string REMARK { get; set; }

        /// <summary>
        /// 审核信息
        /// </summary>
        [XmlElement("AUDIT_INFO")]
        public AUDIT_INFODto AUDIT_INFO { get; set; }
    }
}
