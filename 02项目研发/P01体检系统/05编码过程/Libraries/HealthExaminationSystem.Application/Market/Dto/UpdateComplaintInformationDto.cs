using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 更新投诉信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ComplaintInformation))]
#endif
    public class UpdateComplaintInformationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人标识
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        public Guid? CompanyInformationId { get; set; }

        /// <summary>
        /// 单位预约信息标识
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }

        /// <summary>
        /// 单位预约分组信息标识
        /// </summary>
        public Guid? CompanyRegisterTeamId { get; set; }

        /// <summary>
        /// 体检人预约标识
        /// </summary>
        public Guid CustomerRegisterId { get; set; }

        /// <summary>
        /// 投诉内容
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [StringLength(512)]
        public string Result { get; set; }

        /// <summary>
        /// 投诉方式
        /// </summary>
        /// <remarks>
        /// 请参考：Sw.Hospital.HealthExaminationSystem.Core.Coding.TbmBasicDictionary
        /// <para>
        /// 类型：Sw.Hospital.HealthExaminationSystem.Common.Enums.BasicDictionaryType.ComplainWay
        /// </para>
        /// </remarks>
        public int? ComplainWay { get; set; }

        /// <summary>
        /// 投诉类别
        /// </summary>
        /// <remarks>
        /// 请参考：Sw.Hospital.HealthExaminationSystem.Core.Coding.TbmBasicDictionary
        /// <para>
        /// 类型：Sw.Hospital.HealthExaminationSystem.Common.Enums.BasicDictionaryType.ComplainCategory
        /// </para>
        /// </remarks>
        public int? ComplainCategory { get; set; }

        /// <summary>
        /// 投诉时间
        /// </summary>
        public DateTime ComplainTime { get; set; }

        /// <summary>
        /// 被投诉人标识
        /// </summary>
        public long? ComplainUserId { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public long HandlerId { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public ComplaintProcessState ProcessState { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessingTime { get; set; }

        /// <summary>
        /// 紧急级别
        /// </summary>
        public ComplaintExigencyLevel ExigencyLevel { get; set; }
    }
}