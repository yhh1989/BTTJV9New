using System;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 参数数据对象
    /// </summary>
    public class MasterDataObject
    {
        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string ParameterCustomerName { get; set; }

        /// <summary>
        /// 体检人性别
        /// </summary>
        public string ParameterCustomerSex { get; set; }

        /// <summary>
        /// 根据性别显示文字
        /// </summary>
        public string ParameterCustomerSexExtend { get; set; }

        /// <summary>
        /// 体检人体检号
        /// </summary>
        public string ParameterCustomerNo { get; set; }

        /// <summary>
        /// 体检人单位
        /// </summary>
        public string ParameterCustomerCompany { get; set; }

        /// <summary>
        /// 体检人年龄
        /// </summary>
        public int? ParameterCustomerAge { get; set; }

        /// <summary>
        /// 体检人登记时间
        /// </summary>
        public DateTime? ParameterCustomerRegisterDate { get; set; }

        /// <summary>
        /// 体检人电话/联系电话
        /// </summary>
        public string ParameterCustomerPhone { get; set; }

        /// <summary>
        /// 总检汇总结果
        /// </summary>
        public string ParameterSummarizing { get; set; }

        /// <summary>
        /// 总检建议内容
        /// </summary>
        public string ParameterSuggest { get; set; }

        /// <summary>
        /// 总检时间
        /// </summary>
        public DateTime? ParameterSuggestDate { get; set; }

        /// <summary>
        /// 总检医生
        /// </summary>
        public string ParameterSuggestDoctor { get; set; }

        /// <summary>
        /// 总检结论
        /// </summary>
        public string ParameterExamineVerdict { get; set; }

        /// <summary>
        /// 体检人身份证号
        /// </summary>
        public string ParameterCustomerIdCard { get; set; }

        /// <summary>
        /// 体检类型
        /// </summary>
        public string ParameterExamineType { get; set; }

        /// <summary>
        /// 导引单流水号
        /// </summary>
        public int? ParameterSerialNumber { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        public string ParameterIntroducer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ParameterRemark { get; set; }

        /// <summary>
        /// 体检人档案号
        /// </summary>
        public string ParameterCustomerArchivesNo { get; set; }
    }
}