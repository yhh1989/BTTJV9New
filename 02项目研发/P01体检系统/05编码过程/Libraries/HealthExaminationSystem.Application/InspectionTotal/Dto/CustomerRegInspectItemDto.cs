using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人检查项目结果表
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerRegItem))]
#endif 
    public class CustomerRegInspectItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerRegId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual Guid DepartmentId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(16)]
        public virtual string DepartmentName { get; set; }


        
        /// <summary>
        /// 项目Id
        /// </summary>
        [StringLength(16)]
        public virtual Guid ItemId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(16)]
        public virtual string ItemName { get; set; }  

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? GroupCheckState { get; set; }

        /// <summary>
        /// 项目状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? ProcessState { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrderNum { get; set; }

        /// <summary>
        /// 危急值状态 1正常2危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }

        /// <summary>
        /// 项目小结
        /// </summary>
        public virtual string ItemSum { get; set; }

        /// <summary>
        /// 项目诊断
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemDiagnosis { get; set; }
        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 检查人ID
        /// </summary>
        [StringLength(100)]
        public virtual string InspectEmployeeBMName { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [StringLength(100)]
        public virtual string CheckEmployeeBMName { get; set; }

    /// <summary>
    /// 总审核人ID
    /// </summary>
    [StringLength(100)]
    public virtual string TotalEmployeeBMName { get; set; }

        /// <summary>
        /// 组合小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupSum { get; set; }

        /// <summary>
        /// 组合诊断 bxy
        /// </summary>       
        public virtual string ItemGroupDiagnosis { get; set; }

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }
        /// <summary>
        /// 是否隐私项目 1是2不是
        /// </summary>
        public virtual int? PrivacyState { get; set; }
        public virtual string Stand { get; set; }

        /// <summary>
        /// 报告编码 
        /// </summary>
        [StringLength(640)]
        public virtual string ReportBM { get; set; }
        /// <summary>
        /// 格式化分组+状态
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string GroupNameRefundFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(GroupCheckState.ToString()))
                {
                    if (int.TryParse(GroupCheckState.ToString(), out var result))
                    {
                        string striszyb = "";
                        if (IsZYB == 1)
                        {
                            striszyb = "--职业检";
                        }
                        if (PrivacyState.HasValue && PrivacyState.Value == 1)
                        {
                            return $"{ItemGroupName}({EnumHelper.GetEnumDesc((ProjectIState)result)})(隐私){ striszyb}";
                        }
                        else
                        {
                            return $"{ItemGroupName}({EnumHelper.GetEnumDesc((ProjectIState)result)}){ striszyb}";
                        }
                    }
                }

                return string.Empty;
            }
        }
    }
}
