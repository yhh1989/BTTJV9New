using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;

#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位分组登记项目
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientTeamRegitem))]
#endif
    public class GroupClientItemsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///</summary>
        public virtual Guid? ClientTeamInfoId { get; set; }

        public virtual ClientTeamInfosDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        public virtual GroupCusReg ClientTeamInfos { get; set; }
    }
}