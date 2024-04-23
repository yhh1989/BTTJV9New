using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
  public   class EXAM_ITEM_RESULTDto
    {
        /// <summary>
        /// 检查项目父级名称
        /// </summary>
        [XmlElement("EXAM_ITEM_PNAME")]
        public string EXAM_ITEM_PNAME { get; set; }
        /// <summary>
        /// 检查项目名称
        /// </summary>
        [XmlElement("EXAM_ITEM_NAME")]
        public string EXAM_ITEM_NAME { get; set; }
        /// <summary>
        /// 检查项目代码
        /// </summary>
        [XmlElement("EXAM_ITEM_CODE")]
        public string EXAM_ITEM_CODE { get; set; }
        /// <summary>
        /// 检查结果类型代码
        /// </summary>
        [XmlElement("EXAM_RESULT_TYPE")]
        public string EXAM_RESULT_TYPE { get; set; }
        /// <summary>
        /// 检查结果
        /// </summary>
        [XmlElement("EXAM_RESULT")]
        public string EXAM_RESULT { get; set; }
        /// <summary>
        /// 检查项目计量单位及参考值范围
        /// </summary>
        [XmlElement("EXAM_ITEM_UNIT_CODE")]
        public string EXAM_ITEM_UNIT_CODE { get; set; }
        /// <summary>
        /// 参考值范围小值
        /// </summary>
        [XmlElement("REFERENCE_RANGE_MIN")]
        public string REFERENCE_RANGE_MIN { get; set; }
        /// <summary>
        /// 参考值范围大值
        /// </summary>
        [XmlElement("REFERENCE_RANGE_MAX")]
        public string REFERENCE_RANGE_MAX { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        [XmlElement("ABNORMAL")]
        public string ABNORMAL { get; set; }
       
    }
}
