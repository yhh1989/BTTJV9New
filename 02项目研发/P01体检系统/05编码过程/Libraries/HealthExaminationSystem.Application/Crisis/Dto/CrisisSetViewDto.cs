using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 设置危急值页面Dto
    /// </summary>
    public class CrisisSetViewDto
    {
        /// <summary>
        /// 登记id
        /// </summary>
        public Guid CustomerRegId { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 单位Id
        /// </summary>
        public Guid? ClinentId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 危急值信息list
        /// </summary>
        public List<CrisisInfo> CrisisList { get; set; }
        /// <summary>
        /// 项目结果List
        /// </summary>
        public List<CustomerRegItemDto> RegItemList { get; set; }


    }
    /// <summary>
    /// 危急值列表
    /// </summary>
    public class CrisisInfo : EntityDto<Guid>
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
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 项目结果记录id
        /// </summary>
        public Guid RegItemId { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string ItemResultChar { get; set; }
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 设置人
        /// </summary>
        public string SetName { get; set; }
        /// <summary>
        /// 设置说明
        /// </summary>
        public string SetNotice { get; set; }
        /// <summary>
        /// 回访方式 
        /// </summary>
        public int? CallBackType { get; set; }
        /// <summary>
        /// 回访内容
        /// </summary>
        public string CallBacKContent { get; set; }
        /// <summary>
        /// 回访状态 
        /// </summary>
        public int? CallBackState { get; set; }
        /// <summary>
        /// 回访人
        /// </summary>
        public string CallBackName { get; set; }
        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime? CallBackDate { get; set; }
    }
    /// <summary>
    /// 项目结果表
    /// </summary>
    public class CustomerRegItemDto : EntityDto<Guid>
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
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string ItemResultChar { get; set; }
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? CheckTime { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        public string Symbol { get; set; }
    }
}
