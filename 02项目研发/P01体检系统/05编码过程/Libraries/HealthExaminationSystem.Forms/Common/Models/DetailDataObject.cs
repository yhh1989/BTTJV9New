using System;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 记录数据对象
    /// </summary>
    public class DetailDataObject
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        public string ItemGroupName { get; set; }

        /// <summary>
        /// 项目组合状态
        /// </summary>
        public string ItemGroupState { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 测量结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 项目标示
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 检查医生
        /// </summary>
        public string ExamineDoctor { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? ExamineDate { get; set; }

        /// <summary>
        /// 科室小结
        /// </summary>
        public string DepartmentSummary { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        public string ReferenceValue { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public string AuditDoctor { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string PictureString { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string CustomerSex { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public int? CustomerAge { get; set; }

        /// <summary>
        /// 标本
        /// </summary>
        public string Specimen { get; set; }

        /// <summary>
        /// 检验号
        /// </summary>
        public string TestNo { get; set; }

        /// <summary>
        /// 医师
        /// </summary>
        public string Doctor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 科室地址
        /// </summary>
        public string DepartmentAddress { get; set; }

        /// <summary>
        /// 多个项目组合名称的拼接
        /// </summary>
        public string ItemGroups { get; set; }

        /// <summary>
        /// 是否餐前项目
        /// </summary>
        public bool? Breakfast { get; set; }
    }
}